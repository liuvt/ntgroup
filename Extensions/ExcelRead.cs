using ntgroup.Data.Entities;
using Microsoft.JSInterop;
using ClosedXML.Excel;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;
namespace ntgroup.Extensions;
public class ExcelRead
{
    public List<Contract> Contracts = new List<Contract>();
    // Export for clips
    public void ExcelReading()
    {
        string fileName = "./wwwroot/templates/contracts-customer.xlsx";
        //Vị trí tệp tin được lưu
        var workbook = new XLWorkbook(fileName);
        //Lấy sheet đầu tiên trong workbook
        var xlsxSheet = workbook.Worksheet(1);
        //Đếm tổng số dòng, thông qua dòng cuối cùng được sử dụng
        int totalRow = xlsxSheet.LastRowUsed().RowNumber(); 
        // Vòng lặp bắt đầu Row 2
        for (int row = 2; row <= totalRow; row++)
        {
            //Danh sách dữ liệu được lưu vào List
            Contracts.Add(
                new Contract{
                    //ID được lấy tại: sheet được chọn + ô(dòng 2, cột 1) lấy giá trị
                    contract_Id = xlsxSheet.Cell(row,1).CachedValue.ToString(),
                    //contract_Date = xlsxSheet.Cell(row,2).CachedValue.ToString(),
                    contract_Time = xlsxSheet.Cell(row,3).CachedValue.ToString(),
                    contract_IdTaxi = xlsxSheet.Cell(row,4).CachedValue.ToString(),
                    contract_StartPoint = xlsxSheet.Cell(row,5).CachedValue.ToString(),
                    contract_EndPoint = xlsxSheet.Cell(row,6).CachedValue.ToString(),
                    contract_Kilometer = xlsxSheet.Cell(row,7).CachedValue.ToString(),
                    contract_KeyTime = xlsxSheet.Cell(row,8).CachedValue.ToString(),
                    contract_Price = xlsxSheet.Cell(row,9).CachedValue.ToString(),
                    contract_DealPrice = xlsxSheet.Cell(row,10).CachedValue.ToString(),
                    contract_Catalog = xlsxSheet.Cell(row,11).CachedValue.ToString(),
                    contract_Tip = xlsxSheet.Cell(row,12).CachedValue.ToString(),
                    contract_TaxiGetTip = xlsxSheet.Cell(row,13).CachedValue.ToString(),
                    contract_PeopleStart = xlsxSheet.Cell(row,14).CachedValue.ToString(),
                    contract_Type = xlsxSheet.Cell(row,15).CachedValue.ToString(),
                    contract_Status = xlsxSheet.Cell(row,16).CachedValue.ToString(),
                    contract_Note = xlsxSheet.Cell(row,17).CachedValue.ToString()
                }
            );
        }
    }
}