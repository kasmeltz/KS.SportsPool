using KS.SportsPool.Data.POCO;
using System.Collections.Generic;

namespace KS.SportsPool.MVC.Models
{
    public class AthleteListViewModel
    {
        public IEnumerable<Athlete> Athletes { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}
