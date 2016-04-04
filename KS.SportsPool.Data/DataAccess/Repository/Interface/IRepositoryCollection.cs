namespace KS.SportsPool.Data.DataAccess.Repository.Interface
{
    /// <summary>
    /// Represents the collection of repositories used in the data store for the application.
    /// </summary>
    public interface IRepositoryCollection
    {
        IAthleteRepository Athletes();
        IPoolEntryRepository PoolEntries();
        ITeamRepository Teams();
    }
}
