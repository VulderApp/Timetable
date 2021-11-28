using Optivulcan.Pocos;

namespace Vulder.Timetable.Infrastructure.Redis.Interfaces;

public interface IBranchRepository
{
    Task Create(Guid schoolId, List<Branch> branch);
    Task<List<Branch>?> GetBranchById(Guid schoolId);
}