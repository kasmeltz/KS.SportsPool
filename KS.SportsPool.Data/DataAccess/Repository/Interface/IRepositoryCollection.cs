namespace KS.SportsPool.Data.DataAccess.Repository.Interface
{
    /// <summary>
    /// Represents the collection of repositories used in the data store for the application.
    /// </summary>
    public interface IRepositoryCollection
    {
        IAthletePickRepository AthletePicks();
        IAthleteRepository Athletes();        
        IPoolEntryRepository PoolEntries();
        ITeamPickRepository TeamPicks();
        ITeamRepository Teams();
    }
}
