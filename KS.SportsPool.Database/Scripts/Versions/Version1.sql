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

/* Athletes */
IF NOT EXISTS (SELECT Id FROM [app].[Athlete] WHERE LastName = 'Kane')
BEGIN
	DECLARE @TeamId INT
	
	SELECT @TeamId = id 
	FROM
		[app].[Team]
	WHERE
		Year = 2016
	AND
		Abbreviation = 'CHI'

	INSERT INTO 
		[app].[Athlete]
		(Year, TeamId, GroupName, FirstName, LastName, Position)
	VALUES
		(2016, @TeamId, 'West Forwards 1', 'Patrick', 'Kane', 'RW'),
		(2016, @TeamId, 'West Forwards 2', 'Jonathon', 'Toews', 'C'),
		(2016, @TeamId, 'West Forwards 2', 'Artemi', 'Panarin', 'LW'),
		(2016, @TeamId, 'West Defense 1', 'Duncan', 'Keith', 'D'),
		(2016, @TeamId, 'West Defense 2', 'Brent', 'Seabrook', 'D')

	SELECT @TeamId = id 
	FROM
		[app].[Team]
	WHERE
		Year = 2016
	AND
		Abbreviation = 'DAL'

	INSERT INTO 
		[app].[Athlete]
		(Year, TeamId, GroupName, FirstName, LastName, Position)
	VALUES
		(2016, @TeamId, 'West Forwards 1', 'Tyler', 'Seguin', 'C'),
		(2016, @TeamId, 'West Forwards 1', 'Jamie', 'Benn', 'RW'),
		(2016, @TeamId, 'West Forwards 2', 'Patrick', 'Sharp', 'RW'),
		(2016, @TeamId, 'West Defense 1', 'John', 'Klinkberg', 'D'),
		(2016, @TeamId, 'West Defense 2', 'Johnny', 'Oduya', 'D')

	SELECT @TeamId = id 
	FROM
		[app].[Team]
	WHERE
		Year = 2016
	AND
		Abbreviation = 'ANA'

	INSERT INTO 
		[app].[Athlete]
		(Year, TeamId, GroupName, FirstName, LastName, Position)
	VALUES
		(2016, @TeamId, 'West Forwards 1', 'Corey', 'Perry', 'RW'),
		(2016, @TeamId, 'West Forwards 1', 'Ryan', 'Getzlaf', 'C'),
		(2016, @TeamId, 'West Forwards 2', 'Ryan', 'Kesler', 'C'),
		(2016, @TeamId, 'West Defense 1', 'Cam', 'Fowler', 'D'),
		(2016, @TeamId, 'West Defense 2', 'Kevin', 'Bieksa', 'D')

	SELECT @TeamId = id 
	FROM
		[app].[Team]
	WHERE
		Year = 2016
	AND
		Abbreviation = 'LAK'

	INSERT INTO 
		[app].[Athlete]
		(Year, TeamId, GroupName, FirstName, LastName, Position)
	VALUES
		(2016, @TeamId, 'West Forwards 1', 'Anze', 'Kopitar', 'C'),
		(2016, @TeamId, 'West Forwards 1', 'Marion', 'Gaborik', 'LW'),
		(2016, @TeamId, 'West Forwards 2', 'Milan', 'Lucic', 'RW'),
		(2016, @TeamId, 'West Defense 1', 'Drew', 'Doughty', 'D'),
		(2016, @TeamId, 'West Defense 2', 'Jim', 'Beam', 'D')

	SELECT @TeamId = id 
	FROM
		[app].[Team]
	WHERE
		Year = 2016
	AND
		Abbreviation = 'PIT'

	INSERT INTO 
		[app].[Athlete]
		(Year, TeamId, GroupName, FirstName, LastName, Position)
	VALUES
		(2016, @TeamId, 'East Forwards 1', 'Sidney', 'Crosby', 'C'),
		(2016, @TeamId, 'East Forwards 1', 'Evgeni', 'Malkin', 'C'),
		(2016, @TeamId, 'East Forwards 2', 'Phil', 'Kessel', 'RW'),
		(2016, @TeamId, 'East Defense 1', 'Kris', 'Letang', 'D'),
		(2016, @TeamId, 'East Defense 2', 'Rob', 'Scuderi', 'D')

	SELECT @TeamId = id 
	FROM
		[app].[Team]
	WHERE
		Year = 2016
	AND
		Abbreviation = 'WSH'

	INSERT INTO 
		[app].[Athlete]
		(Year, TeamId, GroupName, FirstName, LastName, Position)
	VALUES
		(2016, @TeamId, 'East Forwards 1', 'Alexander', 'Ovechkin', 'RW'),
		(2016, @TeamId, 'East Forwards 1', 'Nicholas', 'Backstrom', 'C'),
		(2016, @TeamId, 'East Forwards 2', 'Mike', 'Richards', 'C'),
		(2016, @TeamId, 'East Defense 1', 'Mike', 'Green', 'D'),
		(2016, @TeamId, 'East Defense 2', 'Bob', 'Smithers', 'D')

	SELECT @TeamId = id 
	FROM
		[app].[Team]
	WHERE
		Year = 2016
	AND
		Abbreviation = 'FLA'

	INSERT INTO 
		[app].[Athlete]
		(Year, TeamId, GroupName, FirstName, LastName, Position)
	VALUES
		(2016, @TeamId, 'East Forwards 1', 'Jaromir', 'Jagr', 'C'),
		(2016, @TeamId, 'East Forwards 1', 'Nicholas', 'Backstrov', 'C'),
		(2016, @TeamId, 'East Forwards 2', 'Mike', 'Pletia', 'C'),
		(2016, @TeamId, 'East Defense 1', 'Mike', 'Kilo', 'D'),
		(2016, @TeamId, 'East Defense 2', 'Bob', 'Smoothers', 'D')

	SELECT @TeamId = id 
	FROM
		[app].[Team]
	WHERE
		Year = 2016
	AND
		Abbreviation = 'TBL'
	
	INSERT INTO 
		[app].[Athlete]
		(Year, TeamId, GroupName, FirstName, LastName, Position)
	VALUES
		(2016, @TeamId, 'East Forwards 1', 'Steven', 'Stamkos', 'C'),
		(2016, @TeamId, 'East Forwards 1', 'Tyler', 'Johnston', 'C'),
		(2016, @TeamId, 'East Forwards 2', 'TJ', 'Hunter', 'C'),
		(2016, @TeamId, 'East Defense 1', 'Lori', 'Korpiokoski', 'D'),
		(2016, @TeamId, 'East Defense 2', 'Bill', 'Smthe', 'D')
END;