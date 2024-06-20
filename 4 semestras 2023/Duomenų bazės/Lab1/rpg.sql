-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 01, 2023 at 09:47 PM
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
-- Table structure for table `charachters`
--

CREATE TABLE `charachters` (
  `Name` varchar(255) NOT NULL,
  `CreationDate` date NOT NULL,
  `BackStory` varchar(255) NOT NULL,
  `Statuss` varchar(255) NOT NULL,
  `id_Charachters` int(11) NOT NULL,
  `fk_Userid_User` int(11) NOT NULL,
  `fk_Statisticsid_Statistics` int(11) NOT NULL,
  `fk_Economyid_Economy` int(11) NOT NULL,
  `fk_Race_Infoid_Race_Info` int(11) NOT NULL,
  `fk_Classid_Class` int(11) NOT NULL,
  `fk_Factionid_Faction` int(11) NOT NULL
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

-- --------------------------------------------------------

--
-- Table structure for table `economy`
--

CREATE TABLE `economy` (
  `Gold` int(11) NOT NULL,
  `Gems` int(11) NOT NULL,
  `id_Economy` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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

-- --------------------------------------------------------

--
-- Table structure for table `friends`
--

CREATE TABLE `friends` (
  `Username` varchar(255) NOT NULL,
  `Lvl` int(11) NOT NULL,
  `Status` varchar(255) NOT NULL,
  `id_Friends` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `has`
--

CREATE TABLE `has` (
  `fk_Charachtersid_Charachters` int(11) NOT NULL,
  `fk_Questsid_Quests` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `has2`
--

CREATE TABLE `has2` (
  `fk_Spellsid_Spells` int(11) NOT NULL,
  `fk_Petsid_Pets` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `has3`
--

CREATE TABLE `has3` (
  `fk_Charachtersid_Charachters` int(11) NOT NULL,
  `fk_Itemsid_Items` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `has4`
--

CREATE TABLE `has4` (
  `fk_Userid_User` int(11) NOT NULL,
  `fk_Friendsid_Friends` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `has5`
--

CREATE TABLE `has5` (
  `fk_Classid_Class` int(11) NOT NULL,
  `fk_Spellsid_Spells` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `Attack_Points` int(11) NOT NULL,
  `Def_Points` int(11) NOT NULL,
  `Lvl_Req` int(11) NOT NULL,
  `Health` double NOT NULL,
  `Description` varchar(255) NOT NULL,
  `Effect` varchar(255) NOT NULL,
  `id_Items` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `logss`
--

CREATE TABLE `logss` (
  `Date` date NOT NULL,
  `Time` varchar(255) NOT NULL,
  `Message` varchar(255) NOT NULL,
  `Rewards` varchar(255) NOT NULL,
  `id_Logs` int(11) NOT NULL,
  `fk_Userid_User` int(11) NOT NULL,
  `fk_Questsid_Quests` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  `fk_Charachtersid_Charachters` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `quests`
--

CREATE TABLE `quests` (
  `Lvl_Req` int(11) NOT NULL,
  `Exp` int(11) NOT NULL,
  `Gold` int(11) NOT NULL,
  `Status` varchar(255) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `id_Quests` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  `id_Spells` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  `Movement_Speed` int(11) NOT NULL,
  `id_Statistics` int(11) NOT NULL,
  `fk_Petsid_Pets` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `Username` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `CreationDate` date NOT NULL,
  `AccessType` varchar(255) NOT NULL,
  `Status` varchar(255) NOT NULL,
  `id_User` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `charachters`
--
ALTER TABLE `charachters`
  ADD PRIMARY KEY (`id_Charachters`),
  ADD UNIQUE KEY `fk_Statisticsid_Statistics` (`fk_Statisticsid_Statistics`),
  ADD KEY `Has74` (`fk_Userid_User`),
  ADD KEY `APartOf` (`fk_Factionid_Faction`);

--
-- Indexes for table `class`
--
ALTER TABLE `class`
  ADD PRIMARY KEY (`id_Class`);

--
-- Indexes for table `economy`
--
ALTER TABLE `economy`
  ADD PRIMARY KEY (`id_Economy`);

--
-- Indexes for table `faction`
--
ALTER TABLE `faction`
  ADD PRIMARY KEY (`id_Faction`);

--
-- Indexes for table `friends`
--
ALTER TABLE `friends`
  ADD PRIMARY KEY (`id_Friends`);

--
-- Indexes for table `has`
--
ALTER TABLE `has`
  ADD PRIMARY KEY (`fk_Charachtersid_Charachters`,`fk_Questsid_Quests`);

--
-- Indexes for table `has2`
--
ALTER TABLE `has2`
  ADD PRIMARY KEY (`fk_Spellsid_Spells`,`fk_Petsid_Pets`);

--
-- Indexes for table `has3`
--
ALTER TABLE `has3`
  ADD PRIMARY KEY (`fk_Charachtersid_Charachters`,`fk_Itemsid_Items`);

--
-- Indexes for table `has4`
--
ALTER TABLE `has4`
  ADD PRIMARY KEY (`fk_Userid_User`,`fk_Friendsid_Friends`);

--
-- Indexes for table `has5`
--
ALTER TABLE `has5`
  ADD PRIMARY KEY (`fk_Classid_Class`,`fk_Spellsid_Spells`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`id_Items`);

--
-- Indexes for table `logss`
--
ALTER TABLE `logss`
  ADD PRIMARY KEY (`id_Logs`),
  ADD KEY `Has9` (`fk_Userid_User`),
  ADD KEY `Make9` (`fk_Questsid_Quests`);

--
-- Indexes for table `pets`
--
ALTER TABLE `pets`
  ADD PRIMARY KEY (`id_Pets`),
  ADD KEY `Has4` (`fk_Charachtersid_Charachters`);

--
-- Indexes for table `quests`
--
ALTER TABLE `quests`
  ADD PRIMARY KEY (`id_Quests`);

--
-- Indexes for table `race_info`
--
ALTER TABLE `race_info`
  ADD PRIMARY KEY (`id_Race_Info`);

--
-- Indexes for table `spells`
--
ALTER TABLE `spells`
  ADD PRIMARY KEY (`id_Spells`);

--
-- Indexes for table `statistics`
--
ALTER TABLE `statistics`
  ADD PRIMARY KEY (`id_Statistics`),
  ADD UNIQUE KEY `fk_Petsid_Pets` (`fk_Petsid_Pets`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id_User`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `charachters`
--
ALTER TABLE `charachters`
  ADD CONSTRAINT `APartOf` FOREIGN KEY (`fk_Factionid_Faction`) REFERENCES `faction` (`id_Faction`),
  ADD CONSTRAINT `Has74` FOREIGN KEY (`fk_Userid_User`) REFERENCES `users` (`id_User`);

--
-- Constraints for table `has`
--
ALTER TABLE `has`
  ADD CONSTRAINT `Has3` FOREIGN KEY (`fk_Charachtersid_Charachters`) REFERENCES `charachters` (`id_Charachters`);

--
-- Constraints for table `has2`
--
ALTER TABLE `has2`
  ADD CONSTRAINT `Has1` FOREIGN KEY (`fk_Spellsid_Spells`) REFERENCES `spells` (`id_Spells`);

--
-- Constraints for table `has3`
--
ALTER TABLE `has3`
  ADD CONSTRAINT `Has84` FOREIGN KEY (`fk_Charachtersid_Charachters`) REFERENCES `charachters` (`id_Charachters`);

--
-- Constraints for table `has4`
--
ALTER TABLE `has4`
  ADD CONSTRAINT `Has5` FOREIGN KEY (`fk_Userid_User`) REFERENCES `users` (`id_User`);

--
-- Constraints for table `has5`
--
ALTER TABLE `has5`
  ADD CONSTRAINT `Has7` FOREIGN KEY (`fk_Classid_Class`) REFERENCES `class` (`id_Class`);

--
-- Constraints for table `logss`
--
ALTER TABLE `logss`
  ADD CONSTRAINT `Has9` FOREIGN KEY (`fk_Userid_User`) REFERENCES `users` (`id_User`),
  ADD CONSTRAINT `Make9` FOREIGN KEY (`fk_Questsid_Quests`) REFERENCES `quests` (`id_Quests`);

--
-- Constraints for table `pets`
--
ALTER TABLE `pets`
  ADD CONSTRAINT `Has4` FOREIGN KEY (`fk_Charachtersid_Charachters`) REFERENCES `charachters` (`id_Charachters`);

--
-- Constraints for table `statistics`
--
ALTER TABLE `statistics`
  ADD CONSTRAINT `Has2` FOREIGN KEY (`fk_Petsid_Pets`) REFERENCES `pets` (`id_Pets`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
