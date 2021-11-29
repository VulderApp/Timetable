using Newtonsoft.Json;
using StackExchange.Redis;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Infrastructure.Redis.Repositories;

public class TimetableRepository : ITimetableRepository
{
    private IDatabase Timetables { get; }
    
    public TimetableRepository(RedisContext context)
    {
        Timetables = context.Timetables;
    }
    
    public async Task Create(Guid? schoolId, string? className, Optivulcan.Pocos.Timetable timetable)
    {
        await Timetables.StringSetAsync(GetTimetableKey(schoolId, className), JsonConvert.SerializeObject(timetable));
    }

    public async Task<Optivulcan.Pocos.Timetable?> GetTimetableById(Guid? schoolId, string? className)
    {
        var timetable = await Timetables.StringGetAsync(GetTimetableKey(schoolId, className));

        return timetable.ToString() == null ? null : JsonConvert.DeserializeObject<Optivulcan.Pocos.Timetable>(timetable.ToString());
    }

    private static string GetTimetableKey(Guid? schoolId, string? className) => $"{schoolId.ToString()}_{className}";
}