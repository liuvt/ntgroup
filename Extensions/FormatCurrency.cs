using System.Globalization;
namespace ntgroup.Extensions;
   //Format VNĐ
public static class FormatCurrency
{

    //Định dạng tiền có dấu . và đ phía sau số
    public static string formatCurrency(string input, string cultureCode) //cultureCode ="vi-VN"
    {
        try
        {
            
            // Chuyển chuỗi thành số
            decimal amount = decimal.Parse(input);

            if(amount == 0 && input == "0")
            {
                return string.Format("{0:N0}", amount);
            }

            // Lấy thông tin
            CultureInfo culture = new CultureInfo(cultureCode);
            
            // Định dạng số thành tiền tệ
            return string.Format(culture, "{0:C}", amount);
        }
        catch (FormatException)
        {
            return "Dữ liệu không hợp lệ.";
        }
    }

    // Định dạng tiền không có dấu ,
    public static string formatCurrency(string input)
    {
        try
        {
            // Chuyển chuỗi thành số
            decimal amount = decimal.Parse(input);

            // Định dạng số thành tiền tệ
            return string.Format("{0:N0}", amount);
        }
        catch (FormatException)
        {
            return "Dữ liệu không hợp lệ.";
        }
    }
}