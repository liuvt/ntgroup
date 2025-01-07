using System.Globalization;
namespace ntgroup.Extensions;

//Tính tổng giá tiền của 1 list, với kiểu string
public static class SumString
{

    // Value là danh sách, namefields là tên cột cần tính tổng
    public static string SumListString(List<object> value, string namefields)
    {
        // Sum the values of all contracts, assuming each object has a 'Price' property.
        var totalList = value.Sum(e =>
        {
            // Lấy tên cột
            var fieldsNameToSum = e.GetType().GetProperty(namefields)?.GetValue(e)?.ToString();
            
            // Sau khi định dang tiền là hàng nghìn (.), thì decimal hiểu đó là phần thập phân (,) dẫn đến mất các số 0 phía sau dấu (.)
            // Do đó cần thay đổi dâu (.) thành dấu (,) để decimal hiểu được đâu là phần thập phân, đâu là hàng nghìn 
            // Trim(): Loại bỏ khoảng trắng thừa
            fieldsNameToSum = fieldsNameToSum?.Replace(".", ",").Trim();
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
}