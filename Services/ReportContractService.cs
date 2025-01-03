using ClosedXML.Excel;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.ErrorMessage;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class ReportContractService : IReportContractService
{
    public async Task<List<ReportContract>> GetsByExcel(IBrowserFile _file)
    {
        List<ReportContract> listReportContracts = new List<ReportContract>();
        //long maxFileSize = 1024 * 1024 * 5; // max 5MB
        var readStream = _file.OpenReadStream(_file.Size);
        var sizefile = new byte[readStream.Length];

        using (var ms = new MemoryStream(sizefile))
        {
            await readStream.CopyToAsync(ms);
            //var buffer = Convert.ToBase64String(ms.ToArray()); //Chuyển stream thành Base64

            //Đọc file trực tiếp từ bộ nhớ MemoryStream
            using var workbook = new XLWorkbook(ms);
            //Lấy sheet đầu tiên trong workbook
            var xlsxSheet = workbook.Worksheet(1);

            //Kiểm tra đúng template nếu không trả exception
            if(xlsxSheet.FirstCell().CachedValue.ToString() != ExcelNotification.REPORT_CONTRACTS_KEY)
            {
                throw new Exception(ExcelNotification.ERROR_FORMAT_FILE);
            }

            //Đếm tổng số dòng, thông qua dòng cuối cùng được sử dụng
            int totalRow = xlsxSheet.RowsUsed().Count();

            // Vòng lặp bắt đầu Row 2
            for (int row = 2; row <= totalRow; row++)
            {
                // Trong vòng lập kiểm tra xem cột mã hợp đồng nếu rỗng thì ngưng load dữ liệu;
                var xlsxReportcontract_IdTaxi = xlsxSheet.Cell(row, 1).CachedValue.ToString();
                if (xlsxReportcontract_IdTaxi == string.Empty)
                {
                    break;
                }
                //Danh sách dữ liệu được lưu vào List
                listReportContracts.Add(
                    new ReportContract
                    {
                        reportcontract_IdTaxi = xlsxReportcontract_IdTaxi,
                        reportcontract_NumberTaxi = xlsxSheet.Cell(row, 2).CachedValue.ToString(),
                        reportcontract_NameTaxi = xlsxSheet.Cell(row, 3).CachedValue.ToString(),
                        reportcontract_TimeStart = xlsxSheet.Cell(row, 4).CachedValue.ToString(),
                        reportcontract_TimeEnd = xlsxSheet.Cell(row, 5).CachedValue.ToString(),
                        reportcontract_Km = xlsxSheet.Cell(row, 6).CachedValue.ToString(),
                        reportcontract_KmEmpty = xlsxSheet.Cell(row, 7).CachedValue.ToString(),
                        reportcontract_KmTotal = xlsxSheet.Cell(row, 8).CachedValue.ToString(),
                        reportcontract_TimeWait = xlsxSheet.Cell(row, 9).CachedValue.ToString(),
                        reportcontract_CostWait = xlsxSheet.Cell(row, 10).CachedValue.ToString(),
                        reportcontract_CostTotal = xlsxSheet.Cell(row, 11).CachedValue.ToString(),
                        reportcontract_CostTaking = xlsxSheet.Cell(row, 12).CachedValue.ToString(),
                        reportcontract_CostTakingType = xlsxSheet.Cell(row, 13).CachedValue.ToString(),
                        reportcontract_PointStart = xlsxSheet.Cell(row, 14).CachedValue.ToString(),
                        reportcontract_PointEnd = xlsxSheet.Cell(row, 15).CachedValue.ToString(),
                    }
                );
            }
            ms.Close();
        }
        return listReportContracts.ToList();
    }
}
