using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KS.SportsPool.Data.Test.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Unit Tests for the DapperAthleteRepository
    /// </summary>
    [TestClass]
    public class DapperAthleteRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            RepositoryTestHelper.PrepareForTest();
        }

        [TestMethod]
        public void AthleteRepository()
        {
            IRepositoryCollection collection = RepositoryTestHelper.Collection();
            IAthleteRepository athleteRepository = collection.Athletes();
            IEnumerable<Athlete> insertedAthletes = RepositoryTestHelper.InsertAthletes();
            IEnumerable<Athlete> listedAthletes = athleteRepository.List(DateTime.Now.Year).Result;
            IEnumerable<Team> teams = RepositoryTestHelper.InsertTeams();

            for (int i = 0; i < listedAthletes.Count(); i++)
            {
                RepositoryTestHelper
                    .AssertProperties(insertedAthletes.ElementAt(i),
                        listedAthletes.ElementAt(i));
            }

            bool exceptionThrown = false;
            try
            {
                athleteRepository.Insert(insertedAthletes.ElementAt(1)).Wait();
            }
            catch (AggregateException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            listedAthletes.ElementAt(2).TeamId = teams.ElementAt(3).Id;
            listedAthletes.ElementAt(2).GroupName = "Group 2";
            listedAthletes.ElementAt(2).FirstName = "Somewhere";
            listedAthletes.ElementAt(2).LastName = "Country";
            listedAthletes.ElementAt(2).Position = "Doggy";
            listedAthletes.ElementAt(2).Goals = 450;
            listedAthletes.ElementAt(2).Assists = 1290;
            athleteRepository.Update(listedAthletes.ElementAt(2)).Wait();

            Athlete updatedAthlete = athleteRepository.Get(listedAthletes.ElementAt(2).Id).Result;
            Assert.AreEqual(teams.ElementAt(3).Id, updatedAthlete.TeamId);
            Assert.AreEqual("Group 2", updatedAthlete.GroupName);
            Assert.AreEqual("Somewhere", updatedAthlete.FirstName);
            Assert.AreEqual("Country", updatedAthlete.LastName);
            Assert.AreEqual("Doggy", updatedAthlete.Position);
            Assert.AreEqual(450, updatedAthlete.Goals);
            Assert.AreEqual(1290, updatedAthlete.Assists);

            exceptionThrown = false;
            try
            {
                listedAthletes.ElementAt(3).FirstName = "Somewhere";
                listedAthletes.ElementAt(3).LastName = "Country";
                athleteRepository.Update(listedAthletes.ElementAt(3)).Wait();
            }
            catch (AggregateException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            updatedAthlete = athleteRepository.Get(listedAthletes.ElementAt(3).Id).Result;
            Assert.AreEqual("Bobby", updatedAthlete.FirstName);
            Assert.AreEqual("Orr", updatedAthlete.LastName);

            athleteRepository.Delete(listedAthletes.ElementAt(0).Id).Wait();
            listedAthletes = athleteRepository.List(DateTime.Now.Year).Result;
            Assert.AreEqual(3, listedAthletes.Count());

            IEnumerable<Athlete> searched = athleteRepository.Search("Mario").Result;
            Assert.AreEqual(1, searched.Count());
            Assert.AreEqual("Lemiuex", searched.ElementAt(0).LastName);

            searched = athleteRepository.Search("Lemiuex").Result;            
            Assert.AreEqual(1, searched.Count());
            Assert.AreEqual("Mario", searched.ElementAt(0).FirstName);

            listedAthletes = athleteRepository.List(2012).Result;
            Assert.AreEqual(0, listedAthletes.Count());
        }
    }
}
