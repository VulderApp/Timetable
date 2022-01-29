using MediatR;
using Vulder.Timetable.Application.Cache;
using Vulder.Timetable.Core.Models;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Application.Classname;

public class ResolveClassRequestHandler : IRequestHandler<ResolveClassRequestModel, string>
{
    private readonly IBranchRepository _branchRepository;

    public ResolveClassRequestHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<string> Handle(ResolveClassRequestModel request, CancellationToken cancellationToken)
    {
        if (request.Path == null)
            throw new Exception("Path param is null");

        var branchesFromCache = await _branchRepository.GetBranchById(request.SchoolId);
        if (branchesFromCache != null && branchesFromCache.ExpiredAt < DateTimeOffset.Now)
            return GetClassFromCollection(branchesFromCache.Branches!, request.Path);

        var branches = await CacheBranch.Create(_branchRepository, request.SchoolId);

        return GetClassFromCollection(branches.Branches!, request.Path);
    }

    private static string GetClassFromCollection(IEnumerable<Optivulcan.Pocos.Branch> branches, string path)
    {
        var branch = branches.FirstOrDefault(x => x.Url == path)?.Name;
        if (branch == null)
            throw new Exception("Couldn't resolve class");

        return branch;
    }
}