using MediatR;
using Optivulcan.Pocos;

namespace Vulder.Timetable.Core.Models;

public class BranchesRequestModel : IRequest<List<Branch>?>
{
    public Guid SchoolId { get; set; }
}