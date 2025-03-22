
using System.Globalization;

namespace ntgroup.Extensions;
public static class DatetimeExtensions
{
    /// <summary>
    /// Giải thích:
    //     Sử dụng DateTime.ParseExact() để phân tích chuỗi ngày tháng với định dạng "MM/dd/yyyy hh:mm:ss tt".
    //     DateTimeOffset được tạo từ DateTime, có thể đặt TimeSpan.Zero nếu muốn thời gian ở UTC hoặc sử dụng TimeZoneInfo.Local.GetUtcOffset(dateTime) 
    //   để có múi giờ địa phương.
    /// </summary>
    /// <param name="offsetString"></param>
    /// <returns></returns>
    public static DateTimeOffset DateTimeOffsetFromString(string offsetString)
    {
        string format = "dd/MM/yyyy hh:mm:ss tt"; // Định dạng của chuỗi đầu vào
        CultureInfo provider = CultureInfo.InvariantCulture;

        // Chuyển đổi thành DateTime
        DateTime dateTime = DateTime.ParseExact(offsetString, format, provider);

        // Chuyển đổi thành DateTimeOffset với múi giờ UTC hoặc local
        DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.Zero); // UTC
        //DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, TimeZoneInfo.Local.GetUtcOffset(dateTime)); // Local
        return dateTimeOffset;
    }

    /// <summary>
    /// Giải thích:
    //    Chuyển đổi format 
    /// </summary>
    /// <param name="offsetString"></param>
    /// <returns></returns>
    public static DateTime DateTimeFromString(string dateString)
    {
        string format = "dd/MM/yyyy HH:mm:ss"; // Định dạng của chuỗi đầu vào
        CultureInfo provider = CultureInfo.InvariantCulture;
        // Convert to DateTime
        DateTime dateTime = DateTime.ParseExact(dateString, format, provider);
        
        // Convert back to string in 24-hour format
        string formatedDate = dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        
        return dateTime;
    }

    public static string DateTimeFromStringDefault(string dateString)
    {
        //string pickupDateStr = "2025-03-19T23:56:55+0700";

        if (string.IsNullOrWhiteSpace(dateString))
        {
            return string.Empty;
        }

        // Chuyển đổi chuỗi ISO 8601 thành DateTime
        if (DateTimeOffset.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:sszzz", 
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset pickupDate))
        {
            // Định dạng lại thành dd/MM/yyyy HH:mm:ss
            string formattedDate = pickupDate.ToString("dd/MM/yyyy HH:mm:ss");
            return formattedDate;
        }
        else
        {
            return string.Empty;
        }
    }
}