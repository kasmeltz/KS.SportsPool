﻿using KS.SportsPool.Data.POCO;
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

            IEnumerable<Team> teams = await Repository
                .Teams()
                .List(DateTime.Now.Year);

            return View(teams);
        }

        public async Task<ActionResult> DeleteTeam(int id)
        {
            IEnumerable<Team> teams = null;

            try
            {
                await Repository.Teams().Delete(id);
                teams = await Repository
                    .Teams()
                    .List(DateTime.Now.Year);
                @ViewBag.Success = "The team was deleted successfully!";
                return View("Teams", teams);
            }
            catch (Exception ex)
            {
                teams = await Repository
                    .Teams()
                    .List(DateTime.Now.Year);
                @ViewBag.Error = "There was an error deleting the team!";
                return View("Teams", teams);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddTeam(Team teamToAdd)
        {
            IEnumerable<Team> teams = null;

            try
            {
                teamToAdd.Year = DateTime.Now.Year;
                await Repository.Teams().Insert(teamToAdd);
                teams = await Repository
                    .Teams()
                    .List(DateTime.Now.Year);
                @ViewBag.Success = "The team was added successfully!";
                return View("Teams", teams);
            }
            catch (Exception ex)
            {
                teams = await Repository
                    .Teams()
                    .List(DateTime.Now.Year);
                @ViewBag.Error = "There was an error adding the team!";
                return View("Teams", teams);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateTeam(Team teamToUpdate)
        {
            IEnumerable<Team> teams = null;

            try
            {
                teamToUpdate.Year = DateTime.Now.Year;
                await Repository.Teams().Update(teamToUpdate);
                @ViewBag.Success = "The team was updated successfully!";
                teams = await Repository
                    .Teams()
                    .List(DateTime.Now.Year);
                return View("Teams", teams);
            }
            catch (Exception ex)
            {
                @ViewBag.Error = "There was an error updating the team!";
                await Repository.Teams().Update(teamToUpdate);
                teams = await Repository
                    .Teams()
                    .List(DateTime.Now.Year);
                return View("Teams", teams);                
            }
        }

        public async Task<ActionResult> Athletes()
        {
            ViewBag.Title = UIUtilities.SiteTitle;

            IEnumerable<Team> teams = await Repository
                .Teams()
                .List(DateTime.Now.Year);

            IEnumerable<Athlete> athletes = await Repository
               .Athletes()
               .List(DateTime.Now.Year);

            return View(new AthleteListViewModel { Athletes = athletes, Teams = teams });
        }


        [HttpPost]
        public async Task<ActionResult> AddAthlete(Athlete athleteToAdd)
        {
            IEnumerable<Team> teams = await Repository
                .Teams()
                .List(DateTime.Now.Year);
                            
            IEnumerable<Athlete> athletes = null;

            try
            {
                athleteToAdd.Year = DateTime.Now.Year;
                await Repository.Athletes().Insert(athleteToAdd);
                athletes = await Repository
                    .Athletes()
                    .List(DateTime.Now.Year);
                @ViewBag.Success = "The athlete was added successfully!";
                return PartialView("AthleteList", new AthleteListViewModel { Athletes = athletes, Teams = teams });
            }
            catch (Exception ex)
            {
                athletes = await Repository
                    .Athletes()
                    .List(DateTime.Now.Year);
                @ViewBag.Error = "There was an error adding the athlete!";
                return PartialView("AthleteList", new AthleteListViewModel { Athletes = athletes, Teams = teams });
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAthlete(Athlete athleteToUpdate)
        {
            try
            {
                athleteToUpdate.Year = DateTime.Now.Year;
                await Repository.Athletes().Update(athleteToUpdate);
                return Content("The athlete was saved successfully");
            }
            catch (Exception ex)
            {
                return Content("There was an error updating the athlete!");
            }
        }

        public ActionResult Pools()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            return View();
        }
    }
}