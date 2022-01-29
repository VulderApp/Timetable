using MediatR;
using Vulder.Timetable.Application.Cache;
using Vulder.Timetable.Core.Models;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Application.Branch;

public class BranchesRequestHandler : IRequestHandler<BranchesRequestModel, List<Optivulcan.Pocos.Branch>?>
{
    private readonly IBranchRepository _branchRepository;

    public BranchesRequestHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<List<Optivulcan.Pocos.Branch>?> Handle(BranchesRequestModel request,
        CancellationToken cancellationToken)
    {
        var branchesFromCache = await _branchRepository.GetBranchById(request.SchoolId);
        if (branchesFromCache != null && branchesFromCache.ExpiredAt < DateTimeOffset.Now)
            return branchesFromCache.Branches;

        var newBranches = await CacheBranch.Create(_branchRepository, request.SchoolId);

        return newBranches.Branches;
    }
}