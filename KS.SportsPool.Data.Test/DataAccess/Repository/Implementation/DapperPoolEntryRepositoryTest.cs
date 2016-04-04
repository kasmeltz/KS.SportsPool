using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KS.SportsPool.Data.Test.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Unit Tests for the DapperPoolEntryRepository
    /// </summary>
    [TestClass]
    public class DapperPoolEntryRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            RepositoryTestHelper.PrepareForTest();
        }

        [TestMethod]
        public void PoolEntryRepository()
        {
            IRepositoryCollection collection = RepositoryTestHelper.Collection();
            IPoolEntryRepository poolEntryRepository = collection.PoolEntries();
            IEnumerable<PoolEntry> insertedPoolEntrys = RepositoryTestHelper.InsertPoolEntries();
            IEnumerable<PoolEntry> listedPoolEntrys = poolEntryRepository.List(DateTime.Now.Year).Result;

            for (int i = 0; i < listedPoolEntrys.Count(); i++)
            {
                RepositoryTestHelper
                    .AssertProperties(insertedPoolEntrys.ElementAt(i),
                        listedPoolEntrys.ElementAt(i));
            }

            bool exceptionThrown = false;
            try
            {
                poolEntryRepository.Insert(insertedPoolEntrys.ElementAt(1)).Wait();
            }
            catch (AggregateException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            listedPoolEntrys.ElementAt(2).Name = "New PoolEntry Name";
            listedPoolEntrys.ElementAt(2).Telephone = "1807POOP";
            listedPoolEntrys.ElementAt(2).Email = "p@P.com";
            listedPoolEntrys.ElementAt(2).Score = 400;
            poolEntryRepository.Update(listedPoolEntrys.ElementAt(2)).Wait();

            PoolEntry updatedPoolEntry = poolEntryRepository.Get(listedPoolEntrys.ElementAt(2).Id).Result;
            Assert.AreEqual("New PoolEntry Name", updatedPoolEntry.Name);
            Assert.AreEqual("1807POOP", updatedPoolEntry.Telephone);
            Assert.AreEqual("p@P.com", updatedPoolEntry.Email);
            Assert.AreEqual(400, updatedPoolEntry.Score);

            exceptionThrown = false;
            try
            {
                listedPoolEntrys.ElementAt(3).Name = "New PoolEntry Name";
                poolEntryRepository.Update(listedPoolEntrys.ElementAt(3)).Wait();
            }
            catch (AggregateException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            updatedPoolEntry = poolEntryRepository.Get(listedPoolEntrys.ElementAt(3).Id).Result;
            Assert.AreEqual("Jim Bob", updatedPoolEntry.Name);
            
            poolEntryRepository.Delete(listedPoolEntrys.ElementAt(0).Id).Wait();
            listedPoolEntrys = poolEntryRepository.List(DateTime.Now.Year).Result;
            Assert.AreEqual(3, listedPoolEntrys.Count());

            IEnumerable<PoolEntry> searched = poolEntryRepository.Search("meltz").Result;
            Assert.AreEqual(1, searched.Count());
            Assert.AreEqual("Bryan Smeltzer", searched.ElementAt(0).Name);

            listedPoolEntrys = poolEntryRepository.List(2012).Result;
            Assert.AreEqual(0, listedPoolEntrys.Count());
        }
    }
}

