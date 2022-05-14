using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vulder.Timetable.Core.Models;

namespace Vulder.Timetable.Api.Controllers.Timetable;

[ApiController]
[Route("/timetable")]
public class TimetableController : ControllerBase
{
    private readonly IMediator _mediator;

    public TimetableController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTimetable([FromQuery] Guid schoolId, [FromQuery] string shortPath)
    {
        var className = await _mediator.Send(new ResolveClassRequestModel
        {
            SchoolId = schoolId,
            Path = shortPath
        });

        var timetable = await _mediator.Send(new TimetableRequestModel
        {
            SchoolId = schoolId,
            Class = className,
            ShortPath = shortPath
        });

        return Ok(timetable);
    }
}