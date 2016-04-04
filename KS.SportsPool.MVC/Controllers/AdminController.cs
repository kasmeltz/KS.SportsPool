using KS.SportsPool.Data.POCO;
using KS.SportsPool.MVC.Models;
using KS.SportsPool.MVC.Utility;
using System;
using System.Collections.Generic;
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

            IEnumerable<Team> teams = await Repository
                .Teams()
                .List(DateTime.Now.Year);

            IEnumerable<Athlete> athletes = await Repository
               .Athletes()
               .List(DateTime.Now.Year);

            IEnumerable<PoolEntry> pools = await Repository
                .PoolEntries()
                .List(DateTime.Now.Year);

            return View(new PoolListViewModel { Athletes = athletes, Teams = teams, Entries = pools });
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
    }
}