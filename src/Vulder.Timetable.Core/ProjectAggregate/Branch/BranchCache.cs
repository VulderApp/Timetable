namespace Vulder.Timetable.Core.ProjectAggregate.Branch;

public class BranchCache
{
    public long ExpiredAt { get; set; }
    public List<Optivulcan.Pocos.Branch>? Branches { get; set; }

    public BranchCache CreateTimestamp()
    {
        ExpiredAt = DateTimeOffset.Now.AddHours(1).ToUnixTimeMilliseconds();

        return this;
    }
}