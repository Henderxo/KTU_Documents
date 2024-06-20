#@(#) script.ddl

DROP TABLE IF EXISTS Charachters;
DROP TABLE IF EXISTS Has3;
DROP TABLE IF EXISTS Has2;
DROP TABLE IF EXISTS Statistics;
DROP TABLE IF EXISTS Has;
DROP TABLE IF EXISTS Pets;
DROP TABLE IF EXISTS Has4;
DROP TABLE IF EXISTS Has5;
DROP TABLE IF EXISTS Logs;
DROP TABLE IF EXISTS User;
DROP TABLE IF EXISTS Spells;
DROP TABLE IF EXISTS Race_Info;
DROP TABLE IF EXISTS Quests;
DROP TABLE IF EXISTS Items;
DROP TABLE IF EXISTS Friends;
DROP TABLE IF EXISTS Faction;
DROP TABLE IF EXISTS Economy;
DROP TABLE IF EXISTS Class;

CREATE TABLE Class
(
	Name varchar (255) NOT NULL,
	Description varchar (255) NOT NULL,
	Difficulty varchar (255) NOT NULL,
	id_Class integer NOT NULL,
	PRIMARY KEY(id_Class)
);

CREATE TABLE Economy
(
	Gold int NOT NULL,
	Gems int NOT NULL,
	id_Economy integer NOT NULL,
	PRIMARY KEY(id_Economy)
);

CREATE TABLE Faction
(

	Name varchar (255) NOT NULL,
	MemberSize int NOT NULL,
	Level int NOT NULL,
	Owner varchar (255) NOT NULL,
	id_Faction integer NOT NULL,
	PRIMARY KEY(id_Faction)
);

CREATE TABLE Friends
(
	Username varchar (255) NOT NULL,
	Lvl int NOT NULL,
	Status varchar (255) NOT NULL,
	id_Friends integer NOT NULL,
	PRIMARY KEY(id_Friends)
);

CREATE TABLE Items
(
	Attack_Points int NOT NULL,
	Def_Points int NOT NULL,
	Lvl_Req int NOT NULL,
	Health double precision NOT NULL,
	Description varchar (255) NOT NULL,
	Effect varchar (255) NOT NULL,
	id_Items integer NOT NULL,
	PRIMARY KEY(id_Items)
);

CREATE TABLE Quests
(
	Lvl_Req int NOT NULL,
	Exp int NOT NULL,
	Gold int NOT NULL,
	Status varchar (255) NOT NULL,
	Description varchar (255) NOT NULL,
	id_Quests integer NOT NULL,
	PRIMARY KEY(id_Quests)
);

CREATE TABLE Race_Info
(
	Ability varchar (255) NOT NULL,
	BonusStats varchar (255) NOT NULL,
	Description varchar (255) NOT NULL,
	id_Race_Info integer NOT NULL,
	PRIMARY KEY(id_Race_Info)
);

CREATE TABLE Spells
(
	Damage double precision NOT NULL,
	Description varchar (255) NOT NULL,
	ManaCost double precision NOT NULL,
	Target varchar (255) NOT NULL,
	Lvl_Req int NOT NULL,
	id_Spells integer NOT NULL,
	PRIMARY KEY(id_Spells)
);

CREATE TABLE User
(
	Username varchar (255) NOT NULL,
	Email varchar (255) NOT NULL,
	Password varchar (255) NOT NULL,
	CreationDate date NOT NULL,
	AccessType varchar (255) NOT NULL,
	Status varchar (255) NOT NULL,
	id_User integer NOT NULL,
	PRIMARY KEY(id_User)
);

CREATE TABLE Logs
(
	Date date NOT NULL,
	Time varchar (255) NOT NULL,
	Message varchar (255) NOT NULL,
	Rewards varchar (255) NOT NULL,
	id_Logs integer NOT NULL,
	fk_Userid_User integer NOT NULL,
	fk_Questsid_Quests integer NOT NULL,
	PRIMARY KEY(id_Logs),
	CONSTRAINT Has FOREIGN KEY(fk_Userid_User) REFERENCES User (id_User),
	CONSTRAINT Make FOREIGN KEY(fk_Questsid_Quests) REFERENCES Quests (id_Quests)
);

