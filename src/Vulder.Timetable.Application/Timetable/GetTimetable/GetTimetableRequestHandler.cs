using MediatR;
using Optivulcan;
using Vulder.Timetable.Core.Models;
using Vulder.Timetable.Core.ProjectAggregate.Timetable;
using Vulder.Timetable.Infrastructure.Api;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Application.Timetable.GetTimetable;

public class GetTimetableRequestHandler : IRequestHandler<GetTimetableRequestModel, Optivulcan.Pocos.Timetable>
{
    private readonly ITimetableRepository _timetableRepository;

    public GetTimetableRequestHandler(ITimetableRepository timetableRepository)
    {
        _timetableRepository = timetableRepository;
    }

    public async Task<Optivulcan.Pocos.Timetable> Handle(GetTimetableRequestModel request,
        CancellationToken cancellationToken)
    {
        var timetableFromCache = await _timetableRepository.GetTimetableById(request.SchoolId, request.ClassName);
        if (timetableFromCache?.Timetable != null && timetableFromCache.ExpiredAt < DateTimeOffset.Now) return timetableFromCache.Timetable;

        var schoolModel = await SchoolApi.GetSchoolModel(request.SchoolId);
        var newTimetable = await Api.GetTimetableAsync(schoolModel.TimetableUrl + request.ShortPath);
        
        var timetableCache = new TimetableCache
        {
            Timetable = newTimetable
        }.CreateTimestamp();
        
        await _timetableRepository.Create(request.SchoolId, request.ClassName, timetableCache);

        return newTimetable;
    }
}