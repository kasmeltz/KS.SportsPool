using KS.SportsPool.Component.Caching.Interface;
using KS.SportsPool.Data.DataAccess.Repository.Interface;
using KS.SportsPool.Data.POCO;

namespace KS.SportsPool.Data.DataAccess.Repository.Implementation
{
    /// <summary>
    /// Provides access to Team data in the data store using the Dapper framework.
    /// 
    /// Dapper documentation at https://github.com/StackExchange/dapper-dot-net
    /// </summary>
    public class DapperTeamRepository : BaseDapperRepository<Team>,
        ITeamRepository
    {    
        public DapperTeamRepository(ICacheProvider cacheProvider)
            : base(cacheProvider)
        {
            CacheContainerName = "Team";
            TableName = "[app].[Team]";
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
                Id, Year, Name, Abbreviation, Conference, Division, Round1, Round2, Round3, Round4
            FROM 
                [app].[Team]
            WHERE
                Id = @Id";

        private const string _listSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, Year, Name, Abbreviation, Conference, Division, Round1, Round2, Round3, Round4
            FROM 
                [app].[Team]
            WHERE
                Year = @Year
            ORDER BY
                Name";

        private const string _searchSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, Year, Name, Abbreviation, Conference, Division, Round1, Round2, Round3, Round4
            FROM 
                [app].[Team]
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
		        [app].[Team]
	        WHERE	
		        Name = @Name
            AND
                Year = @Year
	            
	        IF(@ExistingId IS NULL)
	        BEGIN
		        INSERT INTO [app].[Team]
		        (Year, Name, Abbreviation, Conference, Division, Round1, Round2, Round3, Round4)
		        VALUES
                (@Year, @Name, @Abbreviation, @Conference, @Division, @Round1, @Round2, @Round3, @Round4)
		        
		        SELECT TOP 1 
			        Id
		        FROM	
		            [app].[Team]
	            WHERE	
		            Name = @Name
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
		        [app].[Team]
	        WHERE	
		        Name = @Name
            AND
                Year = @Year

            IF(@ExistingId IS NULL OR @ExistingId = @Id)
	        BEGIN
		        UPDATE 
                    [app].[Team]
                SET
                    Year = @Year,
                    Name = @Name,
                    Abbreviation = @Abbreviation,
                    Conference = @Conference,
                    Division = @Division,
                    Round1 = @Round1,
                    Round2 = @Round2,
                    Round3 = @Round3,
                    Round4 = @Round4
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
