namespace Vulder.Timetable.Core.ProjectAggregate.Timetable;

public class TimetableCache
{
    public long ExpiredAt { get; set; }
    public Optivulcan.Pocos.Timetable? Timetable { get; set; }

    public TimetableCache CreateTimestamp()
    {
        ExpiredAt = DateTimeOffset.Now.AddHours(1).ToUnixTimeMilliseconds();

        return this;
    }
}