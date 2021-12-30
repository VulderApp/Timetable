using System.Text.Json;
using StackExchange.Redis;
using Vulder.Timetable.Core.ProjectAggregate.Branch;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Infrastructure.Redis.Repositories;

public class BranchRepository : IBranchRepository
{
    public BranchRepository(RedisContext context)
    {
        Branches = context.Branches;
    }

    private IDatabase Branches { get; }

    public async Task Create(Guid schoolId, BranchCache branch)
    {
        await Branches.StringSetAsync(schoolId.ToString(), JsonSerializer.Serialize(branch));
    }

    public async Task<BranchCache?> GetBranchById(Guid schoolId)
    {
        try
        {
            var branch = await Branches.StringGetAsync(schoolId.ToString());

            return branch.ToString() == null ? null : JsonSerializer.Deserialize<BranchCache>(branch.ToString());
        }
        catch (JsonException)
        {
            return null;
        }
    }
}