namespace Vulder.Timetable.Infrastructure.Redis.Interfaces;

public interface ITimetableRepository
{
    Task Create(Guid? schoolId, string? className, Optivulcan.Pocos.Timetable timetable);
    Task<Optivulcan.Pocos.Timetable?> GetTimetableById(Guid? schoolId, string? className);
}