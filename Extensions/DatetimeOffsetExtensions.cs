
using System.Globalization;

namespace ntgroup.Extensions;
public static class DatetimeOffsetExtensions
{
    /// <summary>
    /// Giải thích:
    //     Sử dụng DateTime.ParseExact() để phân tích chuỗi ngày tháng với định dạng "MM/dd/yyyy hh:mm:ss tt".
    //     DateTimeOffset được tạo từ DateTime, có thể đặt TimeSpan.Zero nếu muốn thời gian ở UTC hoặc sử dụng TimeZoneInfo.Local.GetUtcOffset(dateTime) 
    //   để có múi giờ địa phương.
    /// </summary>
    /// <param name="offsetString"></param>
    /// <returns></returns>
    public static DateTimeOffset FromString(string offsetString)
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
}