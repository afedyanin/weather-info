namespace WeatherDemo.Domain.Shared.Utils
{
    /// <summary>
    /// https://aske.wachs.dk/06/07/2021/c-conversion-between-unix-timestamps-and-datetime/
    /// </summary>
    public static class UnixTime
    {
        public static DateTime UnixSecondsToDateTime(long timestamp, bool local = false)
        {
            var offset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            return local ? offset.LocalDateTime : offset.UtcDateTime;
        }
        public static  DateTime UnixMillisecondsToDateTime(long timestamp, bool local = false)
        {
            var offset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            return local ? offset.LocalDateTime : offset.UtcDateTime;
        }
        public static long DateTimeToUnixSeconds(DateTime time)
        {
            return ((DateTimeOffset)time).ToUnixTimeSeconds();
        }
        public static long DateTimeToUnixMilliseconds(DateTime time)
        {
            return ((DateTimeOffset)time).ToUnixTimeMilliseconds();
        }
    }
}
