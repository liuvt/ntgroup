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

public class SpreadsShiftworkServer : ISpreadsShiftworkServer
{

    protected readonly IConfiguration configuration;
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string sheetDrives = "Drives";
    private readonly string spreadSheetId = "1VX5fbHH4A5rabQOi6lMBWEzo_Fk8QpzgzmvQ_p1_feo"; //SpreadID of DBShiftworkDrive
    private SheetsService sheetsService;

    //Constructor
    public SpreadsShiftworkServer(IConfiguration _configuration)
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

    Task<bool> ISpreadsShiftworkServer.CreateDrive(Drive model)
    {
        throw new NotImplementedException();
    }

    Task<bool> ISpreadsShiftworkServer.DeleteDrive(string drive_Id)
    {
        throw new NotImplementedException();
    }

    Task<bool> ISpreadsShiftworkServer.DeleteRowDrive(string drive_Id)
    {
        throw new NotImplementedException();
    }

    Task<Drive> ISpreadsShiftworkServer.GetDriveById(string drive_Id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Drive>> GetsDriveAll()
    {
        try
        {
            var listDrives = new List<Drive>();
            var range = $"{sheetDrives}!A2:F";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    listDrives.Add(new Drive
                    {
                        drive_Id = item[0].ToString()?? string.Empty,
                        drive_Plate = item[1].ToString()?? string.Empty,
                        drive_Name = item[2].ToString()?? string.Empty,
                        drive_Type = item[3].ToString()?? string.Empty,
                        drive_Static = item[4].ToString()?? string.Empty,
                        CreatedAt = DateTime.ParseExact(item[5].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                    });
                }
            }
            else
            {
                throw new Exception("Không có dữ liệu sheet.");
            }

            return listDrives;
        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    Task<bool> ISpreadsShiftworkServer.UpdateDrive(Drive model)
    {
        throw new NotImplementedException();
    }
}