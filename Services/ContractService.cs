using System.Globalization;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.ErrorMessage;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class ContractService : IContractService
{
    public async Task<List<Contract>> GetsByExcel(IBrowserFile _file)
    {
        List<Contract> listContracts = new List<Contract>();
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
            if(xlsxSheet.FirstCell().CachedValue.ToString() != ExcelNotification.CONTRACTS_KEY)
            {
                throw new Exception(ExcelNotification.ERROR_FORMAT_FILE);
            }

            //Đếm tổng số dòng, thông qua dòng cuối cùng được sử dụng
            int totalRow = xlsxSheet.RowsUsed().Count();

            //Convert datetime
            var dutchCultureInfo = CultureInfo.CreateSpecificCulture("vi-VN");

            // Vòng lặp bắt đầu Row 2
            for (int row = 2; row <= totalRow; row++)
            {
                // Trong vòng lập kiểm tra xem cột mã hợp đồng nếu rỗng thì ngưng load dữ liệu;
                var xlsxContactID = xlsxSheet.Cell(row, 1).CachedValue.ToString();
                if (xlsxContactID == null || xlsxContactID == "")
                {
                    break;
                }
                //Danh sách dữ liệu được lưu vào List
                listContracts.Add(
                    new Contract
                    {
                        //ID được lấy tại: sheet được chọn + ô(dòng 2, cột 1) lấy giá trị
                        contract_Id = xlsxContactID,
                        contract_Date = DateTime.Parse(xlsxSheet.Cell(row, 2).CachedValue.ToString(), dutchCultureInfo),
                        contract_Time = xlsxSheet.Cell(row, 3).CachedValue.ToString(),
                        contract_IdTaxi = xlsxSheet.Cell(row, 4).CachedValue.ToString(),
                        contract_StartPoint = xlsxSheet.Cell(row, 5).CachedValue.ToString(),
                        contract_EndPoint = xlsxSheet.Cell(row, 6).CachedValue.ToString(),
                        contract_Kilometer = xlsxSheet.Cell(row, 7).CachedValue.ToString(),
                        contract_KeyTime = xlsxSheet.Cell(row, 8).CachedValue.ToString(),
                        contract_Price = xlsxSheet.Cell(row, 9).CachedValue.ToString(),
                        contract_DealPrice = xlsxSheet.Cell(row, 10).CachedValue.ToString(),
                        contract_Catalog = xlsxSheet.Cell(row, 11).CachedValue.ToString(),
                        contract_Tip = xlsxSheet.Cell(row, 12).CachedValue.ToString(),
                        contract_TaxiGetTip = xlsxSheet.Cell(row, 13).CachedValue.ToString(),
                        contract_PeopleStart = xlsxSheet.Cell(row, 14).CachedValue.ToString(),
                        contract_Type = xlsxSheet.Cell(row, 15).CachedValue.ToString(),
                        contract_Status = xlsxSheet.Cell(row, 16).CachedValue.ToString(),
                        contract_Note = xlsxSheet.Cell(row, 17).CachedValue.ToString()
                    }
                );
            }
            ms.Close();
        }
        return listContracts.ToList();
    }
}
