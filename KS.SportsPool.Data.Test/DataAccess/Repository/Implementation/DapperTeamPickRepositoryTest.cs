using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KS.SportsPool.Data.Test.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Unit Tests for the DapperTeamPickRepository
    /// </summary>
    [TestClass]
    public class DapperTeamPickRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            RepositoryTestHelper.PrepareForTest();
        }

        [TestMethod]
        public void TeamPickRepository()
        {
            IRepositoryCollection collection = RepositoryTestHelper.Collection();
            ITeamPickRepository teamPickRepository = collection.TeamPicks();
            IEnumerable<TeamPick> insertedTeamPicks = RepositoryTestHelper.InsertTeamPicks();
            IEnumerable<TeamPick> listedTeamPicks = teamPickRepository.List(DateTime.Now.Year).Result;

            IEnumerable<Team> teams = RepositoryTestHelper.InsertTeams();
            IEnumerable<PoolEntry> poolEntries = RepositoryTestHelper.InsertPoolEntries();

            for (int i = 0; i < listedTeamPicks.Count(); i++)
            {
                RepositoryTestHelper
                    .AssertProperties(insertedTeamPicks.ElementAt(i),
                        listedTeamPicks.ElementAt(i));
            }

            bool exceptionThrown = false;
            try
            {
                teamPickRepository.Insert(insertedTeamPicks.ElementAt(1)).Wait();
            }
            catch (AggregateException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            listedTeamPicks.ElementAt(2).TeamId = teams.ElementAt(2).Id;
            listedTeamPicks.ElementAt(2).PoolEntryId = poolEntries.ElementAt(1).Id;
            listedTeamPicks.ElementAt(2).Round = 5;
            teamPickRepository.Update(listedTeamPicks.ElementAt(2)).Wait();

            TeamPick updatedTeamPick = teamPickRepository.Get(listedTeamPicks.ElementAt(2).Id).Result;
            Assert.AreEqual(teams.ElementAt(2).Id, updatedTeamPick.TeamId);
            Assert.AreEqual(poolEntries.ElementAt(1).Id, updatedTeamPick.PoolEntryId);
            Assert.AreEqual(5, updatedTeamPick.Round);

            exceptionThrown = false;
            try
            {
                listedTeamPicks.ElementAt(3).TeamId = teams.ElementAt(2).Id;
                listedTeamPicks.ElementAt(3).PoolEntryId = poolEntries.ElementAt(1).Id;
                listedTeamPicks.ElementAt(3).Round = 5;
                teamPickRepository.Update(listedTeamPicks.ElementAt(3)).Wait();
            }
            catch (AggregateException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            updatedTeamPick = teamPickRepository.Get(listedTeamPicks.ElementAt(3).Id).Result;
            Assert.AreEqual(teams.ElementAt(3).Id, updatedTeamPick.TeamId);
            Assert.AreEqual(poolEntries.ElementAt(2).Id, updatedTeamPick.PoolEntryId);
            Assert.AreEqual(3, updatedTeamPick.Round);

            teamPickRepository.Delete(listedTeamPicks.ElementAt(0).Id).Wait();
            listedTeamPicks = teamPickRepository.List(DateTime.Now.Year).Result;
            Assert.AreEqual(3, listedTeamPicks.Count());

            listedTeamPicks = teamPickRepository.List(2012).Result;
            Assert.AreEqual(0, listedTeamPicks.Count());
        }
    }
}

