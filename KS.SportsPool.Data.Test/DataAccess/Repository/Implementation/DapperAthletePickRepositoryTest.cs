using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KS.SportsPool.Data.Test.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Unit Tests for the DapperAthletePickRepository
    /// </summary>
    [TestClass]
    public class DapperAthletePickRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            RepositoryTestHelper.PrepareForTest();
        }

        [TestMethod]
        public void AthletePickRepository()
        {
            IRepositoryCollection collection = RepositoryTestHelper.Collection();
            IAthletePickRepository athletePickRepository = collection.AthletePicks();
            IEnumerable<AthletePick> insertedAthletePicks = RepositoryTestHelper.InsertAthletePicks();
            IEnumerable<AthletePick> listedAthletePicks = athletePickRepository.List().Result;

            IEnumerable<Athlete> athletes = RepositoryTestHelper.InsertAthletes();
            IEnumerable<PoolEntry> poolEntries = RepositoryTestHelper.InsertPoolEntries();

            for (int i = 0; i < listedAthletePicks.Count(); i++)
            {
                RepositoryTestHelper
                    .AssertProperties(insertedAthletePicks.ElementAt(i),
                        listedAthletePicks.ElementAt(i));
            }

            bool exceptionThrown = false;
            try
            {
                athletePickRepository.Insert(insertedAthletePicks.ElementAt(1)).Wait();
            }
            catch (AggregateException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            listedAthletePicks.ElementAt(2).AthleteId = athletes.ElementAt(2).Id;
            listedAthletePicks.ElementAt(2).PoolEntryId = poolEntries.ElementAt(1).Id;
            athletePickRepository.Update(listedAthletePicks.ElementAt(2)).Wait();

            AthletePick updatedAthletePick = athletePickRepository.Get(listedAthletePicks.ElementAt(2).Id).Result;
            Assert.AreEqual(athletes.ElementAt(2).Id, updatedAthletePick.AthleteId);
            Assert.AreEqual(poolEntries.ElementAt(1).Id, updatedAthletePick.PoolEntryId);

            exceptionThrown = false;
            try
            {
                listedAthletePicks.ElementAt(3).AthleteId = athletes.ElementAt(2).Id;
                listedAthletePicks.ElementAt(3).PoolEntryId = poolEntries.ElementAt(1).Id;
                athletePickRepository.Update(listedAthletePicks.ElementAt(3)).Wait();
            }
            catch (AggregateException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            updatedAthletePick = athletePickRepository.Get(listedAthletePicks.ElementAt(3).Id).Result;
            Assert.AreEqual(athletes.ElementAt(3).Id, updatedAthletePick.AthleteId);
            Assert.AreEqual(poolEntries.ElementAt(2).Id, updatedAthletePick.PoolEntryId);

            athletePickRepository.Delete(listedAthletePicks.ElementAt(0).Id).Wait();
            listedAthletePicks = athletePickRepository.List().Result;
            Assert.AreEqual(3, listedAthletePicks.Count());
        }
    }
}

