using KS.SportsPool.Data.POCO;
using KS.SportsPool.MVC.Models;
using KS.SportsPool.MVC.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KS.SportsPool.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            return View();
        }

        public async Task<ActionResult> Pools()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            string prizeAmounts = ConfigurationManager.AppSettings["PrizeAmounts"];
            string[] prizes = prizeAmounts.Split(',');
            ViewBag.Prizes = prizes;

            IEnumerable<PoolEntry> entries = await Repository.PoolEntries()
                .List(DateTime.Now.Year);

            entries = entries.OrderByDescending(ent => ent.Score);

            int rank = 1;
            foreach(PoolEntry entry in entries) {
                entry.Rank = rank++;
            }

            return View(entries);
        }

        [HttpPost]
        public async Task<ActionResult> PoolEntryDetails(int Id)
        {
            PoolEntry entry = await Repository
                .PoolEntries()
                .Get(Id);

            IEnumerable<Team> teams = await Repository
                .Teams()
                .List(DateTime.Now.Year);

            IEnumerable<Athlete> athletes = await Repository
               .Athletes()
               .List(DateTime.Now.Year);

            IEnumerable<AthletePick> athletePicks = await Repository
                .AthletePicks()
                .ListForEntry(Id);

            foreach(AthletePick athletePick in athletePicks)
            {
                athletePick.Athlete = athletes
                    .Where(ath => ath.Id == athletePick.AthleteId)
                    .FirstOrDefault();          
            }

            IEnumerable<TeamPick> teamPicks = await Repository
               .TeamPicks()
               .ListForEntry(Id);

            foreach (TeamPick teamPick in teamPicks)
            {
                teamPick.Team = teams
                    .Where(ath => ath.Id == teamPick.TeamId)
                    .FirstOrDefault();
            }

            PoolDetailsViewModel model = new PoolDetailsViewModel
            {
                Entry = entry,
                AthletePicks = athletePicks,
                TeamPicks = teamPicks,
            };

            return PartialView("PoolDetails", model);
        }
    }
}