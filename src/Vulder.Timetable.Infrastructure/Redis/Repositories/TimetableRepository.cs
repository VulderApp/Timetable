using Newtonsoft.Json;
using StackExchange.Redis;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Infrastructure.Redis.Repositories;

public class TimetableRepository : ITimetableRepository
{
    public TimetableRepository(RedisContext context)
    {
        Timetables = context.Timetables;
    }

    private IDatabase Timetables { get; }

    public async Task Create(Guid? schoolId, string? className, Optivulcan.Pocos.Timetable timetable)
    {
        await Timetables.StringSetAsync(GetTimetableKey(schoolId, className), JsonConvert.SerializeObject(timetable));
    }

    public async Task<Optivulcan.Pocos.Timetable?> GetTimetableById(Guid? schoolId, string? className)
    {
        var timetable = await Timetables.StringGetAsync(GetTimetableKey(schoolId, className));

        return timetable.ToString() == null
            ? null
            : JsonConvert.DeserializeObject<Optivulcan.Pocos.Timetable>(timetable.ToString());
    }

    private static string GetTimetableKey(Guid? schoolId, string? className)
    {
        return $"{schoolId.ToString()}_{className}";
    }
}