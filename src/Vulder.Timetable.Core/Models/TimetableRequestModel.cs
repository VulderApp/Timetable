using MediatR;

namespace Vulder.Timetable.Core.Models;

public class TimetableRequestModel : IRequest<Optivulcan.Pocos.Timetable>
{
    public Guid? SchoolId { get; set; }
    public string? Class { get; set; }
    public string? ShortPath { get; set; }
}