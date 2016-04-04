using KS.SportsPool.Component.Caching.Implementation;
using KS.SportsPool.Component.Caching.Interface;
using KS.SportsPool.Data.DataAccess.Repository.Implementation;
using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KS.SportsPool.Data.Test.DataAccess.Repository.Implementation
{
    public class RepositoryTestHelper
    {
        public static IRepositoryCollection Collection()
        {
            ICacheProvider cacheProvider = new NoOpCacheProvider();
            IRepositoryCollection collection =
                new DapperRepositoryCollection(cacheProvider);
            return collection;
        }


        /// <summary>
        /// Prepares for a data acccess test by starting with a completely empty database.
        /// </summary>
        public static void PrepareForTest()
        {
            IRepositoryCollection collection = Collection();

            IAthletePickRepository AthletePicks = collection.AthletePicks();
            IAthleteRepository Athletes = collection.Athletes();
            IPoolEntryRepository PoolEntries = collection.PoolEntries();
            ITeamPickRepository TeamPicks = collection.TeamPicks();
            ITeamRepository Teams = collection.Teams();

            AthletePicks.PurgeForTest().Wait();
            TeamPicks.PurgeForTest().Wait();
            PoolEntries.PurgeForTest().Wait();
            Athletes.PurgeForTest().Wait();            
            Teams.PurgeForTest().Wait();
        }


        /// <summary>
        /// Asserts the values of the properties for the two objects
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void AssertProperties(object expected, object actual, params string[] toSkip)
        {
            Type ot = actual.GetType();
            PropertyInfo[] expectedProps = expected.GetType().GetProperties();
            foreach (PropertyInfo prop in expectedProps)
            {
                if (toSkip.Contains(prop.Name))
                {
                    continue;
                }

                PropertyInfo other = ot.GetProperty(prop.Name);
                if (other == null)
                {
                    Assert.Fail("Missing property: " + prop.Name);
                }

                object v1 = null;
                object v2 = null;

                try
                {
                    v1 = prop.GetValue(expected, null);
                    v2 = other.GetValue(actual, null);
                }
                catch
                {
                    Assert.Fail(prop.Name);
                }

                Assert.AreEqual(v1, v2, prop.Name);
            }
        }
        
        public static IEnumerable<Athlete> InsertAthletes()
        {
            IRepositoryCollection collection = Collection();
            IAthleteRepository athleteRepository = collection.Athletes();
            IEnumerable<Athlete> existing = athleteRepository.List().Result;
            if (existing.Count() > 0)
            {
                return existing;
            }

            IEnumerable<Team> teams = InsertTeams();

            List<Athlete> athletes = new List<Athlete>();

            athletes.Add(new Athlete
            {
                TeamId = teams.ElementAt(0).Id,
                GroupName = "West Forwards 1",
                FirstName = "Wayne",
                LastName = "Gretzky",
                Position = "C",
                Goals = 91,
                Assists = 115
            });

            athletes.Add(new Athlete
            {
                TeamId = teams.ElementAt(1).Id,
                GroupName = "East Defence 2",
                FirstName = "Bobby",
                LastName = "Orr",
                Position = "LD",
                Goals = 45,
                Assists = 87
            });

            athletes.Add(new Athlete
            {
                TeamId = teams.ElementAt(2).Id,
                GroupName = "East Forwards 1",
                FirstName = "Mario",
                LastName = "Lemiuex",
                Position = "C",
                Goals = 66,
                Assists = 110                
            });

            athletes.Add(new Athlete
            {
                TeamId = teams.ElementAt(3).Id,
                GroupName = "West Forwards 1",
                FirstName = "Mark",
                LastName = "Messier",
                Position = "C",
                Goals = 40,
                Assists = 60
            });

            foreach (Athlete athlete in athletes)
            {
                athleteRepository
                    .Insert(athlete)
                    .Wait();
            }

            return athletes.OrderBy(ath => ath.LastName)
                .ThenBy(ath => ath.FirstName);
        }
       
        public static IEnumerable<Team> InsertTeams()
        {
            IRepositoryCollection collection = Collection();
            ITeamRepository teamRepository = collection.Teams();
            IEnumerable<Team> existing = teamRepository.List().Result;
            if (existing.Count() > 0)
            {
                return existing;
            }

            List<Team> teams = new List<Team>();

            teams.Add(new Team
            {
                Name = "Calgary Flames",
                Abbreviation = "CGY",
                Conference = "West",
                Division = "Pacific",
                Round1 = 15,
                Round2 = 0,
                Round3 = 0,
                Round4 = 0
            });

            teams.Add(new Team
            {
                Name = "Vanouver Canucks",
                Abbreviation = "VAN",
                Conference = "West",
                Division = "Pacific",
                Round1 = 15,
                Round2 = 30,
                Round3 = 45,
                Round4 = 0
            });

            teams.Add(new Team
            {
                Name = "Washington Capitals",
                Abbreviation = "WSH",
                Conference = "East",
                Division = "Metropolitan",
                Round1 = 15,
                Round2 = 30,
                Round3 = 45,
                Round4 = 60
            });

            teams.Add(new Team
            {
                Name = "Chicago Blackhawks",
                Abbreviation = "CHI",
                Conference = "West",
                Division = "Central",
                Round1 = 15,
                Round2 = 30,
                Round3 = 0,
                Round4 = 0
            });

            foreach (Team team in teams)
            {
                teamRepository
                    .Insert(team)
                    .Wait();
            }

            return teams.OrderBy(leg => leg.Name);
        }

        public static IEnumerable<PoolEntry> InsertPoolEntries()
        {            
            IRepositoryCollection collection = Collection();
            IPoolEntryRepository poolEntryRepository = collection.PoolEntries();
            IEnumerable<PoolEntry> existing = poolEntryRepository.List().Result;
            if (existing.Count() > 0)
            {
                return existing;
            }

            List<PoolEntry> poolEntries = new List<PoolEntry>();

            poolEntries.Add(new PoolEntry
            {
                Name = "Bilbo Baggins",
                Telephone = "18076213659",
                Email = "kasmeltz@lakeheadu.ca",
                Score = 23
            });

            poolEntries.Add(new PoolEntry
            {
                Name = "Bryan Smeltzer",
                Telephone = null,
                Email = null,
                Score = 99
            });

            poolEntries.Add(new PoolEntry
            {
                Name = "Darren Smeltzer",
                Telephone = "18076213659",
                Email = "",
                Score = 103
            });

            poolEntries.Add(new PoolEntry
            {
                Name = "Jim Bob",
                Telephone = "",
                Email = "p",
                Score = 203
            });

            foreach (PoolEntry poolEntry in poolEntries)
            {
                poolEntryRepository
                    .Insert(poolEntry)
                    .Wait();
            }

            return poolEntries.OrderBy(leg => leg.Name);
        }

        public static IEnumerable<AthletePick> InsertAthletePicks()
        {
            IRepositoryCollection collection = Collection();
            IAthletePickRepository athletePickRepository = collection.AthletePicks();
            IEnumerable<AthletePick> existing = athletePickRepository.List().Result;
            if (existing.Count() > 0)
            {
                return existing;
            }

            IEnumerable<Athlete> athletes = InsertAthletes();
            IEnumerable<PoolEntry> poolEntries = InsertPoolEntries();

            List<AthletePick> athletePicks = new List<AthletePick>();

            athletePicks.Add(new AthletePick
            {
                AthleteId = athletes.ElementAt(0).Id,
                PoolEntryId = poolEntries.ElementAt(0).Id,
            });

            athletePicks.Add(new AthletePick
            {
                AthleteId = athletes.ElementAt(2).Id,
                PoolEntryId = poolEntries.ElementAt(0).Id,
            });

            athletePicks.Add(new AthletePick
            {
                AthleteId = athletes.ElementAt(1).Id,
                PoolEntryId = poolEntries.ElementAt(1).Id,
            });

            athletePicks.Add(new AthletePick
            {
                AthleteId = athletes.ElementAt(3).Id,
                PoolEntryId = poolEntries.ElementAt(2).Id,
            });

            foreach (AthletePick athletePick in athletePicks)
            {
                athletePickRepository
                    .Insert(athletePick)
                    .Wait();
            }

            return athletePicks.OrderBy(leg => leg.PoolEntryId).ThenBy(leg => leg.AthleteId);
        }

        public static IEnumerable<TeamPick> InsertTeamPicks()
        {
            IRepositoryCollection collection = Collection();
            ITeamPickRepository teamPickRepository = collection.TeamPicks();
            IEnumerable<TeamPick> existing = teamPickRepository.List().Result;
            if (existing.Count() > 0)
            {
                return existing;
            }

            IEnumerable<Team> teams = InsertTeams();
            IEnumerable<PoolEntry> poolEntries = InsertPoolEntries();

            List<TeamPick> teamPicks = new List<TeamPick>();

            teamPicks.Add(new TeamPick
            {
                PoolEntryId = poolEntries.ElementAt(0).Id,
                TeamId = teams.ElementAt(0).Id,
                Round = 1
            });

            teamPicks.Add(new TeamPick
            {
                PoolEntryId = poolEntries.ElementAt(0).Id,
                TeamId = teams.ElementAt(1).Id,
                Round = 1
            });

            teamPicks.Add(new TeamPick
            {
                PoolEntryId = poolEntries.ElementAt(1).Id,
                TeamId = teams.ElementAt(1).Id,
                Round = 2
            });

            teamPicks.Add(new TeamPick
            {
                PoolEntryId = poolEntries.ElementAt(2).Id,
                TeamId = teams.ElementAt(3).Id,
                Round = 3
            });

            foreach (TeamPick teamPick in teamPicks)
            {
                teamPickRepository
                    .Insert(teamPick)
                    .Wait();
            }

            return teamPicks.OrderBy(leg => leg.PoolEntryId).ThenBy(leg => leg.TeamId);
        }
    }
}
