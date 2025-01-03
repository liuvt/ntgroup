using ClosedXML.Excel;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;

namespace ntgroup.Services;

public class CustomerService : ICustomerService
{
    public async Task<List<Customer>> GetsByExcel(IBrowserFile _file, string _customerType)
    {
        List<Customer> listCustomers = new List<Customer>();
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

            //Đếm tổng số dòng, thông qua dòng cuối cùng được sử dụng
            int totalRow = xlsxSheet.RowsUsed().Count();
            // Vòng lặp bắt đầu Row 2
            for (int row = 2; row <= totalRow; row++)
            {
                // Trong vòng lập kiểm tra xem cột mã hợp đồng nếu rỗng thì ngưng load dữ liệu;
                var xlsxCustomerID = xlsxSheet.Cell(row, 2).CachedValue.ToString();
                if (xlsxCustomerID == string.Empty) break;
                
                //Danh sách dữ liệu được lưu vào List
                listCustomers.Add(
                    new Customer
                    {
                        //Truyền tham số tay
                        customer_Type = _customerType,
                        //Lấy dữ liệu từ ô B2 (row 2, column 2)
                        customer_Id = xlsxCustomerID,
                        customer_Phone = xlsxSheet.Cell(row, 3).CachedValue.ToString(),
                        customer_Name = xlsxSheet.Cell(row, 4).CachedValue.ToString(),
                        customer_Status = xlsxSheet.Cell(row, 5).CachedValue.ToString(),
                        customer_Kilometer = xlsxSheet.Cell(row, 6).CachedValue.ToString(),
                        customer_PhoneTaxi = xlsxSheet.Cell(row, 7).CachedValue.ToString(),
                        customer_IdTaxi = xlsxSheet.Cell(row, 8).CachedValue.ToString(),
                        customer_Price = xlsxSheet.Cell(row, 9).CachedValue.ToString(),
                        customer_Point = xlsxSheet.Cell(row, 10).CachedValue.ToString(),
                        customer_DateTime = xlsxSheet.Cell(row, 11).CachedValue.ToString(),
                    }
                );
            }
            ms.Close();
        }
        return listCustomers.ToList();
    }
}
