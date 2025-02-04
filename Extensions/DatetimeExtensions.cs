
using System.Globalization;

namespace ntgroup.Extensions;
public static class DatetimeExtensions
{
    public static DateTime FromString(string date)
    {
        // Parse the input string into a DateTime object
        var dateTime = DateTime.ParseExact(date, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
        // Parse the input string into a DateTime object
        
        return dateTime;
    }
}