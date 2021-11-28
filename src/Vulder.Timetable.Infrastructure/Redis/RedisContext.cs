using StackExchange.Redis;
using Vulder.Timetable.Core;

namespace Vulder.Timetable.Infrastructure.Redis;

public class RedisContext
{
    public IDatabase Branches { get; }
    
    public RedisContext()
    {
        var redis = ConnectionMultiplexer.Connect(Constants.RedisConnectionString);
        Branches = redis.GetDatabase(0);
    }
}