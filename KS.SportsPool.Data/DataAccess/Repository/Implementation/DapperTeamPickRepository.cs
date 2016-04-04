using KS.SportsPool.Component.Caching.Interface;
using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;

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

        private const string _getSql = @"
            SET NOCOUNT ON;
            SELECT TOP 1
                Id, TeamId, PoolEntryId, Round
            FROM 
                [app].[TeamPick]
            WHERE
                Id = @Id";

        private const string _listSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, TeamId, PoolEntryId, Round
            FROM 
                [app].[TeamPick]
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
	            
	        IF(@ExistingId IS NULL)
	        BEGIN
		        INSERT INTO [app].[TeamPick]
		        (TeamId, PoolEntryId, Round)
		        VALUES
		        (@TeamId, @PoolEntryId, @Round)

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

            IF(@ExistingId IS NULL OR @ExistingId = @Id)
	        BEGIN
		        UPDATE 
                    [app].[TeamPick]
                SET
                    TeamId = @TeamId,
                    PoolEntryId = @PoolEntryId,
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
