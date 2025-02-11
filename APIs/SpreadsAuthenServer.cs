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
using System.Globalization;
using ntgroup.Extensions;

namespace ntgroup.APIs;

public class SpreadsAuthenServer : ISpreadsAuthenServer
{

    protected readonly IConfiguration configuration;
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string sheetUsers = "Users";
    private readonly string sheetRoles = "Roles";
    private readonly string sheetUserRoles = "UserRoles";
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

    #region Users
    // Lấy toàn thông tin sheets   
    public async Task<List<NTUser>> Gets()
    {
        try
        {
            var listDrivers = new List<NTUser>();
            var range = $"{sheetUsers}!A2:H";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    listDrivers.Add(new NTUser
                    {
                        Id = item[0].ToString() ?? string.Empty,
                        Username = item[1].ToString() ?? string.Empty,
                        PasswordHash = item[2].ToString() ?? string.Empty,
                        FullName = item[3].ToString() ?? string.Empty,
                        PhoneNumber = item[4].ToString() ?? string.Empty,
                        EmplyeeID = item[5].ToString().ToUpper(),
                        CreatedAt = DateTime.ParseExact(item[6].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        Static = item[7].ToString().ToUpper(),
                    });
                }
            }
            else
            {
                throw new Exception("Không có dữ liệu sheet.");
            }

            return listDrivers;
        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }

    }

    // Lấy thông tin qua ID
    public async Task<NTUser> GetById(string Id)
    {
        var listDrivers = await this.Gets();
        var byId = listDrivers.Where(a => a.Id == Id).FirstOrDefault();

        if (byId == null)
        {
            throw new Exception($"ID tài xế ({Id}) không tồn tại!");
        }

        return byId;
    }

    // Tạo dữ liệu
    public async Task<bool> Register(DriverRegisterDTO model)
    {
        try
        {
            // Kiểm tra tồn tại trùng số tài khoản và mã ngân hàng
            var listDrivers = await this.Gets();
            if (listDrivers.Any(a => a.Username == model.Username))
            {
                throw new Exception($"Tên tài khoản ({model.Username}) đã tồn tại");
            }

            var range = $"{sheetUsers}!A2:H"; // Không chỉ định dòng
            var valueRange = new ValueRange();

            var passwordHash = new PasswordHasher<DriverRegisterDTO>().HashPassword(model, model.Password);

            // Convert model to object
            var objectList = new List<object>()
            {
                Guid.NewGuid().ToString().ToUpper(),
                model.Username,
                passwordHash,
                "new",
                "new",
                "new",
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                "TRUE"
            };

            // Gán giá trị vào trong valueRange
            valueRange.Values = new List<IList<object>> { objectList };
            
            //Create user
            await GGSExtensions.APICreateValues(sheetsService, spreadSheetId, range, valueRange);
           
            //Tạo role cho user
            await CreateUserRole(objectList[0].ToString()!, "5", model.Area_Id);

            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Không thể tạo mới. {ex.Message}");
        }
    }

    // Login
    public async Task<string> Login(DriverLoginDTO model)
    {
        try
        {
            var listDrivers = await this.Gets();
            
            var byUsername = listDrivers.Where(a => a.Username == model.Username).FirstOrDefault();

            if (byUsername == null) throw new Exception("Sai tài khoản");

            var verify = new PasswordHasher<DriverLoginDTO>().VerifyHashedPassword(model, byUsername.PasswordHash, model.Password);

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

    private async Task<string> CreateToken(NTUser _ntUser)
    {
        try
        {
            
            // Lấy thông tin UserRole
            var userRole = await this.GetUserRole(_ntUser.Id);
            // Lấy role
            var role = await this.GetRole(userRole.role_Id);

            //Thông tin User đưa vào Token
            var listClaims = new List<Claim>
                        {
                            new Claim("id", _ntUser.Id),
                            new Claim("username", _ntUser.Username),
                            new Claim("area", userRole.area_Id),
                            new Claim(ClaimTypes.Role, role.role_Name),
                            new Claim(JwtRegisteredClaimNames.Jti, _ntUser.Id)
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

    private async Task<Role> GetRole(string role_Id)
    {
        try
        {
            var listRoles = new List<Role>();
            var range = $"{sheetRoles}!A2:D";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    listRoles.Add(new Role
                    {
                        role_Id = item[0].ToString() ?? string.Empty,
                        role_Name = item[1].ToString() ?? string.Empty,
                        role_NormalizedName = item[2].ToString() ?? string.Empty,
                        CreatedAt = DateTime.ParseExact(item[3].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    });
                }
            }
            else
            {
                throw new Exception("Không có dữ liệu sheet.");
            }

            var role = listRoles.Where(a => a.role_Id == role_Id).FirstOrDefault();
            if(role == null)
            {
                throw new Exception("Chưa tồn tại role này");
            }
            return role;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task<UserRole> GetUserRole(string driver_Id)
    {
        try
        {
            var listUserRoles = new List<UserRole>();
            var range = $"{sheetUserRoles}!A2:C";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    listUserRoles.Add(new UserRole
                    {
                        user_Id = item[0].ToString() ?? string.Empty,
                        role_Id = item[1].ToString() ?? string.Empty,
                        area_Id = item[2].ToString() ?? string.Empty,
                    });
                }
            }
            else
            {
                throw new Exception("Không có dữ liệu sheet.");
            }

            var userRole = listUserRoles.Where(a => a.user_Id == driver_Id).FirstOrDefault();
            if(userRole == null)
            {
                throw new Exception("Không tìm thấy quyền truy cập của tài khoản này");
            }
            return userRole;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task CreateUserRole(string user_Id, string role_Id, string area_Id)
    {
        try
        {  
            //Create extensions
            await GGSExtensions.APICreateValues(sheetsService, spreadSheetId, $"{sheetUserRoles}!A2:C", new ValueRange 
            {
                Values = new List<IList<object>> 
                { 
                    new List<object> { user_Id, role_Id, area_Id } 
                }
            });
            Console.WriteLine("Create UserRole success");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}