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
	AutoMoved integer, -- boolean
	AutoAmp integer,
	AutoSpeaker integer,
	Amp integer,
	Speaker integer,
	OnStage integer, -- boolean
	OnStageChain integer, -- boolean
	NoteInOnChain integer, -- boolean
	Spotlight integer, -- boolean
	Notes varchar
);