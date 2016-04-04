using KS.SportsPool.Component.Caching.Interface;
using KS.SportsPool.Data.DataAccess.Repository.Interface;

namespace KS.SportsPool.Data.DataAccess.Repository.Implementation
{
    public class DapperRepositoryCollection : IRepositoryCollection
    {
        protected ICacheProvider CacheProvider { get; set; }

        public DapperRepositoryCollection(ICacheProvider cacheProvider)
        {
            CacheProvider = cacheProvider;
        }
       
        public IAthletePickRepository AthletePicks()
        {
            return new DapperAthletePickRepository(CacheProvider);
        }

        public IAthleteRepository Athletes()
        {
            return new DapperAthleteRepository(CacheProvider);
        }

        public IPoolEntryRepository PoolEntries()
        {
            return new DaperPoolEntryRepository(CacheProvider);
        }

        public ITeamPickRepository TeamPicks()
        {
            return new DapperTeamPicksRepository(CacheProvider);
        }

        public ITeamRepository Teams()
        {
            return new DapperTeamRepository(CacheProvider);
        }
    }
}
