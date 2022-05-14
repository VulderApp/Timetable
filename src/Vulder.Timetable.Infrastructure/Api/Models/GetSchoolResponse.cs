namespace Vulder.Timetable.Infrastructure.Api.Models;

public class GetSchoolResponse
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? TimetableUrl { get; set; }
    public string? SchoolUrl { get; set; }
}