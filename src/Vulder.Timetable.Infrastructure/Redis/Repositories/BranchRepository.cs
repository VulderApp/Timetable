using Newtonsoft.Json;
using Optivulcan.Pocos;
using StackExchange.Redis;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Infrastructure.Redis.Repositories;

public class BranchRepository : IBranchRepository
{
    private IDatabase Branches { get; }
    
    public BranchRepository(RedisContext context)
    {
        Branches = context.Branches;
    }

    public async Task Create(Guid schoolId, List<Branch> branch)
    {
        await Branches.StringSetAsync(schoolId.ToString(), JsonConvert.SerializeObject(branch));
    }

    public async Task<List<Branch>?> GetBranchById(Guid schoolId)
    {
        var branch = await Branches.StringGetAsync(schoolId.ToString());

        return branch.ToString() == null ? null : JsonConvert.DeserializeObject<List<Branch>>(branch.ToString());
    }
}