-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 27, 2023 at 01:26 PM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `rpg`
--

-- --------------------------------------------------------

--
-- Table structure for table `characters`
--

CREATE TABLE `characters` (
  `id` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `CreationDate` date NOT NULL,
  `BackStory` varchar(255) NOT NULL,
  `Status` varchar(255) NOT NULL,
  `fk_Classid_Class` int(11) NOT NULL,
  `fk_Race_Infoid_Race_Info` int(11) NOT NULL,
  `fk_Factionid_Faction` int(11) NOT NULL,
  `fk_Userid_User` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `characters`
--

INSERT INTO `characters` (`id`, `Name`, `CreationDate`, `BackStory`, `Status`, `fk_Classid_Class`, `fk_Race_Infoid_Race_Info`, `fk_Factionid_Faction`, `fk_Userid_User`) VALUES
(0, 'Hello', '2002-09-10', 'DIED ONCE', 'Dead', 2, 0, 5, 2);

-- --------------------------------------------------------

--
-- Table structure for table `character_quests`
--

CREATE TABLE `character_quests` (
  `id_Quest` int(11) NOT NULL,
  `fk_Characterid` int(11) NOT NULL,
  `fk_Questsid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `class`
--

CREATE TABLE `class` (
  `Name` varchar(255) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `Difficulty` varchar(255) NOT NULL,
  `id_Class` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `class`
--

INSERT INTO `class` (`Name`, `Description`, `Difficulty`, `id_Class`) VALUES
('Wolf', 'A man that can turn into a wolf at night', 'Hard', 2),
('Sniper', 'Long range class that lets you snipe you enemies', 'Hard', 9);

-- --------------------------------------------------------

--
-- Table structure for table `faction`
--

CREATE TABLE `faction` (
  `Name` varchar(255) NOT NULL,
  `MemberSize` int(11) NOT NULL,
  `Level` int(11) NOT NULL,
  `Owner` varchar(255) NOT NULL,
  `id_Faction` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `faction`
--

INSERT INTO `faction` (`Name`, `MemberSize`, `Level`, `Owner`, `id_Faction`) VALUES
('Bears', 25, 5, 'Beard_Dog', 5);

-- --------------------------------------------------------

--
-- Table structure for table `pets`
--

CREATE TABLE `pets` (
  `Name` varchar(255) NOT NULL,
  `Age` int(11) NOT NULL,
  `Type` varchar(255) NOT NULL,
  `Status` varchar(255) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `id_Pets` int(11) NOT NULL,
  `fk_Characterid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `quests`
--

CREATE TABLE `quests` (
  `id` int(11) NOT NULL,
  `Lvl_Req` int(11) NOT NULL,
  `Exp` int(11) NOT NULL,
  `Gold` int(11) NOT NULL,
  `Status` varchar(255) NOT NULL,
  `Description` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `quests`
--

INSERT INTO `quests` (`id`, `Lvl_Req`, `Exp`, `Gold`, `Status`, `Description`) VALUES
(9, 10, 10, 10, 'Working', 'Kill 5 demons');

-- --------------------------------------------------------

--
-- Table structure for table `race_info`
--

CREATE TABLE `race_info` (
  `Ability` varchar(255) NOT NULL,
  `BonusStats` varchar(255) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `id_Race_Info` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `race_info`
--

INSERT INTO `race_info` (`Ability`, `BonusStats`, `Description`, `id_Race_Info`) VALUES
('High kick', '10 ad', 'Demon', 0);

-- --------------------------------------------------------

--
-- Table structure for table `spells`
--

CREATE TABLE `spells` (
  `Damage` double NOT NULL,
  `Description` varchar(255) NOT NULL,
  `ManaCost` double NOT NULL,
  `Target` varchar(255) NOT NULL,
  `Lvl_Req` int(11) NOT NULL,
  `id_Spells` int(11) NOT NULL,
  `fk_Classid_Class` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `spells`
--

INSERT INTO `spells` (`Damage`, `Description`, `ManaCost`, `Target`, `Lvl_Req`, `id_Spells`, `fk_Classid_Class`) VALUES
(5, 'Howl', 5, 'Self', 5, 2, 2),
(10, 'Snipers_aim', 7, 'Enemies', 9, 8, 9);

-- --------------------------------------------------------

--
-- Table structure for table `statistics`
--

CREATE TABLE `statistics` (
  `Health` double NOT NULL,
  `Mana` double NOT NULL,
  `Lvl` int(11) NOT NULL,
  `Def_Points` int(11) NOT NULL,
  `Attack_Points` int(11) NOT NULL,
  `Exp_Points` double NOT NULL,
  `Exp_ForLvlUp` double NOT NULL,
  `Gender` varchar(255) NOT NULL,
  `Gold` int(11) NOT NULL,
  `Gems` int(11) NOT NULL,
  `Movement_Speed` int(11) NOT NULL,
  `id_Statistics` int(11) NOT NULL,
  `fk_Petsid_Pets` int(11) NOT NULL,
  `fk_Characterid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `Username` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `CreationDate` date NOT NULL,
  `AccessType` varchar(255) NOT NULL,
  `Status` varchar(255) NOT NULL,
  `id_User` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`Username`, `Email`, `Password`, `CreationDate`, `AccessType`, `Status`, `id_User`) VALUES
('Catfoxy123', 'hendermann123@gmail.com', 'sktPlayer123', '2023-04-14', 'Admin', 'Working', 2);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `characters`
--
ALTER TABLE `characters`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Has3` (`fk_Classid_Class`),
  ADD KEY `Apartof` (`fk_Factionid_Faction`),
  ADD KEY `Has9` (`fk_Race_Infoid_Race_Info`),
  ADD KEY `Has7` (`fk_Userid_User`);

--
-- Indexes for table `character_quests`
--
ALTER TABLE `character_quests`
  ADD PRIMARY KEY (`id_Quest`),
  ADD KEY `Has4` (`fk_Characterid`),
  ADD KEY `Has8` (`fk_Questsid`);

--
-- Indexes for table `class`
--
ALTER TABLE `class`
  ADD PRIMARY KEY (`id_Class`);

--
-- Indexes for table `faction`
--
ALTER TABLE `faction`
  ADD PRIMARY KEY (`id_Faction`);

--
-- Indexes for table `pets`
--
ALTER TABLE `pets`
  ADD PRIMARY KEY (`id_Pets`),
  ADD KEY `Has5` (`fk_Characterid`);

--
-- Indexes for table `quests`
--
ALTER TABLE `quests`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `race_info`
--
ALTER TABLE `race_info`
  ADD PRIMARY KEY (`id_Race_Info`);

--
-- Indexes for table `spells`
--
ALTER TABLE `spells`
  ADD PRIMARY KEY (`id_Spells`),
  ADD KEY `Has2` (`fk_Classid_Class`);

--
-- Indexes for table `statistics`
--
ALTER TABLE `statistics`
  ADD PRIMARY KEY (`id_Statistics`),
  ADD UNIQUE KEY `fk_Petsid_Pets` (`fk_Petsid_Pets`),
  ADD UNIQUE KEY `fk_Characterid` (`fk_Characterid`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id_User`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `characters`
--
ALTER TABLE `characters`
  ADD CONSTRAINT `Apartof` FOREIGN KEY (`fk_Factionid_Faction`) REFERENCES `faction` (`id_Faction`),
  ADD CONSTRAINT `Has3` FOREIGN KEY (`fk_Classid_Class`) REFERENCES `class` (`id_Class`),
  ADD CONSTRAINT `Has7` FOREIGN KEY (`fk_Userid_User`) REFERENCES `users` (`id_User`),
  ADD CONSTRAINT `Has9` FOREIGN KEY (`fk_Race_Infoid_Race_Info`) REFERENCES `race_info` (`id_Race_Info`);

--
-- Constraints for table `character_quests`
--
ALTER TABLE `character_quests`
  ADD CONSTRAINT `Has4` FOREIGN KEY (`fk_Characterid`) REFERENCES `characters` (`id`),
  ADD CONSTRAINT `Has8` FOREIGN KEY (`fk_Questsid`) REFERENCES `quests` (`id`);

--
-- Constraints for table `pets`
--
ALTER TABLE `pets`
  ADD CONSTRAINT `Has5` FOREIGN KEY (`fk_Characterid`) REFERENCES `characters` (`id`);

--
-- Constraints for table `spells`
--
ALTER TABLE `spells`
  ADD CONSTRAINT `Has2` FOREIGN KEY (`fk_Classid_Class`) REFERENCES `class` (`id_Class`);

--
-- Constraints for table `statistics`
--
ALTER TABLE `statistics`
  ADD CONSTRAINT `Has10` FOREIGN KEY (`fk_Characterid`) REFERENCES `characters` (`id`),
  ADD CONSTRAINT `Has6` FOREIGN KEY (`fk_Petsid_Pets`) REFERENCES `pets` (`id_Pets`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
