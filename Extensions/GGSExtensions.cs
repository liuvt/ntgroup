using System.Globalization;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
namespace ntgroup.Extensions;

//Google sheet extensions
public static class GGSExtensions
{
   // Đỗ toàn bộ dữ liệu Sheet về để xữ lý
    public static async Task<IList<IList<object>>> APIGetValues(SheetsService service, string spreadsheetId, string range)
    {
        var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
        var response = await request.ExecuteAsync();
        return response.Values;
    }

    // Cập nhật dữ liệu
    public static async Task<IList<IList<object>>> APIUpdateValues(SheetsService service, string spreadsheetId, string range, ValueRange valueRange)
    {
        var updateRequest = service.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        var response = await updateRequest.ExecuteAsync();
        return response.UpdatedData.Values;
    }
    // Xóa dữ liệu
    public static async Task APIRemoveValues(SheetsService service, string spreadsheetId, string range)
    {
        var clearRequest = service.Spreadsheets.Values.Clear(new ClearValuesRequest(), spreadsheetId, range);
        await clearRequest.ExecuteAsync();
    }

    // Xóa dòng dữ liệu
    public static async Task APIRemoveRows(SheetsService service, string spreadsheetId, int SheetId, int byIdIndex)
    {
        // Create the delete row request
            var deleteRequest = new Request()
            {
                DeleteDimension = new DeleteDimensionRequest()
                {
                    Range = new DimensionRange()
                    {
                        SheetId = SheetId,
                        Dimension = "ROWS",
                        StartIndex = byIdIndex+1, // Row to delete (inclusive) Loại bỏ dòng tiêu đề. 
                        EndIndex = byIdIndex+1 +1, // One past the row to delete (exclusive) Loại bỏ dòng tiêu đề và dòng kế tiếp để kết thúc endindex
                    }
                }
            };
                    // Create and execute the batch update request
            var batchUpdateRequest = new BatchUpdateSpreadsheetRequest()
            {
                Requests = new[] { deleteRequest }
            };

            var request = service.Spreadsheets.BatchUpdate(batchUpdateRequest, spreadsheetId);
            await request.ExecuteAsync();
    }
    
    // Tạo dữ liệu
    public static async Task APICreateValues(SheetsService service, string spreadsheetId, string range, ValueRange valueRange)
    {
        var appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
        await appendRequest.ExecuteAsync();
    }

     

}