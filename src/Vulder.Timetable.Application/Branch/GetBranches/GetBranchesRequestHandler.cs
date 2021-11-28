using MediatR;
using Optivulcan;
using Vulder.Timetable.Core.Models;
using Vulder.Timetable.Infrastructure.Api;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Application.Branch.GetBranches;

public class GetBranchesRequestHandler : IRequestHandler<GetBranchesRequestModel, List<Optivulcan.Pocos.Branch>?>
{
    private readonly IBranchRepository _branchRepository;

    public GetBranchesRequestHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<List<Optivulcan.Pocos.Branch>?> Handle(GetBranchesRequestModel request,
        CancellationToken cancellationToken)
    {
        var branchesFromCache = await _branchRepository.GetBranchById(request.SchoolId);
        if (branchesFromCache != null) return branchesFromCache;

        var schoolModel = await SchoolApi.GetSchoolModel(request.SchoolId);
        var newSchoolBranches = await Api.GetBranchListAsync(schoolModel.TimetableUrl);
        await _branchRepository.Create(request.SchoolId, newSchoolBranches);

        return newSchoolBranches;
    }
}