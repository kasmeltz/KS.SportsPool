using Dapper;
using KS.SportsPool.Component.Caching.Interface;
using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KS.SportsPool.Data.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Provides access to TeamPick data in the data store using the Dapper framework.
    /// 
    /// Dapper documentation at https://github.com/StackExchange/dapper-dot-net
    /// </summary>
    public class DapperTeamPickRepository : BaseDapperRepository<TeamPick>,
        ITeamPickRepository
    {    
        public DapperTeamPickRepository(ICacheProvider cacheProvider)
            : base(cacheProvider)
        {
            CacheContainerName = "TeamPick";
            TableName = "[app].[TeamPick]";
            CacheSeconds = 0;
        }

        protected override void CreateSql()
        {
            GetSql = _getSql;
            ListSql = _listSql;
            InsertSql = _insertSql;
            UpdateSql = _updateSql;
        }

        private const string _forEntry = @"
             SET NOCOUNT ON;
            SELECT  
                Id, TeamId, PoolEntryId, Year, Round
            FROM 
                [app].[TeamPick]
            WHERE
                PoolEntryId = @EntryId";

        public async Task<IEnumerable<TeamPick>> ListForEntry(int id)
        {
            return await List(0, _forEntry, new
            {
                EntryId = id,
            }, "TeamForEntry" + id);
        }

        public async Task DeleteForEntry(int id)
        {
            using (SqlConnection connection =
             SqlConnectionManager.GetConnection(DbConnectionString))
            {
                await connection.ExecuteAsync(@"
                SET NOCOUNT ON;
                DELETE FROM 
                    [app].[TeamPick]
                WHERE
                    PoolEntryId = @Id",
                new
                {
                    Id = id
                }).ConfigureAwait(false);
            }
        }

        private const string _getSql = @"
            SET NOCOUNT ON;
            SELECT TOP 1
                Id, TeamId, PoolEntryId, Year, Round
            FROM 
                [app].[TeamPick]
            WHERE
                Id = @Id";

        private const string _listSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, TeamId, PoolEntryId, Year, Round
            FROM 
                [app].[TeamPick]
            WHERE 
                Year = @Year
            ORDER BY
                PoolEntryId, TeamId";

        private const string _insertSql = @"
            SET NOCOUNT ON;
	        DECLARE @ExistingId	int;
	        SET @ExistingId = NULL;

	        SELECT TOP 1
		        @ExistingId = Id
	        FROM	
		        [app].[TeamPick]
	        WHERE	
		        TeamId = @TeamId
            AND
                PoolEntryId = @PoolEntryId
            AND
                Round = @Round
            AND
                Year = @Year
	            
	        IF(@ExistingId IS NULL)
	        BEGIN
		        INSERT INTO [app].[TeamPick]
		        (TeamId, PoolEntryId, Year, Round)
		        VALUES
		        (@TeamId, @PoolEntryId, @Year, @Round)

		        SELECT TOP 1 
			        Id
		        FROM	
		            [app].[TeamPick]
	            WHERE	
		            TeamId = @TeamId
                AND
                    PoolEntryId = @PoolEntryId
                AND
                    Round = @Round     
                AND
                    Year = @Year  
            END
	        ELSE
	        BEGIN
		        SELECT -1
	        END";

        private const string _updateSql = @"
            SET NOCOUNT ON;
            DECLARE @ExistingId	int;
	        SET @ExistingId = NULL;

	        SELECT TOP 1
		        @ExistingId = Id
	        FROM	
		        [app].[TeamPick]
	        WHERE	
		        TeamId = @TeamId
            AND
                PoolEntryId = @PoolEntryId
            AND
                Round = @Round      
            AND
                Year = @Year

            IF(@ExistingId IS NULL OR @ExistingId = @Id)
	        BEGIN
		        UPDATE 
                    [app].[TeamPick]
                SET
                    TeamId = @TeamId,
                    PoolEntryId = @PoolEntryId,
                    Year = @Year,
                    Round = @Round
		        WHERE	
		            Id = @Id
                    
                SELECT @Id
            END
            ELSE
            BEGIN
                SELECT -1
            END";       
    }
}
