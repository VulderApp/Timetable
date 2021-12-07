using Vulder.Timetable.Core.ProjectAggregate.Timetable;

namespace Vulder.Timetable.Infrastructure.Redis.Interfaces;

public interface ITimetableRepository
{
    Task Create(Guid? schoolId, string? className, TimetableCache timetable);
    Task<TimetableCache?> GetTimetableById(Guid? schoolId, string? className);
}