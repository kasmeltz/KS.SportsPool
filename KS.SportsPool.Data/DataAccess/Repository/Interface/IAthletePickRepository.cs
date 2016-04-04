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
        /// <returns>All of the Athlete Picks for the provided Pool Entry.</returns>
        Task<IEnumerable<AthletePick>> ListForEntry(int id);
    }
}
