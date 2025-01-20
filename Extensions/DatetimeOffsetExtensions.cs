
namespace ntgroup.Extensions;
public static class DatetimeOffsetExtensions
{
    public static DateTimeOffset FromString(string offsetString)
    {

        DateTimeOffset offset;
        if (!DateTimeOffset.TryParse(offsetString, out offset))
        {
            offset = DateTimeOffset.Now;
        }

        return offset;
    }
}