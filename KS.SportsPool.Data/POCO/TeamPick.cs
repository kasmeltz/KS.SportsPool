namespace KS.SportsPool.Data.POCO
{
    public class TeamPick : PocoDataObject
    {
        public int TeamId { get; set; }
        public int PoolEntryId { get; set; }
        public int Round { get; set; }
        public Team Team { get; set; }
    }
}
