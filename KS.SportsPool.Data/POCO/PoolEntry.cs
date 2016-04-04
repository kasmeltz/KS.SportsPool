using System.Collections.Generic;

namespace KS.SportsPool.Data.POCO
{
    public class PoolEntry : PocoDataObject
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }

        public IEnumerable<AthletePick> AthletePicks { get; set; }
        public IEnumerable<TeamPick> Round1Teams { get; set; }
        public IEnumerable<TeamPick> Round2Teams { get; set; }
        public IEnumerable<TeamPick> Round3Teams { get; set; }
        public IEnumerable<TeamPick> Round4Teams { get; set; }
    }
}