using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuffleStats
{
    public class MatchResult
    {
        private int GetStat(string stat)
        {
            if (stat != null)
                return int.Parse(stat);
            else
                return 0;
        }

        public string displayMatchHeader { get; set; }
        public string displayMatchResult { get; set; }


        public string homeCoach { get; set; }
        public string awayCoach { get; set; }
        public string homeTeam { get; set; }
        public string awayTeam { get; set; }
        public string homeRace { get; set; }
        public string awayRace { get; set; }

        public int homeScore { get; set; }
        public int awayScore { get; set; }


        public string homeCoachedBy { get; set; }
        public string awayCoachedBy { get; set; }

        public int awayMetresRunning { get; set; }
        public int awayMetresPassing { get; set; }
        public int awayPasses { get; set; }
        public int awayInterceptions { get; set; }  //TODO - find interception stats
        public int awayCatches { get; set; }
        public int awayDeathsInflicted { get; set; }    //TODO - find death stats
        public int awayStunsInflicted { get; set; }
        public int awayKOsInflicted { get; set; }
        public int awayCasInflicted { get; set; }
        public int awayInjuriesInflicted { get; set; }
        public int awaySupporters { get; set; }
        public int awayApothecary { get; set; }
        public int awayCheerleaders { get; set; }
        public int awayAssistantCoaches { get; set; }
        public int awayRerolls { get; set; }
        public int awayTeamValue { get; set; }

        public int homeMetresRunning { get; set; }
        public int homeMetresPassing { get; set; }
        public int homePasses { get; set; }
        public int homeInterceptions { get; set; }  //TODO - find interception stats
        public int homeCatches { get; set; }
        public int homeDeathsInflicted { get; set; }    //TODO - find death stats
        public int homeStunsInflicted { get; set; }
        public int homeKOsInflicted { get; set; }
        public int homeCasInflicted { get; set; }
        public int homeInjuriesInflicted { get; set; }
        public int homeSupporters { get; set; }
        public int homeApothecary { get; set; }
        public int homeCheerleaders { get; set; }
        public int homeAssistantCoaches { get; set; }
        public int homeRerolls { get; set; }
        public int homeTeamValue { get; set; }

        public List<Player> homePlayers { get; set; }
        public List<Player> awayPlayers { get; set; }


        public Dictionary<string,Player> homePlayersStart { get; set; }
        public Dictionary<string, Player> awayPlayersStart { get; set; }

        public Player GetPlayerFromID(int id)
        {
            Player result = null;
            foreach(Player p in homePlayers)
            {
                if (p.ID == id)
                    result = p;
            }
            foreach (Player p in awayPlayers)
            {
                if (p.ID == id)
                    result = p;
            }

            return result;
        }

        private string GetRace(string id)
        {
            string result = "";
            switch (id)
            {
/*
 * unknowns:
 * lizards
 */

                case "1": result += "Humans"; break;
                case "2": result = "Dwarfs"; break;
                case "3": result = "Skaven"; break;
                case "4": result = "Orcs"; break;
                case "5": result = "Lizardmen"; break;
                case "7": result = "Wood Elves"; break;
                case "8": result = "Chaos"; break;
                case "9": result = "Dark Elves"; break;
                case "12": result = "Norse"; break;
                case "15": result = "High Elves"; break;
                case "24": result = "Bretonnians"; break;
                default: result = "UnknownRaceID" + id; break;
            }
            return result;
        }

        public MatchResult(ReplayReplayStepRulesEventGameFinishedMatchResult matchResultXML, ReplayReplayStepBoardStateListTeamsTeamState[] matchStartTeamStatesXML)
        {
            awayMetresRunning = GetStat(matchResultXML.Row[0].AwayInflictedMetersRunning);
            awayMetresPassing = GetStat(matchResultXML.Row[0].AwayInflictedMetersPassing);
            awayPasses = GetStat(matchResultXML.Row[0].AwayInflictedPasses);
            awayCatches = GetStat(matchResultXML.Row[0].AwayInflictedCatches);

            awayStunsInflicted = GetStat(matchResultXML.Row[0].AwayInflictedStuns);
            awayKOsInflicted = GetStat(matchResultXML.Row[0].AwayInflictedKO);
            awayInjuriesInflicted = GetStat(matchResultXML.Row[0].AwayInflictedInjuries);
            awayCasInflicted = GetStat(matchResultXML.Row[0].AwayInflictedCasualties);
            awayDeathsInflicted = GetStat(matchResultXML.Row[0].AwayInflictedDead);

            homeMetresRunning = GetStat(matchResultXML.Row[0].HomeInflictedMetersRunning);
            homeMetresPassing = GetStat(matchResultXML.Row[0].HomeInflictedMetersPassing);
            homePasses = GetStat(matchResultXML.Row[0].HomeInflictedPasses);
            homeCatches = GetStat(matchResultXML.Row[0].HomeInflictedCatches);

            homeStunsInflicted = GetStat(matchResultXML.Row[0].HomeInflictedStuns);
            homeKOsInflicted = GetStat(matchResultXML.Row[0].HomeInflictedKO);
            homeInjuriesInflicted = GetStat(matchResultXML.Row[0].HomeInflictedInjuries);
            homeCasInflicted = GetStat(matchResultXML.Row[0].HomeInflictedCasualties);
            homeDeathsInflicted = GetStat(matchResultXML.Row[0].HomeInflictedDead);

            awaySupporters = GetStat(matchResultXML.Row[0].AwayNbSupporters);
            homeSupporters = GetStat(matchResultXML.Row[0].HomeNbSupporters);

            //GetStat(matchResultXML.Row[0].);

            homeTeam = matchResultXML.Row[0].TeamHomeName;
            awayTeam = matchResultXML.Row[0].TeamAwayName;


            homeRace = GetRace(matchResultXML.Row[0].IdRacesHome);
            awayRace = GetRace(matchResultXML.Row[0].IdRacesAway);

            displayMatchHeader = homeTeam + " V " + awayTeam;

            homeCoach = matchResultXML.Row[0].CoachHomeName;
            awayCoach = matchResultXML.Row[0].CoachAwayName;

            homeCoachedBy = homeRace + " coached by " + homeCoach;
            awayCoachedBy = awayRace + " coached by " + awayCoach; 

            if (matchResultXML.Row[0].HomeScore != null)
                homeScore = int.Parse(matchResultXML.Row[0].HomeScore);
            else
                homeScore = 0;
            if (matchResultXML.Row[0].AwayScore != null)
                awayScore = int.Parse(matchResultXML.Row[0].AwayScore);
            else
                awayScore = 0;

            homeCheerleaders = GetStat(matchResultXML.CoachResults[0].TeamResult[0].TeamData[0].Cheerleaders);
            awayCheerleaders = GetStat(matchResultXML.CoachResults[1].TeamResult[0].TeamData[0].Cheerleaders);
            homeAssistantCoaches = GetStat(matchResultXML.CoachResults[0].TeamResult[0].TeamData[0].CoachSlot);
            awayAssistantCoaches = GetStat(matchResultXML.CoachResults[1].TeamResult[0].TeamData[0].CoachSlot);
            homeApothecary = GetStat(matchResultXML.CoachResults[0].TeamResult[0].TeamData[0].Apothecary);
            awayApothecary = GetStat(matchResultXML.CoachResults[1].TeamResult[0].TeamData[0].Apothecary);
            homeRerolls = GetStat(matchResultXML.CoachResults[0].TeamResult[0].TeamData[0].Reroll);
            awayRerolls = GetStat(matchResultXML.CoachResults[1].TeamResult[0].TeamData[0].Reroll);
            homeTeamValue = GetStat(matchResultXML.CoachResults[0].TeamResult[0].TeamData[0].Value);
            awayTeamValue = GetStat(matchResultXML.CoachResults[1].TeamResult[0].TeamData[0].Value);

            displayMatchResult = homeScore + "    " + awayScore;

            homePlayersStart = new Dictionary<string, Player>();
            for (int i = 0; i < matchStartTeamStatesXML[0].ListPitchPlayers.Length; i++)
            {
                Player p = new Player();
                p.LoadPlayerData(matchStartTeamStatesXML[0].ListPitchPlayers[i]);
                if (p.uniqueID != null) //null values for currently MNG injured players??
                    homePlayersStart.Add(p.uniqueID, p);
            }


            awayPlayersStart = new Dictionary<string, Player>();
            for (int i = 0; i < matchStartTeamStatesXML[1].ListPitchPlayers.Length; i++)
            {
                Player p = new Player();
                p.LoadPlayerData(matchStartTeamStatesXML[1].ListPitchPlayers[i]);
                if (p.uniqueID != null)
                    awayPlayersStart.Add(p.uniqueID, p);
            }

            homePlayers = new List<Player>();
            for (int i = 0; i < matchResultXML.CoachResults[0].TeamResult[0].PlayerResults.Length; i++)
            {
                Player p = new Player();
                p.LoadPlayerData(matchResultXML.CoachResults[0].TeamResult[0].PlayerResults[i], homePlayersStart);
                homePlayers.Add(p);
            }

            awayPlayers = new List<Player>();
            for (int i = 0; i < matchResultXML.CoachResults[1].TeamResult[0].PlayerResults.Length; i++)
            {
                Player p = new Player();
                p.LoadPlayerData(matchResultXML.CoachResults[1].TeamResult[0].PlayerResults[i], awayPlayersStart);
                awayPlayers.Add(p);
            }


            int homeTVofPlayers = homePlayers.Sum(p => p.TV);
            int awayTVofPlayers = awayPlayers.Sum(p => p.TV);
            


        }
    }

    public class Player
    { 
        public int number { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public int MA { get; set; }
        public int ST { get; set; }
        public int AG { get; set; }
        public int AV { get; set; }

        

        public int SPP { get; set; }
        public int TV { get; set; }

        public string skills { get; set; }
        public string injuries { get; set; }

        public int statsMatchesPlayed { get; set; }
        public int statsTouchdowns { get; set; }
        public int statsMVP { get; set; }
        public int statsKOs { get; set; }
        public int statsMetresRunning { get; set; }
        public int statsMetresPassing { get; set; }
        public int statsKills { get; set; }
        public int statsStuns { get; set; }
        public int statsInjuries { get; set; }
        public int statsCasualties { get; set; }

        public string uniqueID { get; set; }
        public int ID { get; set; }

        public bool IsInjured { get; set; }

        public void LoadPlayerData(ReplayReplayStepBoardStateListTeamsTeamStateListPitchPlayersPlayerState playerresult)
        {
            //if (playerresult.PlayerData[0].Number != null)
            //    number = int.Parse(playerresult.PlayerData[0].Number);
            //name = playerresult.PlayerData[0].Name;
            //MA = int.Parse(playerresult.PlayerData[0].Ma);
            //ST = int.Parse(playerresult.PlayerData[0].St);
            //AG = int.Parse(playerresult.PlayerData[0].Ag);
            //AV = int.Parse(playerresult.PlayerData[0].Av);
            ////p.TV = int.Parse(playerresult..PlayerData[0].Value);

            //if (playerresult.Xp != null)
            //    SPP = int.Parse(playerresult.Xp);
            //else
            //    SPP = 0;

            //skills = ParseSkills(playerresult.PlayerData[0].ListSkills);
            //injuries = ParseCasualties(playerresult.PlayerData[0].ListCasualties);
            //position = ParsePosition(playerresult.PlayerData[0].IdPlayerTypes);

            //GetDetailedStats(playerresult);

            uniqueID = playerresult.Data[0].Number;
            injuries = playerresult.Data[0].ListCasualties;

            //FixLonerNames();
            //FixStarPlayerNames();
        }

        public void LoadPlayerData(ReplayReplayStepRulesEventGameFinishedMatchResultCoachResultsCoachResultTeamResultPlayerResultsPlayerResult playerresult, Dictionary<string,Player> rosterAtStart)
        {
            if (playerresult.PlayerData[0].Number != null)
                number = int.Parse(playerresult.PlayerData[0].Number);
            name = playerresult.PlayerData[0].Name;
            MA = int.Parse(playerresult.PlayerData[0].Ma);
            ST = int.Parse(playerresult.PlayerData[0].St);
            AG = int.Parse(playerresult.PlayerData[0].Ag);
            AV = int.Parse(playerresult.PlayerData[0].Av);
            ID = int.Parse(playerresult.PlayerData[0].Id);
            //p.TV = int.Parse(playerresult..PlayerData[0].Value);

            uniqueID = playerresult.PlayerData[0].Number;

            if (playerresult.Xp != null)
                SPP = int.Parse(playerresult.Xp);
            else
                SPP = 0;

            if (playerresult.PlayerData[0].Value != null)
                TV = int.Parse(playerresult.PlayerData[0].Value);
            else
                TV = 0;

            skills = ParseSkills(playerresult.PlayerData[0].ListSkills);

            string startInjuries = "";
            if (uniqueID != null && rosterAtStart.ContainsKey(uniqueID))
                startInjuries = rosterAtStart[uniqueID].injuries;
            
            injuries = ParseCasualties( playerresult.PlayerData[0].ListCasualties, startInjuries );
            if (injuries != "")
                IsInjured = true;
            else
                IsInjured = false;

            position = ParsePosition(playerresult.PlayerData[0].IdPlayerTypes);

            GetDetailedStats(playerresult);

            uniqueID = playerresult.PlayerData[0].LobbyId;

            FixStarPlayerNames();
        }

        private void FixStarPlayerNames()
        {
            /*
             * PLAYERTYPE_TROISQUART
PLAYERTYPE_RECEVEUR
PLAYERTYPE_LANCEUR
PLAYERTYPE_BLITZER
PLAYERTYPE_OGRE
PLAYERTYPE_BLOCKER_LONGBEARD
PLAYERTYPE_COUREUR
PLAYERTYPE_BLITZER
PLAYERTYPE_TROLLSLAYER
PLAYERTYPE_DEATH_ROLLER
PLAYERTYPE_TROISQUART
PLAYERTYPE_LANCEUR
PLAYERTYPE_GUTTERRUNNER
PLAYERTYPE_BLITZER_VERMIN
PLAYERTYPE_RATOGRE
PLAYERTYPE_TROISQUART
PLAYERTYPE_GOBELIN
PLAYERTYPE_LANCEUR
PLAYERTYPE_BLACKORC
PLAYERTYPE_BLITZER
PLAYERTYPE_TROLL
PLAYERTYPE_BEASTMAN
PLAYERTYPE_CHAOSWARRIOR
PLAYERTYPE_MINOTAUR
PLAYER_NAMES_CHAMPION_CHAOS
PLAYER_NAMES_CHAMPION_HUMAIN
PLAYER_NAMES_CHAMPION_NAIN
PLAYER_NAMES_CHAMPION_SKAVEN
PLAYER_NAMES_CHAMPION_ELFE_SYLVAIN
PLAYER_NAMES_CHAMPION_GOBELIN
PLAYER_NAMES_CHAMPION_HOMME_LEZARD
PLAYER_NAMES_CHAMPION_ORC
PLAYERTYPE_TROISQUART
PLAYERTYPE_COUREUR
PLAYERTYPE_ASSASSIN
PLAYERTYPE_BLITZER
PLAYERTYPE_WITCH_ELF
PLAYER_NAMES_CHAMPION_DARK_ELF
PLAYER_NAMES_CHAMPION_MORGNTHORG
PLAYER_NAMES_CHAMPION_UNDEAD
PLAYERTYPE_TROISQUART
PLAYERTYPE_LANCEUR
PLAYERTYPE_RECEVEUR
PLAYERTYPE_BLITZER
PLAYER_NAMES_CHAMPION_ZARATHESLAYER
PLAYER_NAMES_CHAMPION_SCRAPPASOREHEAD
PLAYER_NAMES_CHAMPION_ELDRILSIDEWINDER
PLAYER_NAMES_CHAMPION_LORDBORAKTHEDESPOILER
PLAYER_NAMES_CHAMPION_DEEPROOTSTRONGBRANCH
PLAYER_NAMES_CHAMPION_NEKBREKEREKH
PLAYER_NAMES_CHAMPION_RAMTUTIII
PLAYER_NAMES_CHAMPION_ICEPELTHAMMERBLOW
PLAYER_NAMES_CHAMPION_BOMBERDRIBBLESNOT
PLAYER_NAMES_CHAMPION_ZZHARGMADEYE
PLAYER_NAMES_CHAMPION_MIGHTYZUG
PLAYER_NAMES_CHAMPION_JEARLICE
PLAYER_NAMES_CHAMPION_HUBRISRAKARTH
PLAYER_NAMES_CHAMPION_HAKFLEMSKUTTLESPIKE
PLAYERTYPE_TROISQUART
PLAYERTYPE_BLITZER
PLAYERTYPE_BLOCKER
PLAYER_NAMES_CHAMPION_BARIK
PLAYER_NAMES_CHAMPION_DOLFAR
PLAYER_NAMES_CHAMPION_GROTTY
PLAYER_NAMES_CHAMPION_GLART
PLAYER_NAMES_CHAMPION_MORANION
PLAYER_NAMES_CHAMPION_ROXANNA
PLAYER_NAMES_CHAMPION_SOAREN
PLAYER_NAMES_CHAMPION_BOOMER
PLAYER_NAMES_CHAMPION_FEZGLITCH
PLAYER_NAMES_CHAMPION_FLINT
PLAYER_NAMES_CHAMPION_HELMUT
PLAYER_NAMES_CHAMPION_ITHACA
PLAYER_NAMES_CHAMPION_LEWDGRIP
PLAYER_NAMES_CHAMPION_MAXSPLEENRIPPER
PLAYER_NAMES_CHAMPION_PUGGY
PLAYER_NAMES_CHAMPION_SKITTER
PLAYER_NAMES_CHAMPION_UGROTH
PLAYER_NAMES_CHAMPION_BRICK
EXTRA_REFEREE
PLAYER_NAMES_CHAMPION_JULES
PLAYER_NAMES_CHAMPION_JB
PLAYER_NAMES_CHAMPION_CHAOS_FALLBACK
PLAYER_NAMES_CHAMPION_HUMAIN_FALLBACK
PLAYER_NAMES_CHAMPION_NAIN_FALLBACK
PLAYER_NAMES_CHAMPION_SKAVEN_FALLBACK
PLAYER_NAMES_CHAMPION_ELFE_SYLVAIN_FALLBACK
PLAYER_NAMES_CHAMPION_GOBELIN_FALLBACK
PLAYER_NAMES_CHAMPION_HOMME_LEZARD_FALLBACK
PLAYER_NAMES_CHAMPION_ORC_FALLBACK
PLAYER_NAMES_CHAMPION_DARK_ELF_FALLBACK
PLAYER_NAMES_CHAMPION_MORGNTHORG_FALLBACK
PLAYER_NAMES_CHAMPION_UNDEAD_FALLBACK
PLAYER_NAMES_CHAMPION_ZARATHESLAYER_FALLBACK
PLAYER_NAMES_CHAMPION_SCRAPPASOREHEAD_FALLBACK
PLAYER_NAMES_CHAMPION_ELDRILSIDEWINDER_FALLBACK
PLAYER_NAMES_CHAMPION_LORDBORAKTHEDESPOILER_FALLBACK
PLAYER_NAMES_CHAMPION_DEEPROOTSTRONGBRANCH_FALLBACK
PLAYER_NAMES_CHAMPION_NEKBREKEREKH_FALLBACK
PLAYER_NAMES_CHAMPION_RAMTUTIII_FALLBACK
PLAYER_NAMES_CHAMPION_ICEPELTHAMMERBLOW_FALLBACK
PLAYER_NAMES_CHAMPION_BOMBERDRIBBLESNOT_FALLBACK
PLAYER_NAMES_CHAMPION_ZZHARGMADEYE_FALLBACK
PLAYER_NAMES_CHAMPION_MIGHTYZUG_FALLBACK
PLAYER_NAMES_CHAMPION_JEARLICE_FALLBACK
PLAYER_NAMES_CHAMPION_HUBRISRAKARTH_FALLBACK
PLAYER_NAMES_CHAMPION_HAKFLEMSKUTTLESPIKE_FALLBACK
PLAYER_NAMES_CHAMPION_BARIK_FALLBACK
PLAYER_NAMES_CHAMPION_DOLFAR_FALLBACK
PLAYER_NAMES_CHAMPION_GROTTY_FALLBACK
PLAYER_NAMES_CHAMPION_GLART_FALLBACK
PLAYER_NAMES_CHAMPION_MORANION_FALLBACK
PLAYER_NAMES_CHAMPION_ROXANNA_FALLBACK
PLAYER_NAMES_CHAMPION_SOAREN_FALLBACK
PLAYER_NAMES_CHAMPION_BOOMER_FALLBACK
PLAYER_NAMES_CHAMPION_FEZGLITCH_FALLBACK
PLAYER_NAMES_CHAMPION_FLINT_FALLBACK
PLAYER_NAMES_CHAMPION_HELMUT_FALLBACK
PLAYER_NAMES_CHAMPION_ITHACA_FALLBACK
PLAYER_NAMES_CHAMPION_LEWDGRIP_FALLBACK
PLAYER_NAMES_CHAMPION_MAXSPLEENRIPPER_FALLBACK
PLAYER_NAMES_CHAMPION_PUGGY_FALLBACK
PLAYER_NAMES_CHAMPION_SKITTER_FALLBACK
PLAYER_NAMES_CHAMPION_UGROTH_FALLBACK
PLAYER_NAMES_CHAMPION_BRICK_FALLBACK
PLAYER_NAMES_CHAMPION_JULES_FALLBACK
PLAYER_NAMES_CHAMPION_JB_FALLBACK
             */
            //TODO Finish this...
            switch (name)
            {
                case "PLAYER_NAMES_CHAMPION_MORGNTHORG": name = "Morg N'Thorg"; break;
                case "PLAYER_NAMES_CHAMPION_NAIN": name = "Grim Ironjaw"; break;
                case "PLAYER_NAMES_CHAMPION_ORC": name = "Varag Ghoul-Chewer"; break;
                case "PLAYER_NAMES_CHAMPION_BARIK": name = "Barik Farblast"; break;
                case "PLAYER_NAMES_CHAMPION_DOLFAR": name = "Dolfar Longstride"; break;
                case "PLAYER_NAMES_CHAMPION_GROTTY": name = "Grotty"; break;
                case "PLAYER_NAMES_CHAMPION_GLART": name = "Glart Smashrip Jr."; break;
                case "PLAYER_NAMES_CHAMPION_MORANION": name = "Prince Moranion"; break;
                case "PLAYER_NAMES_CHAMPION_ROXANNA": name = "Roxanna Darknail"; break;
                case "PLAYER_NAMES_CHAMPION_SOAREN": name = "Soaren Hightower"; break;
                case "PLAYER_NAMES_CHAMPION_BOOMER": name = "Boomer Eziasson"; break;
                case "PLAYER_NAMES_CHAMPION_FEZGLITCH": name = "Fezglitch"; break;
                case "PLAYER_NAMES_CHAMPION_FLINT": name = "Flint Churnblade"; break;
                case "PLAYER_NAMES_CHAMPION_HELMUT": name = "Helmut Wulf"; break;
                case "PLAYER_NAMES_CHAMPION_ITHACA": name = "Ithaca Benoin"; break;
                case "PLAYER_NAMES_CHAMPION_LEWDGRIP": name = "Lewdgrip Whiparm"; break;
                case "PLAYER_NAMES_CHAMPION_MAXSPLEENRIPPER": name = "Max Spleenripper"; break;
                case "PLAYER_NAMES_CHAMPION_PUGGY": name = "Puggy Baconbreath"; break;
                case "PLAYER_NAMES_CHAMPION_SKITTER": name = "Skitter Stab-Stab"; break;
                case "PLAYER_NAMES_CHAMPION_UGROTH": name = "Ugroth Bolgrot"; break;
                case "PLAYER_NAMES_CHAMPION_BRICK": name = "Brick Far'th"; break;
                //case "PLAYER_NAMES_CHAMPION_JULES
                //case "PLAYER_NAMES_CHAMPION_JB
                //case "PLAYER_NAMES_CHAMPION_CHAOS_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_HUMAIN_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_NAIN_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_SKAVEN_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_ELFE_SYLVAIN_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_GOBELIN_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_HOMME_LEZARD_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_ORC_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_DARK_ELF_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_MORGNTHORG_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_UNDEAD_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_ZARATHESLAYER_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_SCRAPPASOREHEAD_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_ELDRILSIDEWINDER_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_LORDBORAKTHEDESPOILER_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_DEEPROOTSTRONGBRANCH_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_NEKBREKEREKH_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_RAMTUTIII_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_ICEPELTHAMMERBLOW_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_BOMBERDRIBBLESNOT_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_ZZHARGMADEYE_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_MIGHTYZUG_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_JEARLICE_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_HUBRISRAKARTH_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_HAKFLEMSKUTTLESPIKE_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_BARIK_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_DOLFAR_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_GROTTY_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_GLART_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_MORANION_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_ROXANNA_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_SOAREN_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_BOOMER_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_FEZGLITCH_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_FLINT_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_HELMUT_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_ITHACA_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_LEWDGRIP_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_MAXSPLEENRIPPER_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_PUGGY_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_SKITTER_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_UGROTH_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_BRICK_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_JULES_FALLBACK
                //case "PLAYER_NAMES_CHAMPION_JB_FALLBACK    
                case "PLAYER_NAMES_CHAMPION_ZARATHESLAYER": name = "Zara The Slayer"; break;
                //case "PLAYER_NAMES_CHAMPION_SCRAPPASOREHEAD
                case "PLAYER_NAMES_CHAMPION_ELDRILSIDEWINDER": name = "Eldril Sidewinder"; break;
                //case "PLAYER_NAMES_CHAMPION_LORDBORAKTHEDESPOILER
                //case "PLAYER_NAMES_CHAMPION_DEEPROOTSTRONGBRANCH
                //case "PLAYER_NAMES_CHAMPION_NEKBREKEREKH
                //case "PLAYER_NAMES_CHAMPION_RAMTUTIII
                case "PLAYER_NAMES_CHAMPION_ICEPELTHAMMERBLOW": name = "Icepelt Hammerblow"; break;
                case "PLAYER_NAMES_CHAMPION_BOMBERDRIBBLESNOT": name = "Bomber Dribblesnot"; break;
                case "PLAYER_NAMES_CHAMPION_ZZHARGMADEYE": name = "Zzharg Madeye"; break;
                case "PLAYER_NAMES_CHAMPION_MIGHTYZUG": name = "Mighty Zug"; break;
                case "PLAYER_NAMES_CHAMPION_JEARLICE": name = "J Earlice"; break;
                case "PLAYER_NAMES_CHAMPION_HUBRISRAKARTH": name = "Hubris Rakarth"; break;
                case "PLAYER_NAMES_CHAMPION_HAKFLEMSKUTTLESPIKE": name = "Hakflem Skuttlespike"; break;
                case "PLAYER_NAMES_CHAMPION_CHAOS": name = "Grashnak Blackhoof"; break;
                case "PLAYER_NAMES_CHAMPION_HUMAIN": name = "Griff Oberwald"; break;
                //case "PLAYER_NAMES_CHAMPION_SKAVEN
                case "PLAYER_NAMES_CHAMPION_ELFE_SYLVAIN": name = "Jordell Freshbreeze"; break;
                case "PLAYER_NAMES_CHAMPION_GOBELIN": name = "Ripper"; break;
                //case "PLAYER_NAMES_CHAMPION_HOMME_LEZARD
                //case "PLAYERTYPE_TROISQUART
                //case "PLAYERTYPE_RECEVEUR
                //case "PLAYERTYPE_LANCEUR
                //case "PLAYERTYPE_BLITZER
                //case "PLAYERTYPE_OGRE
                //case "PLAYERTYPE_BLOCKER_LONGBEARD": name = "Loner Dwarf Longbeard"; break;
                //case "PLAYERTYPE_COUREUR
                //case "PLAYERTYPE_BLITZER
                //case "PLAYERTYPE_TROLLSLAYER
                //case "PLAYERTYPE_DEATH_ROLLER
                //case "PLAYERTYPE_TROISQUART
                //case "PLAYERTYPE_LANCEUR
                //case "PLAYERTYPE_GUTTERRUNNER
                //case "PLAYERTYPE_BLITZER_VERMIN
                //case "PLAYERTYPE_RATOGRE
                //case "PLAYERTYPE_TROISQUART
                //case "PLAYERTYPE_GOBELIN
                //case "PLAYERTYPE_LANCEUR
                //case "PLAYERTYPE_BLACKORC
                //case "PLAYERTYPE_BLITZER
                //case "PLAYERTYPE_TROLL
                //case "PLAYERTYPE_BEASTMAN
                //case "PLAYERTYPE_CHAOSWARRIOR
                //case "PLAYERTYPE_MINOTAUR
                //case "PLAYERTYPE_TROISQUART
                //case "PLAYERTYPE_COUREUR
                //case "PLAYERTYPE_ASSASSIN
                //case "PLAYERTYPE_BLITZER
                //case "PLAYERTYPE_WITCH_ELF
                case "PLAYER_NAMES_CHAMPION_DARK_ELF": name = "Horkon Heartripper"; break;
                //case "PLAYER_NAMES_CHAMPION_UNDEAD
                //case "PLAYERTYPE_TROISQUART
                //case "PLAYERTYPE_LANCEUR
                //case "PLAYERTYPE_RECEVEUR
                //case "PLAYERTYPE_BLITZER
                //case "PLAYERTYPE_TROISQUART
                //case "PLAYERTYPE_BLITZER
                //case "PLAYERTYPE_BLOCKER
                default: break;
            }

            if (name.StartsWith("PLAYERTYPE_"))
                name = "Loner " + this.position;
        }

        

        private int GetStat(string stat)
        {
            if (stat != null)
                return int.Parse(stat);
            else
                return 0;
        }

        public void GetDetailedStats(ReplayReplayStepRulesEventGameFinishedMatchResultCoachResultsCoachResultTeamResultPlayerResultsPlayerResult playerresult)
        {
            statsMatchesPlayed = GetStat(playerresult.Statistics[0].MatchPlayed);
            statsTouchdowns = GetStat(playerresult.Statistics[0].InflictedTouchdowns);
            statsMVP = GetStat(playerresult.Statistics[0].MVP);
            statsKOs = GetStat(playerresult.Statistics[0].InflictedKO);
            statsMetresPassing = GetStat(playerresult.Statistics[0].InflictedMetersPassing);
            statsMetresRunning = GetStat(playerresult.Statistics[0].InflictedMetersRunning);
            statsCasualties = GetStat(playerresult.Statistics[0].InflictedCasualties);
            statsInjuries = GetStat(playerresult.Statistics[0].InflictedInjuries);
            statsKills = GetStat(playerresult.Statistics[0].InflictedDead);
        }

        public string ParsePosition(string pos)
        {
            string result = "";
            if (pos == null)
                return result;

            /*1	Human_Lineman
2	Human_Catcher
3	Human_Thrower
4	Human_Blitzer
5	Human_Ogre
6	Dwarf_Blocker
7	Dwarf_Runner
8	Dwarf_Blitzer
9	Dwarf_TrollSlayer
10	Dwarf_DeathRoller
16	Skaven_Lineman
17	Skaven_Thrower
18	Skaven_GutterRunner
19	Skaven_Blitzer
20	Skaven_RatOgre
21	Orc_Lineman
22	Orc_Goblin
23	Orc_Thrower
24	Orc_BlackOrcBlocker
25	Orc_Blitzer
26	Orc_Troll
32	Chaos_Beastman
33	Chaos_Warrior
34	Chaos_Minotaur
36	StarPlayer_GrashnakBlackhoof
37	StarPlayer_GriffOberwald
38	StarPlayer_GrimIronjaw
39	StarPlayer_Headsplitter
40	StarPlayer_JordellFreshbreeze
41	StarPlayer_Ripper
42	StarPlayer_Slibli
43	StarPlayer_VaragGhoulChewer
47	DarkElf_Lineman
48	DarkElf_Runner
49	DarkElf_Assassin
50	DarkElf_Blitzer
51	DarkElf_WitchElf
52	StarPlayer_HorkonHeartripper
53	StarPlayer_MorgNThorg
59	StarPlayer_CountLuthorvonDrakenborg
77	HighElf_Lineman
78	HighElf_Thrower
79	HighElf_Catcher
80	HighElf_Blitzer
99	StarPlayer_ZaraTheSlayer
100	StarPlayer_ScrappaSorehead
101	StarPlayer_EldrilSidewinder
102	StarPlayer_LordBorakTheDespoiler
103	StarPlayer_DeeprootStrongbranch
104	StarPlayer_Setekh
105	StarPlayer_RamtutIII
106	StarPlayer_IcepeltHammerblow
133	StarPlayer_BomberDribblesnot
134	StarPlayer_ZzhargMadeye
135	StarPlayer_MightyZug
136	StarPlayer_JEarlice
137	StarPlayer_HubrisRakarth
138	StarPlayer_HakflemSkuttlespike
139	Bretonnia_Lineman
140	Bretonnia_Blitzer
141	Bretonnia_Blocker
146	StarPlayer_BarikFarblast
147	StarPlayer_DolfarLongstride
148	StarPlayer_Grotty
149	StarPlayer_GlartSmashripJr
150	StarPlayer_PrinceMoranion
151	StarPlayer_RoxannaDarknail
152	StarPlayer_SoarenHightower
153	StarPlayer_BoomerEziasson
154	StarPlayer_Fezglitch
155	StarPlayer_FlintChurnblade
156	StarPlayer_HelmutWulf
157	StarPlayer_IthacaBenoin
158	StarPlayer_LewdgripWhiparm
160	StarPlayer_MaxSpleenripper
161	StarPlayer_PuggyBaconbreath
162	StarPlayer_SkitterStabStab
163	StarPlayer_UgrothBolgrot
164	StarPlayer_BrickFarth
165	Extra_Referee
166	StarPlayer_JulesArnoud
167	StarPlayer_JeanBaptisteDuFer
214	StarPlayer_GrashnakBlackhoof_Fallback
215	StarPlayer_GriffOberwald_Fallback
216	StarPlayer_GrimIronjaw_Fallback
217	StarPlayer_Headsplitter_Fallback
218	StarPlayer_JordellFreshbreeze_Fallback
219	StarPlayer_Ripper_Fallback
220	StarPlayer_Slibli_Fallback
221	StarPlayer_VaragGhoulChewer_Fallback
222	StarPlayer_HorkonHeartripper_Fallback
223	StarPlayer_MorgNThorg_Fallback
224	StarPlayer_CountLuthorvonDrakenborg_Fallback
225	StarPlayer_ZaraTheSlayer_Fallback
226	StarPlayer_ScrappaSorehead_Fallback
227	StarPlayer_EldrilSidewinder_Fallback
228	StarPlayer_LordBorakTheDespoiler_Fallback
229	StarPlayer_DeeprootStrongbranch_Fallback
230	StarPlayer_Setekh_Fallback
231	StarPlayer_RamtutIII_Fallback
232	StarPlayer_IcepeltHammerblow_Fallback
233	StarPlayer_BomberDribblesnot_Fallback
234	StarPlayer_ZzhargMadeye_Fallback
235	StarPlayer_MightyZug_Fallback
236	StarPlayer_JEarlice_Fallback
237	StarPlayer_HubrisRakarth_Fallback
238	StarPlayer_HakflemSkuttlespike_Fallback
239	StarPlayer_BarikFarblast_Fallback
240	StarPlayer_DolfarLongstride_Fallback
241	StarPlayer_Grotty_Fallback
242	StarPlayer_GlartSmashripJr_Fallback
243	StarPlayer_PrinceMoranion_Fallback
244	StarPlayer_RoxannaDarknail_Fallback
245	StarPlayer_SoarenHightower_Fallback
246	StarPlayer_BoomerEziasson_Fallback
247	StarPlayer_Fezglitch_Fallback
248	StarPlayer_FlintChurnblade_Fallback
249	StarPlayer_HelmutWulf_Fallback
250	StarPlayer_IthacaBenoin_Fallback
251	StarPlayer_LewdgripWhiparm_Fallback
252	StarPlayer_MaxSpleenripper_Fallback
253	StarPlayer_PuggyBaconbreath_Fallback
254	StarPlayer_SkitterStabStab_Fallback
255	StarPlayer_UgrothBolgrot_Fallback
256	StarPlayer_BrickFarth_Fallback
258	StarPlayer_JulesArnoud_Fallback
259	StarPlayer_JeanBaptisteDuFer_Fallback
260	Extra_Bob
261	Extra_Cheerleaders
262	Extra_Cabalcopter
263	Extra_Assistant
264	Extra_JimCabal
265	Extra_BobCabal
266	Extra_Public
267	Extra_GoblinCabal
             * */

            switch (pos)
            {
                case "1": result = "Human Lineman"; break;
                case "2": result = "Human Catcher"; break;
                case "3": result = "Human Thrower"; break;
                case "4": result = "Human Blizer"; break;
                case "5": result = "Ogre"; break;                   
                case "6": result = "Dwarf Longbeard"; break;
                case "7": result = "Dwarf Runner"; break;
                case "8": result = "Dwarf Blitzer"; break;
                case "9": result = "Dwarf Troll Slayer"; break;
                case "10": result = "Dwarf Death Roller"; break;
                case "11": result = "Wood Elf Lineman"; break;
                case "12": result = "Wood Elf Catcher"; break;
                case "13": result = "Wood Elf Thrower"; break;
                case "14": result = "Wood Elf Wardancer"; break;
                case "15": result = "Treeman"; break;
                case "16": result = "Skaven Lineman"; break;
                case "17": result = "Skaven Thrower"; break;
                case "18": result = "Skaven GutterRunner"; break;
                case "19": result = "Skaven Blitzer"; break;
                case "20": result = "Skaven RatOgre"; break;
                case "21": result = "Orc Lineman"; break;
                case "22": result = "Goblin"; break;
                case "23": result = "Orc Thrower"; break;
                case "24": result = "Black Orc"; break;
                case "25": result = "Orc Blitzer"; break;
                case "26": result = "Troll"; break;
                case "27": result = "Skink"; break;
                case "28": result = "Saurus"; break;
                case "29": result = "Krox"; break;
                case "32": result = "Beastman"; break;
                case "33": result = "Chaos Warrior"; break;
                case "34": result = "Chaos Minotaur"; break;
                case "47": result = "Dark Elf Lineman"; break;
                case "48": result = "Dark Elf Runner"; break;
                case "50": result = "Dark Elf Blitzer"; break;
                case "51": result = "Witch Elf"; break;
                case "62": result = "Norse Lineman"; break;
                case "63": result = "Norse Thrower"; break;
                case "64": result = "Norse Runner"; break;
                case "65": result = "Norse Berserker"; break;
                case "66": result = "Norse Ulfwerener"; break;
                case "67": result = "Yhetee"; break;
                case "77": result = "High Elf Lineman"; break;
                case "78": result = "High Elf Thrower"; break;
                case "79": result = "High Elf Catcher"; break;
                case "80": result = "High Elf Blitzer"; break;
                case "139": result = "Brettonian Blocker"; break;
                case "140": result = "Brettonian Blitzer"; break;
                case "141": result = "Bretonian Peasant"; break;


                case "53": result = "Star Player"; break;
                case "38": result = "Star Player"; break;

                default: result += "POS" + pos; break;
            }            


            return result;
        }

        public string ParseCasualties(string cas, string startCas)
        {
            string result = "";
            if (cas == null)
                return result;

            Dictionary<string, int> startingInjuries = new Dictionary<string, int>();
            if (startCas != null)
            {
                startCas = startCas.Replace("(", "").Replace(")", "");
                if (startCas.Trim() != "")
                {
                    string[] casualties = startCas.Split(',');
                    foreach (string casualty in casualties)
                    {
                        if (!startingInjuries.ContainsKey(casualty))
                        {
                            startingInjuries.Add(casualty, 0);
                        }
                        startingInjuries[casualty]++;
                    }
                }
            }
       

            cas = cas.Replace("(", "").Replace(")", "");
            if (cas.Trim() != "")
            {

                string[] casualties = cas.Split(',');
                /*
                 * 
1	16	549		50	0	0	Commotion
2	828	550		52	0	1	Cotes cassées broken ribs
3	827	550		54	0	1	Ligaments arrachés  torn ligaments
4	826	550		56	0	1	oeil crevé  punctured eye
5	825	550		58	0	1	marchoîre Fracassées  marchoîre smashed?
6	824	550		60	0	1	bras fracturés  fractured arm
7	30	550		62	0	1	jambe cassée  broken leg
8	17	550		64	0	1	main écrasée  crushed hand
9	20	550		67	0	1	nerf coincé  pinched nerve
10	18	551		69	1	1	dos abimé  back damaged
11	19	551		71	1	1	genou déboité  dislocated knee
12	21	552	1	73	0	1	hanche démise  dislocated hip
13	22	552	1	75	0	1	cheville détruite  destroyed ankle
14	25	554	4	77	0	1	commotion grave  severe concussion
15	26	554	4	79	0	1	traumatisme cranien   head trauma
16	24	553	3	81	0	1	cou brisé  broken neck
17	23	555	2	84	0	1	clavicule défoncée  battered collarbone
18	27	556		100	0	0	mort
                 * */

                foreach (string casualty in casualties)
                {
                    if ( startingInjuries.ContainsKey(casualty) )
                    {
                        startingInjuries[casualty]--;
                        if (startingInjuries[casualty] == 0)
                            startingInjuries.Remove(casualty);
                        continue;
                    }

                    switch (casualty)
                    {
                        case "1": result += "Badly Hurt, "; break;
                        case "2": result += "Broken Ribs, "; break;
                        case "3": result += "Groin Strain, "; break;
                        case "4": result += "Gouged Eye, "; break;
                        case "5": result += "Broken Jaw, "; break;
                        case "6": result += "Fractured Arm, "; break;
                        case "7": result += "Fractured Leg, "; break;
                        case "8": result += "Smashed Hand, "; break;
                        case "9": result += "Pinched Nerve, "; break;
                        case "10": result += "Damaged Back (Niggle), "; break;
                        case "11": result += "Smashed Knee (Niggle), "; break;
                        case "12": result += "Smashed Hip (-MA), "; break;
                        case "13": result += "Smashed Ankle (-MA), "; break;
                        case "14": result += "Serious Concussion (-AV), "; break;
                        case "15": result += "Fractured Skull (-AV), "; break;
                        case "16": result += "Broken Neck (-AG), "; break;
                        case "17": result += "Smashed Collar Bone (-ST), "; break;
                        case "18": result += "Dead, "; break;
                        default: result += "INJURY" + casualty + ", "; break;
                    }

                }
            }


            return result.Trim().Trim(',');
        }

        public string ParseSkills(string sk)
        {
            
            string result = "";

            if (sk == null)
                return result;

            sk = sk.Replace("(","").Replace(")","");
            if (sk.Trim() != "")
            {

                string[] skills = sk.Split(',');


                foreach (string skill in skills)
                {
                    switch (skill)
                    {
                        case "1": result += "Strip Ball, "; break;
                        case "2": result += "+ST, "; break;    //+ST?
                        case "3": result += "+AG, "; break;
                        case "4": result += "+MA, "; break;
                        case "5": result += "+AV, "; break;
                        case "6": result += "Catch, "; break;
                        case "7": result += "Dodge, "; break;
                        case "8": result += "Sprint, "; break;
                        case "9": result += "Pass Block, "; break;
                        case "10": result += "Foul Appearance, "; break;
                        case "11": result += "Leap, "; break;
                        case "12": result += "Extra Arms, "; break;
                        case "13": result += "Mighty Blow, "; break;
                        case "14": result += "Leader, "; break;
                        case "15": result += "Horns, "; break;
                        case "16": result += "Two Heads, "; break;
                        case "17": result += "Stand Firm, "; break;
                        case "18": result += "Always Hungry, "; break;
                        case "19": result += "Regeneration, "; break;
                        case "20": result += "Take Root, "; break;
                        case "21": result += "Accurate, "; break;
                        case "22": result += "Break Tackle, "; break;
                        case "23": result += "Sneaky Git, "; break;
                            //24 empty
                        case "25": result += "Chainsaw, "; break;
                        case "26": result += "Dauntless, "; break;
                        case "27": result += "Dirty Player, "; break;
                        case "28": result += "Diving Catch, "; break;
                        case "29": result += "Dump Off, "; break;
                        case "30": result += "Block, "; break;
                        case "31": result += "Bone Head, "; break;
                        case "32": result += "Very Long Legs, "; break;
                        case "33": result += "Disturbing Presence, "; break;
                        case "34": result += "Diving Tackle, "; break;
                        case "35": result += "Fend, "; break;
                        case "36": result += "Frenzy, "; break;
                        case "37": result += "Grab, "; break;
                        case "38": result += "Guard, "; break;
                        case "39": result += "Hail Mary Pass, "; break;
                        case "40": result += "Juggernaut, "; break;
                        case "41": result += "Jump Up, "; break;
                            //empty slot?
                            //empty slot?
                        case "44": result += "Loner, "; break;
                        case "45": result += "Nerves Of Steel, "; break;
                        case "46": result += "No Hands, "; break;
                        case "47": result += "Pass, "; break;
                        case "48": result += "Piling On, "; break;
                        case "49": result += "Prehensile Tail, "; break;
                        case "50": result += "Pro, "; break;
                        case "51": result += "Really Stupid, "; break;
                        case "52": result += "Right Stuff, "; break;
                        case "53": result += "Safe Throw, "; break;
                        case "54": result += "Secret Weapon, "; break;
                        case "55": result += "Shadowing, "; break;
                        case "56": result += "Side Step, "; break;
                        case "57": result += "Tackle, "; break;
                        case "58": result += "Strong Arm, "; break;
                        case "59": result += "Stunty, "; break;
                        case "60": result += "Sure Feet, "; break;
                        case "61": result += "Sure Hands, "; break;
                            //62 empty 
                        case "63": result += "Thick Skull, "; break;
                        case "64": result += "Throw Team Mate, "; break;
                            //65 empty
                            //66 empty
                        case "67": result += "Wild Animal, "; break;
                        case "68": result += "Wrestle, "; break;
                        case "69": result += "Tentacles, "; break;
                        case "70": result += "Multiple Block, "; break;
                        case "71": result += "Kick, "; break;
                        case "72": result += "Kick Off Return, "; break;
//73 empty
                        case "74": result += "Big Hand, "; break;
                        case "75": result += "Claw, "; break;
                        case "76": result += "Ball And Chain, "; break;
                        case "77": result += "Stab, "; break;
                        case "78": result += "Hypnotic Gaze, "; break;
                        case "79": result += "Stakes, "; break;
                        case "80": result += "Bombardier, "; break;
                        case "81": result += "Decay, "; break;
                        case "82": result += "Nurgle's Rot, "; break;
                        case "83": result += "Titchy, "; break;
                        case "84": result += "Blood Lust, "; break;
                        case "85": result += "Fan Favourite, "; break;
                        case "86": result += "Animosity, "; break;
                        default: result += "SKILL" + skill + ", "; break;
                    }

                }
            }

        
            return result.Trim().Trim(',');
        }
    }
}