CREATE TABLE Has5
(
	fk_Classid_Class integer NOT NULL,
	fk_Spellsid_Spells integer NOT NULL,
	PRIMARY KEY(fk_Classid_Class, fk_Spellsid_Spells),
	CONSTRAINT Has FOREIGN KEY(fk_Classid_Class) REFERENCES Class (id_Class)
);

CREATE TABLE Has4
(
	fk_Userid_User integer NOT NULL,
	fk_Friendsid_Friends integer NOT NULL,
	PRIMARY KEY(fk_Userid_User, fk_Friendsid_Friends),
	CONSTRAINT Has FOREIGN KEY(fk_Userid_User) REFERENCES User (id_User)
);

CREATE TABLE Pets
(
	Name varchar (255) NOT NULL,
	Age int NOT NULL,
	Type varchar (255) NOT NULL,
	Status varchar (255) NOT NULL,
	Description varchar (255) NOT NULL,
	id_Pets integer NOT NULL,
	fk_Charachtersid_Charachters integer NOT NULL,
	PRIMARY KEY(id_Pets),
	CONSTRAINT Has FOREIGN KEY(fk_Charachtersid_Charachters) REFERENCES Charachters (id_Charachters)
);

CREATE TABLE Has
(
	fk_Charachtersid_Charachters integer NOT NULL,
	fk_Questsid_Quests integer NOT NULL,
	PRIMARY KEY(fk_Charachtersid_Charachters, fk_Questsid_Quests),
	CONSTRAINT Has FOREIGN KEY(fk_Charachtersid_Charachters) REFERENCES Charachters (id_Charachters)
);

CREATE TABLE Statistics
(
	Health double precision NOT NULL,
	Mana double precision NOT NULL,
	Lvl int NOT NULL,
	Def_Points int NOT NULL,
	Attack_Points int NOT NULL,
	Exp_Points double precision NOT NULL,
	Exp_ForLvlUp double precision NOT NULL,
	Gender varchar (255) NOT NULL,
	Movement_Speed int NOT NULL,
	id_Statistics integer NOT NULL,
	fk_Petsid_Pets integer NOT NULL,
	PRIMARY KEY(id_Statistics),
	UNIQUE(fk_Petsid_Pets),
	CONSTRAINT Has FOREIGN KEY(fk_Petsid_Pets) REFERENCES Pets (id_Pets)
);

CREATE TABLE Has2
(
	fk_Spellsid_Spells integer NOT NULL,
	fk_Petsid_Pets integer NOT NULL,
	PRIMARY KEY(fk_Spellsid_Spells, fk_Petsid_Pets),
	CONSTRAINT Has FOREIGN KEY(fk_Spellsid_Spells) REFERENCES Spells (id_Spells)
);

CREATE TABLE Has3
(
	fk_Charachtersid_Charachters integer NOT NULL,
	fk_Itemsid_Items integer NOT NULL,
	PRIMARY KEY(fk_Charachtersid_Charachters, fk_Itemsid_Items),
	CONSTRAINT Has FOREIGN KEY(fk_Charachtersid_Charachters) REFERENCES Charachters (id_Charachters)
);

CREATE TABLE Charachters
(
	Name varchar (255) NOT NULL,
	CreationDate date NOT NULL,
	BackStory varchar (255) NOT NULL,
	Status varchar (255) NOT NULL,
	id_Charachters integer NOT NULL,
	fk_Userid_User integer NOT NULL,
	fk_Statisticsid_Statistics integer NOT NULL,
	fk_Economyid_Economy integer NOT NULL,
	fk_Race_Infoid_Race_Info integer NOT NULL,
	fk_Classid_Class integer NOT NULL,
	fk_Factionid_Faction integer NOT NULL,
	PRIMARY KEY(id_Charachters),
	UNIQUE(fk_Statisticsid_Statistics),
	CONSTRAINT Has FOREIGN KEY(fk_Userid_User) REFERENCES User (id_User),
	CONSTRAINT A Part Of FOREIGN KEY(fk_Factionid_Faction) REFERENCES Faction (id_Faction)
);
