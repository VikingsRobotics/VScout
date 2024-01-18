CREATE TABLE Round (
	RoundId integer primary key,
	StartTime timestamp,
	MatchNumber integer,
	Field varchar,
	TournamentLevel varchar
);

CREATE TABLE Team (
	TeamId integer primary key,
	TeamNumber integer,
	TeamName varchar
);

CREATE TABLE TeamRound (
	TeamRoundId integer primary key,
	TeamId integer,
	RoundId integer,
	Station varchar,
	AutonomousMoved integer, -- boolean
	AutonomousConeHigh integer,
	AutonomousConeMiddle integer,
	AutonomousConeBottom integer,
	AutonomousCubeHigh integer,
	AutonomousCubeMiddle integer,
	AutonomousCubeBottom integer,
	AutonomousDocked integer, -- boolean
	AutonomousEngaged integer, -- boolean
	ConeHigh integer,
	ConeMiddle integer,
	ConeBottom integer,
	CubeHigh integer,
	CubeMiddle integer,
	CubeBottom integer,
	Comments varchar,
	Docked integer,
	Engaged integer,
	Parked integer
);