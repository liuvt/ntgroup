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

    // Lấy thông tin sheet Bankings     
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


}