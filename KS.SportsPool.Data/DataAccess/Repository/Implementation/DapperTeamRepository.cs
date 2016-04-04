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
            TableName = "[mlist].[Team]";
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
                Id, Name, Abbreviation, Conference, Division, Round1, Round2, Round3, Round4
            FROM 
                [mlist].[Team]
            WHERE
                Id = @Id";

        private const string _listSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, Name, Abbreviation, Conference, Division, Round1, Round2, Round3, Round4
            FROM 
                [mlist].[Team]
            ORDER BY
                Name";

        private const string _searchSql = @"
            SET NOCOUNT ON;
            SELECT  
                Id, Name, Abbreviation, Conference, Division, Round1, Round2, Round3, Round4
            FROM 
                [mlist].[Team]
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
		        [mlist].[Team]
	        WHERE	
		        Name = @LastName
	            
	        IF(@ExistingId IS NULL)
	        BEGIN
		        INSERT INTO [mlist].[Team]
		        (Name, Abbreviation, Conference, Division, Round1, Round2, Round3, Round4)
		        VALUES
                (@Name, @Abbreviation, @Conference, @Division, @Round1, @Round2, @Round3, @Round4)
		        
		        SELECT TOP 1 
			        Id
		        FROM	
		            [mlist].[Team]
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
		        [mlist].[Team]
	        WHERE	
		        Name = @Name

            IF(@ExistingId IS NULL OR @ExistingId = @Id)
	        BEGIN
		        UPDATE 
                    [mlist].[Team]
                SET
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
