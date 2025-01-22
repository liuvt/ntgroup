using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using ntgroup.Extensions;

namespace ntgroup.APIs;

public class SpreadsAuthenServer : ISpreadsAuthenServer
{

    protected readonly IConfiguration configuration;
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string sheetUsers = "Users";
    private readonly string sheetDrives = "Drives";
    private readonly string spreadSheetId = "1K8qLOLf4YTEmyw6wLEH-HvTwpPcD0EtIia3o55V7XQs"; //SpreadID of Authentication
    private SheetsService sheetsService;

    //Constructor
    public SpreadsAuthenServer(IConfiguration _configuration)
    {
        this.configuration = _configuration;

        //File xác thực google tài khoản
        GoogleCredential credential;
        using (var stream = new FileStream(configuration["GoogleSheetConfig:ServiceAccount"]!, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
        }

        // Đăng ký service
        sheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = configuration["GoogleSheetConfig:ApplicationName"],
        });

    }

    // Đỗ toàn bộ dữ liệu Sheet về để xữ lý
    private async Task<IList<IList<object>>> APIGetValues(SheetsService service, string spreadsheetId, string range)
    {
        var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
        var response = await request.ExecuteAsync();
        return response.Values;
    }
    #region Users
    // Lấy toàn thông tin sheets   
    public async Task<List<Driver>> Gets()
    {
        try
        {
            var listDrivers = new List<Driver>();
            var range = $"{sheetUsers}!A2:H";
            var values = await this.APIGetValues(sheetsService, spreadSheetId, range);
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    listDrivers.Add(new Driver
                    {
                        Id = item[0].ToString() ?? string.Empty,
                        Username = item[1].ToString() ?? string.Empty,
                        PasswordHash = item[2].ToString() ?? string.Empty,
                        FullName = item[3].ToString() ?? string.Empty,
                        PhoneNumber = item[4].ToString() ?? string.Empty,
                        EmplyeeID = item[5].ToString().ToUpper() ,
                        CreatedAt = DatetimeOffsetExtensions.FromString(item[6].ToString()!),
                        Static = item[7].ToString().ToUpper(),
                    });
                }
            }
            else
            {
                throw new Exception("Không có dữ liệu sheet.");
            }

            return listDrivers.ToList();
        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }

    }

    // Lấy thông tin qua ID
    public async Task<Driver> GetById(string Id)
    {
        List<Driver> listDrivers = await this.Gets();
        var byId = listDrivers.Select(a => a).Where(a => a.Id == Id).FirstOrDefault();

        if (byId == null)
        {
            throw new Exception($"ID tài xế ({Id}) không tồn tại!");
        }

        return byId;
    }

    // Tạo dữ liệu
    public async Task<bool> Register(DriverDTO model)
    {
        try
        {
            // Kiểm tra tồn tại trùng số tài khoản và mã ngân hàng
            var listDrivers = await this.Gets();
            if (listDrivers.Any(a => a.Username == model.Username))
            {
                throw new Exception($"Tên tài khoản này đã tồn tại");
            }

            var range = $"{sheetUsers}!A2:H"; // Không chỉ định dòng
            var valueRange = new ValueRange();
            
            var passwordHash = new PasswordHasher<DriverDTO>().HashPassword(model, model.Password);

            // Convert model to object
            var objectList = new List<object>()
            {
                Guid.NewGuid().ToString(),
                model.Username,
                passwordHash,
                "null",
                "null",
                "null",
                DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"),
                "TRUE"
            };
           
            // Gán giá trị vào trong valueRange
            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = sheetsService.Spreadsheets.Values.Append(valueRange, spreadSheetId, range);
            //Type input
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var result = await appendRequest.ExecuteAsync();

            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Không thể tạo mới. {ex.Message}");
        }
    }

    // Login
    public async Task<string> Login(DriverDTO model)
    {
        try
        {
            List<Driver> listDrivers = await this.Gets();
            var byUsername = listDrivers.Select(a => a).Where(a => a.Username == model.Username).FirstOrDefault();

            if (byUsername == null) throw new Exception("Sai tài khoản");

            var verify = new PasswordHasher<DriverDTO>().VerifyHashedPassword(model, byUsername.PasswordHash, model.Password);

            if (verify == PasswordVerificationResult.Failed) throw new Exception("Sai mật khẩu");

            var token = await this.CreateToken(byUsername);
            
            return token;
        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi đăng nhập. {ex.Message}");
        }
    }
    #endregion

    private async Task<string> CreateToken(Driver _driver)
    {
        try
        {
            //Thông tin User đưa vào Token
            var listClaims = new List<Claim>
                        {
                            new Claim("id", _driver.Id),
                            new Claim("username", _driver.Username),
                            new Claim(ClaimTypes.Role, "Employee"),
                            new Claim(JwtRegisteredClaimNames.Jti, _driver.Id)
                        };

            //Khóa bí mật
            var autKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my Secret, my infomation: vanlt490811@gmail.com , the key size must be greater than: 512 bits"));

            //Tạo chữ ký với khóa bí mật
            var signCredentials = new SigningCredentials(autKey, SecurityAlgorithms.HmacSha512Signature);

            var autToken = new JwtSecurityToken(
                claims: listClaims, //Thông tin User
                issuer: "https://localhost:5110",
                audience: "Taxi Nam Thang Manager API",
                expires: DateTime.Now.AddDays(30), //Thời gian tồn tại Token
                signingCredentials: signCredentials //Chữ ký
            );

            return new JwtSecurityTokenHandler().WriteToken(autToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}