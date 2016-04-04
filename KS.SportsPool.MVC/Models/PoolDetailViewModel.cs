using KS.SportsPool.Data.POCO;
using System.Collections.Generic;

namespace KS.SportsPool.MVC.Models
{
    public class PoolViewModel
    {
        public IEnumerable<Athlete> Athletes { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public PoolEntry Entry { get; set; }
        public IEnumerable<AthletePick> AthletePicks { get; set; }
        public IEnumerable<TeamPick> TeamPicks { get; set; }
    }
}
