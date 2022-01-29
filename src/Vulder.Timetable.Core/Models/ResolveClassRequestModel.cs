using MediatR;

namespace Vulder.Timetable.Core.Models;

public class ResolveClassRequestModel : IRequest<string>
{
    public Guid SchoolId { get; set; }
    public string? Path { get; set; }
}