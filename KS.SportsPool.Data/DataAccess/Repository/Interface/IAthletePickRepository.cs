using KS.SportsPool.Data.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KS.SportsPool.Data.DataAccess.Repository.Interface
{
    public interface IAthletePickRepository : IDataRepository<AthletePick>
    {
        /// <summary>
        /// Returns all of the Athlete Picks for the provided Pool Entry.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>All of the Athlete Picks for the provided Pool Entry.</returns>
        Task<IEnumerable<AthletePick>> ListForEntry(int id);

        /// <summary>
        /// Deletes all of the Athlete Picks for an entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteForEntry(int id);
    }
}
