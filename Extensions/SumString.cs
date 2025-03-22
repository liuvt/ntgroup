using System.Globalization;
namespace ntgroup.Extensions;

//Tính tổng giá tiền của 1 list, với kiểu string
public static class SumString
{

    // Value là danh sách, namefields là tên cột cần tính tổng
    public static string SumListString<T>(List<T> value, string namefields)
    {
        // Sum the values of all contracts, assuming each object has a 'Price' property.
        var totalList = value.Sum(e =>
        {
            // Lấy tên cột
            var fieldsNameToSum = e.GetType().GetProperty(namefields)?.GetValue(e)?.ToString() ?? "0";

            // Sau khi định dang tiền là hàng nghìn (.), thì decimal hiểu đó là phần thập phân (,) dẫn đến mất các số 0 phía sau dấu (.)
            // Do đó cần thay đổi dâu (.) thành dấu (,) để decimal hiểu được đâu là phần thập phân, đâu là hàng nghìn 
            // Trim(): Loại bỏ khoảng trắng thừa
            fieldsNameToSum = fieldsNameToSum?.Replace(".", "").Replace(",", "").Trim();
            // Try to parse the price string to decimal
            if (decimal.TryParse(fieldsNameToSum, out decimal price))
            {
                return price; // If parsing is successful, return the price
            }
            else
            {
                return 0; // If parsing fails or Price is invalid, return 0
            }
        });

        // Trả về tiền tệ kiểu string
        return FormatCurrency.formatCurrency(totalList.ToString());
    }

    // Tính tổng tiền 2 số kiểu string
    public static string SumDoubleString(string str1, string str2)
    {
        // Xử lý chuỗi và chuyển đổi thành số decimal
        if (decimal.TryParse(str1.Replace(".", "").Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal num1) &&
            decimal.TryParse(str2.Replace(".", "").Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal num2))
        {
            // Trả về tiền tệ kiểu string
            return FormatCurrency.formatCurrency((num1 + num2).ToString());
        }
        else
        {
            throw new FormatException("Chuỗi nhập vào không hợp lệ!");
        }

    }

    //Cộng 2 chuỗi trả về một double type
    public static double ReturnDouble(string meters1, string meters2)
    {
        // Chuẩn hóa dấu phân cách thập phân về dấu chấm (.)
        meters1 = meters1.Replace(",", ".");
        meters2 = meters2.Replace(",", ".");

        // Xử lý chuỗi và chuyển đổi thành số 
        if (double.TryParse(meters1, NumberStyles.Any, CultureInfo.InvariantCulture, out double num1) &&
            double.TryParse(meters2, NumberStyles.Any, CultureInfo.InvariantCulture, out double num2))
        {
            return Math.Round((num1 + num2) / 1000, 2);
        }
        else
        {
            return 0.00;
        }

    }

    // Chuyển đổi Meters to Km
    public static double ReturnDouble(string meters)
    {
        if(string.IsNullOrWhiteSpace(meters)) return 0.00;

        // Chuẩn hóa dấu phân cách thập phân
        meters = meters.Replace(",", "."); 

        if (double.TryParse(meters, NumberStyles.Any, CultureInfo.InvariantCulture, out double _meters))
        {
            return Math.Round(_meters / 1000, 2);
        }
        else
        {
            return 0.00;
        }
    }

    // Helper method to safely convert and sum string values
    public static string SumFields(List<string> fields)
    {
        decimal sum = 0;
        foreach (var field in fields)
        {
            if (decimal.TryParse(field, out decimal value))
            {
                sum += value;
            }
        }
        return FormatCurrency.formatCurrency(sum.ToString());
    }
}