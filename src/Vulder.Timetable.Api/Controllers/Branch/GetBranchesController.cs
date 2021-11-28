using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vulder.Timetable.Core.Models;

namespace Vulder.Timetable.Api.Controllers.Branch;

[ApiController]
[Route("branch/[controller]")]
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
        var schoolBranch = await _mediator.Send(new GetBranchesRequestModel
        {
            SchoolId = schoolId
        });

        return Ok(schoolBranch);
    }
}