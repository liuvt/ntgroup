using System.Globalization;
namespace ntgroup.Extensions;

//Tính tổng giá tiền của 1 list, với kiểu string
public static class SumTotalListString
{

    // Value là danh sách, namefields là tên cột cần tính tổng
    public static string SumTotalPrices(List<object> value, string namefields)
    {
        // Sum the values of all contracts, assuming each object has a 'Price' property.
        var totalList = value.Sum(e =>
        {
            // Use reflection to get the Fields property value
            var fieldsNameToSum = e.GetType().GetProperty(namefields)?.GetValue(e)?.ToString();

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


}