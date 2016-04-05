using KS.SportsPool.Data.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KS.SportsPool.Data.DataAccess.Repository.Interface
{
    public interface ITeamPickRepository : IDataRepository<TeamPick>
    {
        /// <summary>
        /// Returns all of the Team Picks for the provided Pool Entry.
        /// </summary>
        /// <returns>All of the Team Picks for the provided Pool Entry.</returns>
        Task<IEnumerable<TeamPick>> ListForEntry(int id);

        /// <summary>
        /// Deletes all of the Team Picks for an entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteForEntry(int id);
    }
}
