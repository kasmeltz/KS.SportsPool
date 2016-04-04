/**********************
   Initial data load
**********************/

/* Teams */
IF NOT EXISTS (SELECT Id FROM [app].[Team] WHERE Name = 'Anaheim Ducks')
BEGIN
	INSERT INTO 
		[app].[Team]
		(Year, Name, Abbreviation, Conference, Division)
	VALUES
		(2016, 'Anaheim Ducks', 'ANA', 'West', 'Pacific'),
		(2016, 'Los Angeles Kings', 'LAK', 'West', 'Pacific'),
		(2016, 'San Jose Sharks', 'SJS', 'West', 'Pacific'),
		(2016, 'Minnesota Wild', 'MIN', 'West', 'Pacific'),
		(2016, 'Dallas Stars', 'DAL', 'West', 'Central'),
		(2016, 'St. Louis Blues', 'STL', 'West', 'Central'),
		(2016, 'Chicago Blackhawks', 'CHI', 'West', 'Central'),
		(2016, 'Nashville Predators', 'NSH', 'West', 'Central'),
		(2016, 'Washington Capitals', 'WSH', 'East', 'Metropolitan'),
		(2016, 'Pittsburgh Penguins', 'PIT', 'East', 'Metropolitan'),
		(2016, 'New York Rangers', 'NYR', 'East', 'Metropolitan'),
		(2016, 'New York Islanders', 'NYI', 'East', 'Metropolitan'),
		(2016, 'Florida Panthers', 'FLA', 'East', 'Atlantic'),
		(2016, 'Tampa Bay Lightning', 'TBL', 'East', 'Atlantic'),
		(2016, 'Detroit Red Wing', 'DET', 'East', 'Atlantic'),
		(2016, 'Boston Bruins', 'BOS', 'East', 'Atlantic')		
END;