using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;

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

        /* Lấy ID của SheetID
        // Gửi yêu cầu để lấy thông tin metadata của spreadsheet
        var request = sheetsService.Spreadsheets.Get(configuration["GoogleSheetConfig:SpreadsSheetID"]);
        var response = request.Execute();

        // Duyệt qua các sheet và in thông tin
        foreach (var sheet in response.Sheets)
        {
            Console.WriteLine($"Sheet Name: {sheet.Properties.Title}, Sheet ID: {sheet.Properties.SheetId}");
        }
       */
    }

    // Đỗ toàn bộ dữ liệu Sheet về để xữ lý
    private async Task<IList<IList<object>>> APIGetValues(SheetsService service, string spreadsheetId, string range)
    {
        var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
        var response = await request.ExecuteAsync();
        return response.Values;
    }
    #region Bankings
    // Lấy toàn thông tin sheet Bankings     
    public async Task<List<Banking>> GetsBankAll()
    {
        try
        {
            var listBanking = new List<Banking>();
            var range = $"{sheetBankings}!A2:G";
            var values = await this.APIGetValues(sheetsService, configuration["GoogleSheetConfig:SpreadsSheetID"]!, range);
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    listBanking.Add(new Banking
                    {
                        bank_Id = item[0].ToString() ?? string.Empty,
                        bank_Name = item[1].ToString() ?? string.Empty,
                        bank_Number = item[2].ToString() ?? string.Empty,
                        bank_Type = item[3].ToString() ?? string.Empty,
                        bank_AccountName = item[4].ToString() ?? string.Empty,
                        bank_Url = item[5].ToString() ?? string.Empty,
                        bank_Static = item[6].ToString() ?? string.Empty
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

            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }

    }

    // Lấy thông tin Bank qua ID bank
    public async Task<Banking> GetBankById(string bank_Id)
    {
        var listBankings = await this.GetsBankAll();
        var byId = listBankings.Select(a => a).Where(a => a.bank_Id == bank_Id).FirstOrDefault();

        if (byId == null)
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
            var listBankings = await this.GetsBankAll();
            if (listBankings.Any(a => a.bank_Id == model.bank_Id && a.bank_Number == model.bank_Number))
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

            throw new Exception($"Không thể tạo mới Bank. {ex.Message}");
        }
    }

    // Cập nhật
    public async Task<bool> UpdateBank(BankingCreateDTO model)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var listBanks = await this.GetsBankAll();
            
            // Tìm vị trí đối tượng cập nhật
            var byId = listBanks.FindIndex(a => a.bank_Id == model.bank_Id);
            if (byId == -1)
            {
                throw new Exception($"Ngân hàng này không tồn tại không thể cập nhật");
            }

            var range = $"{sheetBankings}!A{byId+2}:G"; // Không chỉ định dòng
            
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

            var updateRequest = sheetsService.Spreadsheets.Values.Update(valueRange, configuration["GoogleSheetConfig:SpreadsSheetID"], range);
            //Type input
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var result = await updateRequest.ExecuteAsync();

            Console.WriteLine("Update successfully.");
            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Không thể cập nhật. {ex.Message}");
        }
    }

    // Xóa dữ liệu
    public async Task<bool> DeleteBank(string bank_Id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var listBanks = await this.GetsBankAll();
            
            // Tìm vị trí đối tượng xóa
            var byId = listBanks.FindIndex(a => a.bank_Id == bank_Id);
            if (byId == -1)
            {
                throw new Exception($"Ngân hàng này không tồn tại không thể xóa");
            }

            var range = $"{sheetBankings}!A{byId+2}:G{byId+2}"; // Chỉ định đối tượng xóa
            
            var valueRange = new ClearValuesRequest();

            var clearRequest = sheetsService.Spreadsheets.Values.Clear(valueRange, configuration["GoogleSheetConfig:SpreadsSheetID"], range);
            var result = await clearRequest.ExecuteAsync();

            Console.WriteLine("Clear successfully.");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi Banking. {ex.Message}");
        }
    }

    // Xóa dòng dữ liệu
    public async Task<bool> DeleteRowBank(string bank_Id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var listBanks = await this.GetsBankAll();
            
            // Tìm vị trí đối tượng xóa
            var byId = listBanks.FindIndex(a => a.bank_Id == bank_Id);
            if (byId == -1)
            {
                throw new Exception($"Ngân hàng này không tồn tại không thể xóa");
            }
            
            // Create the delete row request
            var deleteRequest = new Request()
            {
                DeleteDimension = new DeleteDimensionRequest()
                {
                    Range = new DimensionRange()
                    {
                        SheetId = 0,
                        Dimension = "ROWS",
                        StartIndex = byId+1, // Row to delete (inclusive) Loại bỏ dòng tiêu đề. 
                        EndIndex = byId+1 +1, // One past the row to delete (exclusive) Loại bỏ dòng tiêu đề và dòng kế tiếp để kết thúc endindex
                    }
                }
            };
                    // Create and execute the batch update request
            var batchUpdateRequest = new BatchUpdateSpreadsheetRequest()
            {
                Requests = new[] { deleteRequest }
            };

            var request = sheetsService.Spreadsheets.BatchUpdate(batchUpdateRequest, configuration["GoogleSheetConfig:SpreadsSheetID"]);
            var response = request.Execute();

            Console.WriteLine("Row deleted successfully.");

            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi Banking. {ex.Message}");
        }
    }
    #endregion

    #region Area
    // Lấy toàn thông tin sheet Areas     
    public async Task<List<Area>> GetsAreaAll()
    {
        try
        {
            var listAreas = new List<Area>();
            var range = $"{sheetArea}!A2:D";
            var values = await this.APIGetValues(sheetsService, configuration["GoogleSheetConfig:SpreadsSheetID"]!, range);
            if (values != null && values.Count > 0)
            {
                foreach (var item in values)
                {
                    listAreas.Add(new Area
                    {
                        area_Id = item[0].ToString() ?? string.Empty,
                        area_Name = item[1].ToString() ?? string.Empty,
                        area_SpreadId = item[2].ToString() ?? string.Empty,
                        bank_Id = item[3].ToString() ?? string.Empty
                    });
                }
            }
            else
            {
                throw new Exception("Không có dữ liệu Area sheet");
            }

            return listAreas;
        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }

    }

    // Lấy thông tin Bank qua ID bank
    public async Task<Area> GetAreaById(string area_Id)
    {
        var listAreas = await this.GetsAreaAll();
        var byId = listAreas.Select(a => a).Where(a => a.area_Id == area_Id.ToUpper()).FirstOrDefault();

        if (byId == null)
        {
            throw new Exception($"Khu vực ({area_Id.ToUpper()}) không tồn tại!");
        }

        return byId;
    }

    // Tạo dữ liệu
    public async Task<bool> CreateArea(AreaCreateDTO model)
    {
        try
        {
            // Kiểm tra tồn tại
            var listAreas = await this.GetsAreaAll();
            if (listAreas.Any(a => a.area_Id == model.area_Id.ToUpper()))
            {
                throw new Exception($"Khu vực này đã tồn tại không thể tạo thêm");
            }

            if (listAreas.Any(a => a.bank_Id == model.bank_Id))
            {
                throw new Exception($"Số tài khoản ngân hàng này đang trùng với khu vực khác");
            }

            var range = $"{sheetArea}!A2:D"; // Không chỉ định dòng
            var valueRange = new ValueRange();

            // Convert model to object
            var objectList = new List<object>()
            {
                model.area_Id.ToUpper(),
                model.area_Name,
                model.area_SpreadId,
                model.bank_Id
            };

            // Gán giá trị vào trong valueRange
            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = sheetsService.Spreadsheets.Values.Append(valueRange, configuration["GoogleSheetConfig:SpreadsSheetID"], range);
            //Type input
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var result = await appendRequest.ExecuteAsync();

            Console.WriteLine("Create successfully.");
            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Không thể tạo mới Area. {ex.Message}");
        }
    }

    // Cập nhật
    public async Task<bool> UpdateArea(AreaCreateDTO model)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var listAreas = await this.GetsAreaAll();
            
            // Tìm vị trí đối tượng cập nhật
            var byId = listAreas.FindIndex(a => a.area_Id == model.area_Id.ToUpper());
            if (byId == -1)
            {
                throw new Exception($"Khu vực này không tồn tại không thể cập nhật");
            }

            if (listAreas.Any(a => a.bank_Id == model.bank_Id))
            {
                throw new Exception($"Số tài khoản ngân hàng này đang trùng với khu vực khác");
            }

            var range = $"{sheetArea}!A{byId+2}:D"; // Không chỉ định dòng
            
            var valueRange = new ValueRange();

            // Convert model to object
            var objectList = new List<object>()
            {
                model.area_Id.ToUpper(), // Không được cập nhật
                model.area_Name,
                model.area_SpreadId,
                model.bank_Id 
            };

            // Gán giá trị vào trong valueRange
            valueRange.Values = new List<IList<object>> { objectList };

            var updateRequest = sheetsService.Spreadsheets.Values.Update(valueRange, configuration["GoogleSheetConfig:SpreadsSheetID"], range);
            //Type input
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var result = await updateRequest.ExecuteAsync();

            Console.WriteLine("Update successfully.");
            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Không thể cập nhật Area. {ex.Message}");
        }
    }

    // Xóa dữ liệu
    public async Task<bool> DeleteArea(string area_Id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var listAreas = await this.GetsAreaAll();
            
            // Tìm vị trí đối tượng xóa
            var byId = listAreas.FindIndex(a => a.area_Id == area_Id.ToUpper());
            if (byId == -1)
            {
                throw new Exception($"Khu vực này không tồn tại không thể xóa");
            }

            var range = $"{sheetArea}!A{byId+2}:D{byId+2}"; // Không chỉ định dòng
            
            var valueRange = new ClearValuesRequest();

            var clearRequest = sheetsService.Spreadsheets.Values.Clear(valueRange, configuration["GoogleSheetConfig:SpreadsSheetID"], range);
            var result = await clearRequest.ExecuteAsync();

            Console.WriteLine("Clear successfully.");
            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi Area. {ex.Message}");
        }
    }

    // Xóa dòng dữ liệu
    public async Task<bool> DeleteRowArea(string area_Id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var listAreas = await this.GetsAreaAll();
            
            // Tìm vị trí đối tượng xóa
            var byId = listAreas.FindIndex(a => a.area_Id == area_Id.ToUpper());
            if (byId == -1)
            {
                throw new Exception($"Khu vực này không tồn tại không thể xóa");
            }
            
            // Create the delete row request
            var deleteRequest = new Request()
            {
                DeleteDimension = new DeleteDimensionRequest()
                {
                    Range = new DimensionRange()
                    {
                        SheetId = 1799356618,
                        Dimension = "ROWS",
                        StartIndex = byId+1, // Row to delete (inclusive)
                        EndIndex = byId+1 +1, // One past the row to delete (exclusive)
                    }
                }
            };
                    // Create and execute the batch update request
            var batchUpdateRequest = new BatchUpdateSpreadsheetRequest()
            {
                Requests = new[] { deleteRequest }
            };

            var request = sheetsService.Spreadsheets.BatchUpdate(batchUpdateRequest, configuration["GoogleSheetConfig:SpreadsSheetID"]);
            var response = request.Execute();

            Console.WriteLine("Row deleted successfully.");

            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Lỗi Area. {ex.Message}");
        }
    }

    #endregion

}