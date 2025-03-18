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
            var result = input.Replace(",","").Replace(".","").Trim();
            // Chuyển chuỗi thành số
            decimal amount = decimal.Parse(input);

            if(amount == 0 && input == "0")
            {
                return string.Format(string.Format(new CultureInfo("vi-VN"), "{0:N0}", amount),"{0:N0}", amount); // Không có chữ đ phía sau
            }

            // Lấy thông tin
            CultureInfo culture = new CultureInfo(cultureCode);
            
            // Định dạng số thành tiền tệ
            return string.Format(culture, "{0:C}", amount); // Có chữ đ phía sau
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
            var result = input.Replace(",","").Replace(".","").Trim();
            // Chuyển chuỗi thành số
            decimal amount = decimal.Parse(result);

            // Định dạng số thành tiền tệ
            return string.Format(string.Format(new CultureInfo("vi-VN"), "{0:N0}", amount),"{0:N0}", amount);  // Không có chữ đ phía sau
        }
        catch (FormatException)
        {
            return "Dữ liệu không hợp lệ.";
        }
    }
}