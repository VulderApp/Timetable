namespace Vulder.Timetable.Core.ProjectAggregate.Timetable;

public class TimetableCache
{
    public DateTimeOffset ExpiredAt { get; set; }
    public Optivulcan.Pocos.Timetable? Timetable { get; set; }

    public TimetableCache CreateTimestamp()
    {
        ExpiredAt = DateTimeOffset.Now.AddHours(1);

        return this;
    }
}