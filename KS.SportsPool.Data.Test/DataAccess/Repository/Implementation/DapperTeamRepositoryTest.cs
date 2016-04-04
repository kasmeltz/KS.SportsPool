using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KS.SportsPool.Data.Test.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Unit Tests for the DapperTeamRepository
    /// </summary>
    [TestClass]
    public class DapperTeamRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            RepositoryTestHelper.PrepareForTest();
        }

        [TestMethod]
        public void TeamRepository()
        {
            IRepositoryCollection collection = RepositoryTestHelper.Collection();
            ITeamRepository teamRepository = collection.Teams();
            IEnumerable<Team> insertedTeams = RepositoryTestHelper.InsertTeams();
            IEnumerable<Team> listedTeams = teamRepository.List().Result;

            for (int i = 0; i < listedTeams.Count(); i++)
            {
                RepositoryTestHelper
                    .AssertProperties(insertedTeams.ElementAt(i),
                        listedTeams.ElementAt(i));
            }

            bool exceptionThrown = false;
            try
            {
                teamRepository.Insert(insertedTeams.ElementAt(1)).Wait();
            }
            catch (AggregateException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            listedTeams.ElementAt(2).Name = "New Team Name";
            listedTeams.ElementAt(2).Abbreviation = "NAB";
            listedTeams.ElementAt(2).Conference = "Conf 2";
            listedTeams.ElementAt(2).Division = "Division 4";
            listedTeams.ElementAt(2).Round1 = 11;
            listedTeams.ElementAt(2).Round2 = 22;
            listedTeams.ElementAt(2).Round3 = 33;
            listedTeams.ElementAt(2).Round4 = 44;
            teamRepository.Update(listedTeams.ElementAt(2)).Wait();

            Team updatedTeam = teamRepository.Get(listedTeams.ElementAt(2).Id).Result;
            Assert.AreEqual("New Team Name", updatedTeam.Name);
            Assert.AreEqual("NAB", updatedTeam.Abbreviation);
            Assert.AreEqual("Conf 2", updatedTeam.Conference);
            Assert.AreEqual("Division 4", updatedTeam.Division);
            Assert.AreEqual(11, updatedTeam.Round1);
            Assert.AreEqual(22, updatedTeam.Round2);
            Assert.AreEqual(33, updatedTeam.Round3);
            Assert.AreEqual(44, updatedTeam.Round4);

            exceptionThrown = false;
            try
            {
                listedTeams.ElementAt(3).Name = "New Team Name";
                teamRepository.Update(listedTeams.ElementAt(3)).Wait();
            }
            catch (AggregateException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            updatedTeam = teamRepository.Get(listedTeams.ElementAt(3).Id).Result;
            Assert.AreEqual("Washington Capitals", updatedTeam.Name);
            
            teamRepository.Delete(listedTeams.ElementAt(0).Id).Wait();
            listedTeams = teamRepository.List().Result;
            Assert.AreEqual(3, listedTeams.Count());

            IEnumerable<Team> searched = teamRepository.Search("hawk").Result;
            Assert.AreEqual(1, searched.Count());
            Assert.AreEqual("Chicago Blackhawks", searched.ElementAt(0).Name);
        }
    }
}

