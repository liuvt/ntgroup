using ClosedXML.Excel;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.ErrorMessage;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class ReportTimelineService : IReportTimelineService
{
    public async Task<List<ReportTimeline>> GetsByExcel(IBrowserFile _file)
    {
        List<ReportTimeline> listReportTimelines = new List<ReportTimeline>();
        //long maxFileSize = 1024 * 1024 * 5; // max 5MB
        var readStream = _file.OpenReadStream(_file.Size);
        var sizefile = new byte[readStream.Length];

        using (var ms = new MemoryStream(sizefile))
        {
            await readStream.CopyToAsync(ms);
            //var buffer = Convert.ToBase64String(ms.ToArray()); //Chuyển stream thành Base64

            //Đọc file trực tiếp từ bộ nhớ MemoryStream
            using var workbook = new XLWorkbook(ms);
            //Lấy sheet đầu tiên trong workbook: sheet thứ 2 của Báo cáo đồng hồ
            var xlsxSheet = workbook.Worksheet(2);

            //Kiểm tra đúng template nếu không trả exception
            if(xlsxSheet.FirstCell().CachedValue.ToString() != ExcelNotification.REPORT_TIMELINE_KEY)
            {
                throw new Exception(ExcelNotification.ERROR_FORMAT_FILE);
            }

            //Đếm tổng số dòng, thông qua dòng cuối cùng được sử dụng
            int totalRow = xlsxSheet.RowsUsed().Count();

            //Kiểm tra dòng rỗng
            int checkEmpty;
            // Vòng lặp bắt đầu Row 2
            for (int row = 6; row <= totalRow; row++)
            {
                // Trong vòng lập kiểm tra xem cột mã hợp đồng nếu rỗng thì ngưng load dữ liệu;
                var xlsxReportTimeline_IdTaxi = xlsxSheet.Cell(row, 1).CachedValue.ToString();

                //Kiểm tra nếu có ID taxi rỗng
                if (xlsxReportTimeline_IdTaxi == string.Empty)
                {
                    checkEmpty =+ 1; //1 dòng rỗng
                    if (checkEmpty > 1) // nhiều hơn 1 dòng rỗng
                    {
                        break;
                    }
                    continue;
                }
                
                //Danh sách dữ liệu được lưu vào List
                listReportTimelines.Add(
                    new ReportTimeline
                    {
                        rptimeline_IdTaxi = xlsxReportTimeline_IdTaxi,
                        rptimeline_NumberTaxi = xlsxSheet.Cell(row, 2).CachedValue.ToString(),
                        rptimeline_TimeStart = xlsxSheet.Cell(row, 3).CachedValue.ToString(),
                        rptimeline_TimeEnd = xlsxSheet.Cell(row, 4).CachedValue.ToString(),
                        rptimeline_Km = xlsxSheet.Cell(row, 5).CachedValue.ToString(),
                        rptimeline_KmEmpty = xlsxSheet.Cell(row, 6).CachedValue.ToString(),
                        rptimeline_KmTotal = xlsxSheet.Cell(row, 7).CachedValue.ToString(),
                        rptimeline_CostTotal = xlsxSheet.Cell(row, 8).CachedValue.ToString(),
                        rptimeline_PointStart = xlsxSheet.Cell(row, 9).CachedValue.ToString(),
                        rptimeline_PointEnd = xlsxSheet.Cell(row, 10).CachedValue.ToString(),
                    }
                );
            }
            ms.Close();
        }
        return listReportTimelines.ToList();
    }
}
