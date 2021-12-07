using Vulder.Timetable.Core.ProjectAggregate.Branch;

namespace Vulder.Timetable.Infrastructure.Redis.Interfaces;

public interface IBranchRepository
{
    Task Create(Guid schoolId, BranchCache branch);
    Task<BranchCache?> GetBranchById(Guid schoolId);
}