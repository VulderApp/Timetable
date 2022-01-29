using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vulder.Timetable.Core.Models;

namespace Vulder.Timetable.Api.Controllers.Branch;

[ApiController]
[Route("/branches")]
public class GetBranchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetBranchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetBranches([FromQuery] Guid schoolId)
    {
        var schoolBranch = await _mediator.Send(new BranchesRequestModel
        {
            SchoolId = schoolId
        });

        return Ok(schoolBranch);
    }
}