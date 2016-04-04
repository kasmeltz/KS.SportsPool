using KS.SportsPool.Data.POCO;
using System.Collections.Generic;

namespace KS.SportsPool.MVC.Models
{
    public class PoolListViewModel
    {
        public IEnumerable<Athlete> Athletes { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<PoolEntry> Entries { get; set; }
    }
}
