using System.Collections.Generic;

namespace KS.SportsPool.Data.POCO
{
    public class PoolEntry : PocoDataObject
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public int Rank { get; set; }
        public IEnumerable<AthletePick> AthletePicks { get; set; }
        public IEnumerable<TeamPick> TeamPicks { get; set; }
    }
}