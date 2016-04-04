using KS.SportsPool.Component.Caching.Interface;
using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;

namespace KS.SportsPool.Data.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Provides access to PoolEntry data in the data store using the Dapper framework.
    /// 
    /// Dapper documentation at https://github.com/StackExchange/dapper-dot-net
    /// </summary>
    public class DapperPoolEntryRepository : BaseDapperRepository<PoolEntry>,
        IPoolEntryRepository
    {    
        public DapperPoolEntryRepository(ICacheProvider cacheProvider)
            : base(cacheProvider)
        {
            CacheContainerName = "PoolEntry";
            TableName = "[app].[PoolEntry]";
            CacheSeconds = 0;
        }

        protected override void CreateSql()
        {
            GetSql = _getSql;
            ListSql = _listSql;
            SearchSql = _searchSql;
            InsertSql = _insertSql;
            UpdateSql = _updateSql;
        }

        private const string _getSql = @"
            SET NOCOUNT ON;
            SELECT TOP 1
                Id, Name, Telephone, Email
            FROM 
                [app].[PoolEntry]
            WHERE
                Id = @Id";

        private const string _listSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, Name, Telephone, Email, Score
            FROM 
                [app].[PoolEntry]
            ORDER BY
                Name";

        private const string _searchSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, Name, Telephone, Email, Score
            FROM 
                [app].[PoolEntry]
            WHERE           
                Name like @SearchTerms
            ORDER BY
                Name";

        private const string _insertSql = @"
            SET NOCOUNT ON;
	        DECLARE @ExistingId	int;
	        SET @ExistingId = NULL;

	        SELECT TOP 1
		        @ExistingId = Id
	        FROM	
		        [app].[PoolEntry]
	        WHERE	
		        Name = @Name
	            
	        IF(@ExistingId IS NULL)
	        BEGIN
		        INSERT INTO [app].[PoolEntry]
		        (Name, Telephone, Email, Score)
		        VALUES
                (@Name, @Telephone, @Email, @Score)
		        
		        SELECT TOP 1 
			        Id
		        FROM	
		            [app].[PoolEntry]
	            WHERE	
		            Name = @Name
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
		        [app].[PoolEntry]
	        WHERE	
		        Name = @Name

            IF(@ExistingId IS NULL OR @ExistingId = @Id)
	        BEGIN
		        UPDATE 
                    [app].[PoolEntry]
                SET
                    Name = @Name,
                    Telephone = @Telephone,
                    Email = @Email,
                    Score = @Score
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
