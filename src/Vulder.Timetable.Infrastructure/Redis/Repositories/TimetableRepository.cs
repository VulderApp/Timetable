using Newtonsoft.Json;
using StackExchange.Redis;
using Vulder.Timetable.Core.ProjectAggregate.Timetable;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Infrastructure.Redis.Repositories;

public class TimetableRepository : ITimetableRepository
{
    public TimetableRepository(RedisContext context)
    {
        Timetables = context.Timetables;
    }

    private IDatabase Timetables { get; }

    public async Task Create(Guid? schoolId, string? className, TimetableCache timetable)
    {
        await Timetables.StringSetAsync(GetTimetableKey(schoolId, className), JsonConvert.SerializeObject(timetable));
    }

    public async Task<TimetableCache?> GetTimetableById(Guid? schoolId, string? className)
    {
        var timetable = await Timetables.StringGetAsync(GetTimetableKey(schoolId, className));

        return timetable.ToString() == null
            ? null
            : JsonConvert.DeserializeObject<TimetableCache>(timetable.ToString());
    }

    private static string GetTimetableKey(Guid? schoolId, string? className) 
        => $"{schoolId.ToString()}_{className}";
}