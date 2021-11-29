using MediatR;

namespace Vulder.Timetable.Core.Models;

public class GetTimetableRequestModel : IRequest<Optivulcan.Pocos.Timetable>
{
    public Guid? SchoolId { get; set; }
    public string? ClassName { get; set; }
    public string? ShortPath { get; set; }
}