using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vulder.Timetable.Core.Models;

namespace Vulder.Timetable.Api.Controllers.Timetable;

[ApiController]
[Route("/timetable/[controller]")]
public class GetTimetableController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTimetableController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTimetable([FromQuery] Guid schoolId, [FromQuery] string className,
        [FromQuery] string shortPath)
    {
        var timetable = await _mediator.Send(new GetTimetableRequestModel
        {
            SchoolId = schoolId,
            ClassName = className,
            ShortPath = shortPath
        });

        return Ok(timetable);
    }
}