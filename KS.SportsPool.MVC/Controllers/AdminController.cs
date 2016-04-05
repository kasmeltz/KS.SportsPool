using KS.SportsPool.Data.POCO;
using KS.SportsPool.MVC.Models;
using KS.SportsPool.MVC.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KS.SportsPool.MVC.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            return View();
        }

        public async Task<ActionResult> Teams()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            ViewBag.Success = TempData["Success"];
            ViewBag.Error = TempData["Error"];
            ViewBag.ScrollSection = TempData["ScrollSection"];

            IEnumerable<Team> teams = await Repository
                .Teams()
                .List(DateTime.Now.Year);

            return View(teams);
        }

        public async Task<ActionResult> DeleteTeam(int id)
        {
            try
            {
                await Repository.Teams().Delete(id);
                TempData["Success"] = "The team was deleted successfully!";
                return RedirectToAction("Teams");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error deleting the team!";
                return RedirectToAction("Teams");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddTeam(Team teamToAdd)
        {
            try
            {
                teamToAdd.Year = DateTime.Now.Year;
                await Repository.Teams().Insert(teamToAdd);
                TempData["Success"] = "The team '" + teamToAdd.Name + "' was added successfully!";
                return RedirectToAction("Teams");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error adding the team '" + teamToAdd.Name + "'!";
                return RedirectToAction("Teams");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateTeam(Team teamToUpdate)
        {
            try
            {
                teamToUpdate.Year = DateTime.Now.Year;
                await Repository.Teams().Update(teamToUpdate);
                TempData["Success"] = "The team '" + teamToUpdate.Name + "' was updated successfully!";
                TempData["ScrollSection"] = "TeamDiv" + teamToUpdate.Id;
                return RedirectToAction("Teams");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error updating the team '" + teamToUpdate.Name + "'!";
                TempData["ScrollSection"] = "TeamDiv" + teamToUpdate.Id;
                return RedirectToAction("Teams");
            }
        }

        public async Task<ActionResult> Athletes()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            ViewBag.Success = TempData["Success"];
            ViewBag.Error = TempData["Error"];
            ViewBag.ScrollSection = TempData["ScrollSection"];

            IEnumerable<Team> teams = await Repository
                .Teams()
                .List(DateTime.Now.Year);

            IEnumerable<Athlete> athletes = await Repository
               .Athletes()
               .List(DateTime.Now.Year);

            return View(new AthleteListViewModel { Athletes = athletes, Teams = teams });
        }

        public async Task<ActionResult> DeleteAthlete(int id)
        {
            try
            {
                await Repository.Athletes().Delete(id);
                TempData["Success"] = "The athlete was deleted successfully!";
                return RedirectToAction("Athletes");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error deleting the athlete!";
                return RedirectToAction("Athletes");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAthlete(Athlete athleteToAdd)
        {
            try
            {
                athleteToAdd.Year = DateTime.Now.Year;
                await Repository.Athletes().Insert(athleteToAdd);
                TempData["Success"] = "The athlete '" + athleteToAdd.FirstName + " " + athleteToAdd.LastName + "' was added successfully!";
                return RedirectToAction("Athletes");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error adding the athlete '" + athleteToAdd.FirstName + " " + athleteToAdd.LastName + "'!";
                return RedirectToAction("Athletes");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAthlete(Athlete athleteToUpdate)
        {
            try
            {
                athleteToUpdate.Year = DateTime.Now.Year;
                await Repository.Athletes().Update(athleteToUpdate);
                TempData["Success"] = "The athlete '" + athleteToUpdate.FirstName + " " + athleteToUpdate.LastName + "' was updated successfully!";
                TempData["ScrollSection"] = "AthleteDiv" + athleteToUpdate.Id;
                return RedirectToAction("Athletes");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error updating the athlete '" + athleteToUpdate.FirstName + " " + athleteToUpdate.LastName + "'!";
                TempData["ScrollSection"] = "AthleteDiv" + athleteToUpdate.Id;
                return RedirectToAction("Athletes");
            }
        }

        public async Task<ActionResult> Pools()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            ViewBag.Success = TempData["Success"];
            ViewBag.Error = TempData["Error"];
            ViewBag.ScrollSection = TempData["ScrollSection"];

            IEnumerable<PoolEntry> pools = await Repository
                .PoolEntries()
                .List(DateTime.Now.Year);

            return View(pools);
        }

        public async Task<ActionResult> DeletePool(int id)
        {
            try
            {
                await Repository.PoolEntries().Delete(id);
                TempData["Success"] = "The pool was deleted successfully!";
                return RedirectToAction("Pools");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error deleting the pool!";
                return RedirectToAction("Pools");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddPool(PoolEntry poolToAdd)
        {
            try
            {
                poolToAdd.Year = DateTime.Now.Year;
                await Repository.PoolEntries().Insert(poolToAdd);
                TempData["Success"] = "The pool for '" + poolToAdd.Name + "' was added successfully!";
                TempData["ScrollSection"] = "PoolDiv" + poolToAdd.Id;
                return RedirectToAction("Pools");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error adding the pool for '" + poolToAdd.Name + "'!";
                return RedirectToAction("Pools");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePool(PoolEntry poolToUpdate)
        {
            try
            {
                poolToUpdate.Year = DateTime.Now.Year;
                await Repository.PoolEntries().Update(poolToUpdate);
                TempData["Success"] = "The pool for '" + poolToUpdate.Name + "' was updated successfully!";
                TempData["ScrollSection"] = "PoolDiv" + poolToUpdate.Id;
                return RedirectToAction("Pools");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error updating the pool for '" + poolToUpdate.Name + "'!";
                TempData["ScrollSection"] = "PoolDiv" + poolToUpdate.Id;
                return RedirectToAction("Pools");
            }
        }

        public async Task<ActionResult> PoolDetails(int id)
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            ViewBag.Success = TempData["Success"];
            ViewBag.Error = TempData["Error"];
            ViewBag.ScrollSection = TempData["ScrollSection"];

            PoolEntry entry = await Repository
                .PoolEntries()
                .Get(id);

            IEnumerable<Team> teams = await Repository
                .Teams()
                .List(DateTime.Now.Year);

            IEnumerable<Athlete> athletes = await Repository
               .Athletes()
               .List(DateTime.Now.Year);

            IEnumerable<AthletePick> athletePicks = await Repository
                .AthletePicks()
                .ListForEntry(id);

            IEnumerable<TeamPick> teamPicks = await Repository
               .TeamPicks()
               .ListForEntry(id);

            return View(new PoolViewModel
            {
                Entry = entry,
                Athletes = athletes,
                Teams = teams,
                AthletePicks = athletePicks,
                TeamPicks = teamPicks
            });
        }

        protected async Task CreateTeamPicks(int entryId, int round, IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                if (id == 0)
                {
                    continue;
                }

                TeamPick teamPick = new TeamPick();
                teamPick.Round = round;
                teamPick.PoolEntryId = entryId;
                teamPick.Year = DateTime.Now.Year;
                teamPick.TeamId = id;
                await Repository.TeamPicks()
                    .Insert(teamPick);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PoolDetails(PoolViewModel model)
        {
            try
            {
                await Repository.AthletePicks()
                    .DeleteForEntry(model.Entry.Id);

                foreach (AthletePick athletePick in model.AthletePicks)
                {
                    athletePick.PoolEntryId = model.Entry.Id;
                    athletePick.Year = DateTime.Now.Year;
                    await Repository.AthletePicks()
                        .Insert(athletePick);
                }

                await Repository.TeamPicks()
                    .DeleteForEntry(model.Entry.Id);

                await CreateTeamPicks(model.Entry.Id, 1, model.SelectedTeamsRound1);
                await CreateTeamPicks(model.Entry.Id, 2, model.SelectedTeamsRound2);
                await CreateTeamPicks(model.Entry.Id, 3, model.SelectedTeamsRound3);
                await CreateTeamPicks(model.Entry.Id, 4, model.SelectedTeamsRound4);

                TempData["Success"] = "The pool picks have been updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Their was an error updating the pool picks";
            }

            return RedirectToAction("PoolDetails", new { id = model.Entry.Id });
        }

        public async Task<ActionResult> UpdateScores()
        {
            try
            {
                IEnumerable<PoolEntry> entries = await Repository
                    .PoolEntries()
                    .List(DateTime.Now.Year);

                IEnumerable<Team> teams = await Repository
                    .Teams()
                    .List(DateTime.Now.Year);

                IEnumerable<Athlete> athletes = await Repository
                   .Athletes()
                   .List(DateTime.Now.Year);

                IEnumerable<AthletePick> athletePicks = await Repository
                    .AthletePicks()
                    .List(DateTime.Now.Year);

                IEnumerable<TeamPick> teamPicks = await Repository
                   .TeamPicks()
                   .List(DateTime.Now.Year);

                Dictionary<int, int> poolScores = new Dictionary<int, int>();
                Dictionary<int, int> athleteScores = new Dictionary<int, int>();
                Dictionary<Tuple<int, int>, int> teamScores = new Dictionary<Tuple<int, int>, int>();

                foreach (Athlete athlete in athletes)
                {
                    athleteScores[athlete.Id] = athlete.Goals + athlete.Assists;
                }

                foreach (Team team in teams)
                {
                    Tuple<int, int> key;
                    key = new Tuple<int, int>(team.Id, 1);
                    teamScores[key] = team.Round1;
                    key = new Tuple<int, int>(team.Id, 2);
                    teamScores[key] = team.Round2;
                    key = new Tuple<int, int>(team.Id, 3);
                    teamScores[key] = team.Round3;
                    key = new Tuple<int, int>(team.Id, 4);
                    teamScores[key] = team.Round4;
                }

                foreach (PoolEntry entry in entries)
                {
                    poolScores[entry.Id] = 0;
                }

                foreach (AthletePick athletePick in athletePicks)
                {
                    poolScores[athletePick.PoolEntryId] += athleteScores[athletePick.AthleteId];
                }

                foreach (TeamPick teamPick in teamPicks)
                {
                    Tuple<int, int> key;
                    key = new Tuple<int, int>(teamPick.TeamId, teamPick.Round);
                    poolScores[teamPick.PoolEntryId] += teamScores[key];
                }

                foreach (PoolEntry entry in entries)
                {
                    entry.Score = poolScores[entry.Id];
                    await Repository.PoolEntries().Update(entry);
                }

                TempData["Success"] = "The pool scores have been updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error updating the pool scores!";
            }

            return RedirectToAction("Pools");
        }
    }
}