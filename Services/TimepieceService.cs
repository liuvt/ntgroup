using ClosedXML.Excel;
using Microsoft.AspNetCore.Components.Forms;
using System.Globalization; //Format datetime
using ntgroup.ErrorMessage;
using ntgroup.Services.Interfaces;
using ntgroup.Data.Entities;

namespace ntgroup.Services;

public class TimepieceService : ITimepieceService
{
    public async Task<List<TimepieceDTO>> GetsByExcel(IBrowserFile _file )
    {
        //Khai báo danh sách lưu data từ browser
        List<TimepieceDTO> timepieces = new List<TimepieceDTO>();

        //long maxFileSize = 1024 * 1024 * 5; // max 5MB
        //Đọc file theo dung lượng file từ browser
        var readStream = _file.OpenReadStream(_file.Size);
        //Chuyển về dạng byte để đọc trên stream
        var sizefile = new byte[readStream.Length];
        //Khai báo một Bộ nhớ tạm của stream
        using (var ms = new MemoryStream(sizefile))
        {
            //Chuyển dữ liệu file vào trong bộ nhớ tạm của stream
            await readStream.CopyToAsync(ms);
            //var buffer = Convert.ToBase64String(ms.ToArray()); //Chuyển stream thành Base64 (hiện tại không cần)

            //Đọc file trực tiếp từ bộ nhớ tạm
            using var workbook = new XLWorkbook(ms);
            //Lấy sheet thứ 2 trong workbook: Đối với timepiece là chi tiết báo cáo
            var xlsxSheet = workbook.Worksheet(2);

            //Kiểm tra đúng template nếu không trả exception
            //Trong file báo cáo đồng hồ, ô đầu tiên có tên "BÁO CÁO CUỐC KHÁCH"
            if(xlsxSheet.FirstCell().CachedValue.ToString() != ExcelNotification.TIMEPIECE_KEY_NAME)
            {
                //ERROR_FORMAT_FILE: định dạng file không đúng
                throw new Exception(ExcelNotification.ERROR_FORMAT_FILE);
            }

            //Đếm tổng số dòng, thông qua dòng cuối cùng được sử dụng
            int totalRow = xlsxSheet.RowsUsed().Count();

            //Kiểm tra dòng rỗng, trường hợp quá 2 dòng rỗng xẽ kết thúc totalRow
            int checkEmpty = 0;

            // Vòng lặp bắt đầu Row 2
            for (int row = 6; row <= totalRow; row++)
            {
                //Lấy dữ liệu cột đầu tiền ở dòng 6
                var checkValue = xlsxSheet.Cell(row, 1).CachedValue.ToString();
                //Kiểm tra trạng thái rỗng
                if (checkValue == string.Empty)
                {
                    checkEmpty += 1; //+= tăng 1
                    if (checkEmpty > 1) // nhiều hơn 1 dòng rỗng
                    {
                        break; //Thoát dòng lập
                    }
                    continue; //Quay lại vòng lập
                }
                
                //Danh sách dữ liệu được lưu vào List
                timepieces.Add(
                    new TimepieceDTO
                    {
                        taxi_NumberId = checkValue,
                        taxi_NumberPlate = xlsxSheet.Cell(row, 2).CachedValue.ToString(),
                        tp_TimeStart = DateTime.Parse(xlsxSheet.Cell(row, 3).CachedValue.ToString(), CultureInfo.CreateSpecificCulture("vi-VN")),
                        tp_TimeEnd = DateTime.Parse(xlsxSheet.Cell(row, 4).CachedValue.ToString(), CultureInfo.CreateSpecificCulture("vi-VN")),
                        tp_Kilometer = (int)Math.Round(xlsxSheet.Cell(row, 5).GetValue<float>()),
                        tp_KilometerEmpty = (int)Math.Round(xlsxSheet.Cell(row, 6).GetValue<float>()),
                        tp_KilometerTotal = (int)Math.Round(xlsxSheet.Cell(row, 7).GetValue<float>()),
                        tp_Amount = xlsxSheet.Cell(row, 8).GetValue<decimal>(),
                        tp_StartPoint = xlsxSheet.Cell(row, 9).CachedValue.ToString(),
                        tp_EndPoint = xlsxSheet.Cell(row, 10).CachedValue.ToString(),
                    }
                ); 
            }
            ms.Close();
        }
        return timepieces.ToList();
    }
}
