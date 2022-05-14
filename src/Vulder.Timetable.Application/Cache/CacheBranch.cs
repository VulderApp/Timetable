using Optivulcan;
using Vulder.Timetable.Core.ProjectAggregate.Branch;
using Vulder.Timetable.Infrastructure.Api;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Application.Cache;

public class CacheBranch
{
    public static async Task<BranchCache> Create(IBranchRepository branchRepository, Guid schoolId)
    {
        var schoolModel = await SchoolApi.GetSchoolModel(schoolId);
        var newSchoolBranches = await OptivulcanApi.GetBranches(schoolModel.TimetableUrl!);

        var branchCache = new BranchCache
        {
            Branches = newSchoolBranches
        }.CreateTimestamp();

        if (branchCache.Branches == null) throw new Exception("Branches is null");

        await branchRepository.Create(schoolId, branchCache);

        return branchCache;
    }
}