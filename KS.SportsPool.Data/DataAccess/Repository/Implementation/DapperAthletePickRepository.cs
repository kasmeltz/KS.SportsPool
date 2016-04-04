using KS.SportsPool.Component.Caching.Interface;
using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;

namespace KS.SportsPool.Data.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Provides access to AthletePick data in the data store using the Dapper framework.
    /// 
    /// Dapper documentation at https://github.com/StackExchange/dapper-dot-net
    /// </summary>
    public class DapperAthletePickRepository : BaseDapperRepository<AthletePick>,
        IAthletePickRepository
    {    
        public DapperAthletePickRepository(ICacheProvider cacheProvider)
            : base(cacheProvider)
        {
            CacheContainerName = "AthletePick";
            TableName = "[app].[AthletePick]";
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
                Id, AthleteId, PoolEntryId, Year
            FROM 
                [app].[AthletePick]
            WHERE
                Id = @Id";

        private const string _listSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, AthleteId, PoolEntryId, Year
            FROM 
                [app].[AthletePick]
            WHERE
                Year = @Year
            ORDER BY
                PoolEntryId, AthleteId";

        private const string _insertSql = @"
            SET NOCOUNT ON;
	        DECLARE @ExistingId	int;
	        SET @ExistingId = NULL;

	        SELECT TOP 1
		        @ExistingId = Id
	        FROM	
		        [app].[AthletePick]
	        WHERE	
                AthleteId = @AthleteId
            AND
		        PoolEntryId = @PoolEntryId
            AND
                Year = @Year
	            
	        IF(@ExistingId IS NULL)
	        BEGIN
		        INSERT INTO [app].[AthletePick]
		        (AthleteId, PoolEntryId, Year)
		        VALUES
                (@AthleteId, @PoolEntryId, @Year)
		        
		        SELECT TOP 1 
			        Id
		        FROM	
		            [app].[AthletePick]
	            WHERE	
		            AthleteId = @AthleteId
                AND
		            PoolEntryId = @PoolEntryId
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
		        [app].[AthletePick]
	        WHERE	
		        AthleteId = @AthleteId
            AND
		        PoolEntryId = @PoolEntryId
            AND
                Year = @Year

            IF(@ExistingId IS NULL OR @ExistingId = @Id)
	        BEGIN
		        UPDATE 
                    [app].[AthletePick]
                SET
                    AthleteId = @AthleteId,
                    PoolEntryId = @PoolEntryId,
                    Year = @Year
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
