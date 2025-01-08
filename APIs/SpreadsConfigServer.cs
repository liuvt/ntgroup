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

namespace ntgroup.APIs;

public class SpreadsConfigServer : ISpreadsConfigServer
{

    protected readonly IConfiguration configuration;
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string sheetBankings = "Bankings";
    private readonly string sheetArea = "Areas";
    private SheetsService sheetsService;

    //Constructor
    public SpreadsConfigServer(IConfiguration _configuration)
    {
        this.configuration = _configuration;

        //File xác thực google tài khoản
        GoogleCredential credential;
        using (var stream = new FileStream(configuration["GoogleSheetConfig:ServiceAccount"], FileMode.Open, FileAccess.Read))
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

    // Lấy toàn thông tin sheet Bankings     
    public async Task<List<Banking>> GetsBankAll ()
    {
        try
        {
            var listBanking = new List<Banking>();
            var range = $"{sheetBankings}!A2:G";
            var values = await this.APIGetValues(sheetsService, configuration["GoogleSheetConfig:SpreadsSheetID"], range);
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    Console.WriteLine("bank_Id: "+ item[0].ToString() + "| bank_Name: "+ item[1].ToString());
                    listBanking.Add(new Banking
                    {
                        bank_Id = item[0].ToString()?? string.Empty,
                        bank_Name = item[1].ToString()?? string.Empty,
                        bank_Number = item[2].ToString()?? string.Empty,
                        bank_Type = item[3].ToString()?? string.Empty,
                        bank_AccountName = item[4].ToString()?? string.Empty,
                        bank_Url = item[5].ToString()?? string.Empty,
                        bank_Static = item[6].ToString()?? string.Empty
                    });
                }
            }
            else
            {
                throw new Exception("Không có dữ liệu Bankings sheet.");
            }

            return listBanking;
        }
        catch (Exception ex)
        {
            
            throw new Exception($"Không có dữ liệu Bankings sheet: {ex.Message}");
        }
        
    }

    // Lấy thông tin Bank qua ID bank
    public async Task<Banking> GetBankById (string bank_Id)
    {
        var listBankings = await this.GetsBankAll ();
        var byId = listBankings.Select(a => a).Where(a => a.bank_Id == bank_Id).FirstOrDefault();

        if(byId == null)
        {
            throw new Exception($"ID ngân hàng ({bank_Id}) không tồn tại!");
        }

        return byId;
    }

    // Tạo dữ liệu
    public async Task<bool> CreateBank(BankingCreateDTO model)
    {
        try
        {

            // Kiểm tra tồn tại trùng số tài khoản và mã ngân hàng
            var listBankings = await this.GetsBankAll ();
            if(listBankings.Any(a => a.bank_Id == model.bank_Id && a.bank_Number == model.bank_Number))
            {
                throw new Exception($"Số tài khoản này đã tồn tại");
            }


            var range = $"{sheetBankings}!A:G"; // Không chỉ định dòng
            var valueRange = new ValueRange();

            // Convert model to object
            var objectList = new List<object>()
            {
                model.bank_Id, 
                model.bank_Name,	
                model.bank_Number, 
                "print.png", 
                model.bank_AccountName, 
                model.bank_Url, 
                model.bank_Static
            };
            // Gán giá trị vào trong valueRange
            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = sheetsService.Spreadsheets.Values.Append(valueRange, configuration["GoogleSheetConfig:SpreadsSheetID"], range);
            //Type input
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var result = await appendRequest.ExecuteAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            
            throw new Exception($"Không thể tạo mới Bank: {ex.Message}");
        }
    }

    // Cập nhật
    public async Task UpdateBank(BankingCreateDTO model)
    {
        try
        {
            var range = $"{sheetBankings}!A:G"; // Không chỉ định dòng
            var valueRange = new ValueRange();

            // Convert model to object
            var objectList = new List<object>()
            {
                model.bank_Id, 
                model.bank_Name,	
                model.bank_Number, 
                "print.png", 
                model.bank_AccountName, 
                model.bank_Url, 
                model.bank_Static
            };
            // Gán giá trị vào trong valueRange
            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = sheetsService.Spreadsheets.Values.Append(valueRange, configuration["GoogleSheetConfig:SpreadsSheetID"], range);
            //Type input
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var result = await appendRequest.ExecuteAsync();
        }
        catch (Exception ex)
        {
            
            throw new Exception($"Không thể tạo mới Bank: {ex.Message}");
        }
    }

}