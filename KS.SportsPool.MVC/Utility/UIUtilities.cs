using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.SportsPool.MVC.Utility
{
    public class UIUtilities
    {
        public static string SiteTitle
        {
            get
            {
                int year = DateTime.Now.Year;
                return (year - 1) + "-" + year + " NHL Playoff Pool";
            }
        }
    }
}
