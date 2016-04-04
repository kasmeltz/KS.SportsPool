using KS.SportsPool.Data.POCO;
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

        public ActionResult Athletes()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            return View();
        }

        public async Task<ActionResult> Teams()
        {
            ViewBag.Title = UIUtilities.SiteTitle;

            IEnumerable<Team> teams = await Repository
                .Teams()
                .List();

            return View(teams);
        }

        [HttpPost]
        public async Task<ActionResult> AddTeam(Team teamToAdd)
        {
            IEnumerable<Team> teams = null;

            try
            {
                await Repository.Teams().Insert(teamToAdd);
                teams = await Repository
                           .Teams()
                           .List();
                @ViewBag.Success = "The team was added successfully!";
                return PartialView("TeamList", teams);
            }
            catch (Exception ex)
            {
                teams = await Repository
                   .Teams()
                    .List();
                @ViewBag.Error = "There was an error adding the team!";
                return PartialView("TeamList", teams);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateTeam(Team teamToUpdate)
        {
            try
            {
                await Repository.Teams().Update(teamToUpdate);
                return Content("The team was saved successfully");
            }
            catch (Exception ex)
            {
                return Content("There was an error updating the team!");
            }
        }       

        public ActionResult Pools()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            return View();
        }
    }
}