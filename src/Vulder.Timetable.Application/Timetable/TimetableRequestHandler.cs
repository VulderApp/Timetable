using MediatR;
using Optivulcan;
using Vulder.Timetable.Core.Models;
using Vulder.Timetable.Core.ProjectAggregate.Timetable;
using Vulder.Timetable.Infrastructure.Api;
using Vulder.Timetable.Infrastructure.Redis.Interfaces;

namespace Vulder.Timetable.Application.Timetable;

public class TimetableRequestHandler : IRequestHandler<TimetableRequestModel, Optivulcan.Pocos.Timetable>
{
    private readonly ITimetableRepository _timetableRepository;

    public TimetableRequestHandler(ITimetableRepository timetableRepository)
    {
        _timetableRepository = timetableRepository;
    }

    public async Task<Optivulcan.Pocos.Timetable> Handle(TimetableRequestModel request,
        CancellationToken cancellationToken)
    {
        var timetableFromCache = await _timetableRepository.GetTimetableById(request.SchoolId, request.Class);
        if (timetableFromCache?.Timetable != null && timetableFromCache.ExpiredAt < DateTimeOffset.Now)
            return timetableFromCache.Timetable;

        var schoolModel = await SchoolApi.GetSchoolModel(request.SchoolId);
        var newTimetable = await OptivulcanApi.GetTimetable(schoolModel.TimetableUrl + request.ShortPath);

        var timetableCache = new TimetableCache
        {
            Timetable = newTimetable
        }.CreateTimestamp();

        await _timetableRepository.Create(request.SchoolId, request.Class, timetableCache);

        return newTimetable;
    }
}