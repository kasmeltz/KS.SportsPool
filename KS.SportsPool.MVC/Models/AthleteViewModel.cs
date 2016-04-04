using KS.SportsPool.Data.POCO;
using System.Collections.Generic;

namespace KS.SportsPool.MVC.Models
{
    public class AthleteViewModel
    {
        public Athlete Athlete { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}
