using KS.SportsPool.Component.Caching.Interface;
using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;

namespace KS.SportsPool.Data.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Provides access to Athlete data in the data store using the Dapper framework.
    /// 
    /// Dapper documentation at https://github.com/StackExchange/dapper-dot-net
    /// </summary>
    public class DapperAthleteRepository : BaseDapperRepository<Athlete>,
        IAthleteRepository
    {    
        public DapperAthleteRepository(ICacheProvider cacheProvider)
            : base(cacheProvider)
        {
            CacheContainerName = "Athlete";
            TableName = "[mlist].[Athlete]";
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
                Id, FirstName, LastName, Position, Goals, Assists
            FROM 
                [mlist].[Athlete]
            WHERE
                Id = @Id";

        private const string _listSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, FirstName, LastName, Position, Goals, Assists
            FROM 
                [mlist].[Athlete]
            ORDER BY
                LastName, FirstName";

        private const string _searchSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, FirstName, LastName, Position, Goals, Assists
            FROM 
                [mlist].[Athlete]
            WHERE           
                FirstName like @SearchTerms
            OR      
                LastName like @SearchTerms
            ORDER BY
                LastName, FirstName";

        private const string _insertSql = @"
            SET NOCOUNT ON;
	        DECLARE @ExistingId	int;
	        SET @ExistingId = NULL;

	        SELECT TOP 1
		        @ExistingId = Id
	        FROM	
		        [mlist].[Athlete]
	        WHERE	
		        LastName = @LastName
            AND
                FirstName = @FirstName
	            
	        IF(@ExistingId IS NULL)
	        BEGIN
		        INSERT INTO [mlist].[Athlete]
		        (FirstName, LastName, Position, Goals, Assists)
		        VALUES
		        (@FirstName, @LastName, @Position, @Goals, @Assists)

		        SELECT TOP 1 
			        Id
		        FROM	
		            [mlist].[Athlete]
	            WHERE	
		            LastName = @LastName
                AND
                    FirstName = @FirstName         
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
		        [mlist].[Athlete]
	        WHERE	
		        LastName = @LastName
            AND
                FirstName = @FirstName

            IF(@ExistingId IS NULL OR @ExistingId = @Id)
	        BEGIN
		        UPDATE 
                    [mlist].[Athlete]
                SET
                    FirstName = @FirstName,
                    LastName = @LastName,
                    Position = @Position,
                    Goals = @Goals,
                    Assists = @Assists
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
