namespace Vulder.Timetable.Core;

public static class Constants
{
    public static readonly string BaseApiUrl = Environment.GetEnvironmentVariable("BASE_API_URL") ?? string.Empty;
    public static readonly string RedisConnectionString =
        Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ?? string.Empty;
}