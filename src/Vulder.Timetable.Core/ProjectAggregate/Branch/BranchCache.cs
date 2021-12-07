namespace Vulder.Timetable.Core.ProjectAggregate.Branch;

public class BranchCache
{
    public DateTimeOffset ExpiredAt { get; set; }
    public List<Optivulcan.Pocos.Branch>? Branches { get; set; }

    public BranchCache CreateTimestamp()
    {
        ExpiredAt = DateTimeOffset.Now.AddHours(1);

        return this;
    }
}