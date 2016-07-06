using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuffleStats.DataObjects;
using System.IO;

namespace NuffleStats
{
    class BoardAction
    {
        public string description;


        public string actionDescription = "";
        public string actingPlayerName = "";
        public string rollType = "";
        public string dice = "";
        public string target = "";

        public BoardAction(ReplayReplayStepRulesEventBoardAction boardAction, MatchResult matchResult, bool isCoachChoice)
        {
            if (boardAction.PlayerId != null)
                actingPlayerName = matchResult.GetPlayerFromID(int.Parse(boardAction.PlayerId)).name;

            switch (boardAction.ActionType)
            {
                case "6": actionDescription = "knocked over"; break;
                case "7": actionDescription = "kicks"; break;
                case "8": actionDescription = "ball scatters"; break;
                case "12": actionDescription = "KO recovery"; break;
                case "42": actionDescription = "blocks"; break;
                default: actionDescription = "Unknown action " + boardAction.ActionType; break;
            }

            if (boardAction.Order != null)
            {
                //get cell to and cell from coordinates here
            }

            description = actingPlayerName + " " + actionDescription;

            if (boardAction.Results != null )
            {
                //get dice details here, skills used etc.
                for (int i = 0; i < boardAction.Results.Length; i++)
                {
                    rollType = boardAction.Results[i].RollType;
                    description += " Roll Type: " + rollType;
                    if (boardAction.Results[i].CoachChoices != null)
                    {
                        dice = boardAction.Results[i].CoachChoices[0].ListDices;
                        description += " Dice: " + dice;
                        target = boardAction.Results[i].Requirement;
                        description += " Target: " + target;
                    }

                    
                }

            }

            //rollType = boardAction.Results[0].RollType;
            //dice = boardAction.Results[0].CoachChoices[0].ListDices;
            //if ( boardAction.ActionType == "42")
            //{
            //    //block
            //    //choosing block dice
            //    description = actingPlayerName + " blocks";
                
            //}
            //if (boardAction.ActionType == "1")
            //{
            //    if (isCoachChoice && rollType == "5")
            //    {
            //        description = actingPlayerName + " chooses " + dice;
            //    }
            //    else
            //    {
            //        description = " Dice: " + boardAction.Results[0].CoachChoices[0].ListDices;
            //    }
                                
            //}
            

        }

        public override string ToString()
        {
            //string result = actingPlayerName + " " + actionDescription + " RollType:" + rollType + " Target:" + target + " Dice:" + dice;
            return description;
        }
    }


    class MatchStats
    {
        public MatchStats(Replay replay, MatchResult matchResult)
        {
            ParseReplay(replay, matchResult);

            

        }

        private Dictionary<int, string> replaysteplog;
        private List<string> actionlog;

        private List<string> boardActionLog;

        private SortedDictionary<string, int> actionTypeCounts = new SortedDictionary<string,int>();

        private Point GetCellToCoordinates(ReplayReplayStepRulesEventBoardAction boardAction)
        {
            Cell cell = boardAction.Order[0].CellTo[0];
            Point result = new Point(cell.x,cell.y);
            return result;
        }

        private void ParseReplay( Replay replay, MatchResult matchResult )
        {
            replaysteplog = new Dictionary<int,string>();
            actionlog = new List<string>();
            boardActionLog = new List<string>();

            int i = 0;
            foreach  ( ReplayReplayStep replayStep in replay.ReplayStep)
            {
                

                string eventType = "";
                if ( replayStep.RulesEventEndTurn != null )
                {
                    eventType += "RulesEventEndTurn ";
                    if (replayStep.RulesEventEndTurn[0].Turnover != null)
                        eventType += "Turnover " + replayStep.RulesEventEndTurn[0].Turnover;
                    if (replayStep.RulesEventEndTurn[0].TouchdownScorer != "-1" && replayStep.RulesEventEndTurn[0].TouchdownScorer != null)
                        eventType += "Touchdown Scored by " + matchResult.GetPlayerFromID(int.Parse( replayStep.RulesEventEndTurn[0].TouchdownScorer)).name;

                }
                if ( replayStep.RulesEventBoardAction != null)
                {
                    //BoardState boardState = new BoardState(replayStep.BoardState[0]);
                    //currentPhase doesn't mean anything?
                    eventType += "CurrentPhase: " + replayStep.BoardState[0].CurrentPhase;

                    eventType += " RulesEventBoardAction ";
                    //RequestType is always 1 so ignore RequestType
                    for (int j = 0; j < replayStep.RulesEventBoardAction.Length; j++)
                    {
                        if (replayStep.RulesEventBoardAction[j].ActionType != null)
                        {
                            bool isCoachChoice = replayStep.RulesEventCoachChoice != null;
                            BoardAction ba = new BoardAction(replayStep.RulesEventBoardAction[j], matchResult, isCoachChoice);
                            eventType += ba.ToString();
                            actionlog.Add(ba.ToString());
                            //if (replayStep.RulesEventBoardAction[j].PlayerId != null )
                            //    eventType += matchResult.GetPlayerFromID(int.Parse( replayStep.RulesEventBoardAction[j].PlayerId)).name + ": ";

                            //if (replayStep.RulesEventBoardAction[j].ActionType == "7")
                            //{
                            //    eventType += "Kicks to square ";
                            //    Point p = GetCellToCoordinates(replayStep.RulesEventBoardAction[j]);
                            //    eventType += p.x + "," + p.y;
                            //}
                            //if (replayStep.RulesEventBoardAction[j].ActionType == "8")
                            //{
                            //    eventType += "Ball scatters to ";
                            //    Point p = GetCellToCoordinates(replayStep.RulesEventBoardAction[j]);
                            //    eventType += p.x + "," + p.y;
                            //}
                            //else if (replayStep.RulesEventBoardAction[j].ActionType == "42")
                            //{
                            //    eventType += "Blocks ";
                            //    //eventType += matchResult.GetPlayerFromID(boardState.GetIDOfPlayerAtCell(replayStep.RulesEventBoardAction[j].Order[0].CellFrom[0])).name;
                            //}
                            //else
                            //{
                            //    eventType += " ActionType " + replayStep.RulesEventBoardAction[j].ActionType;
                            //}

                            if (!actionTypeCounts.ContainsKey(replayStep.RulesEventBoardAction[j].ActionType))
                                actionTypeCounts.Add(replayStep.RulesEventBoardAction[j].ActionType, 0);
                            actionTypeCounts[replayStep.RulesEventBoardAction[j].ActionType]++;
                            if (i == 38 || i == 287 || i == 288)
                            {
                                eventType += "";

                            }

                            
                        }
                    }
                
                }
                if ( replayStep.RulesEventGameFinished != null)
                {
                    eventType += "GameOver";
                }
                replaysteplog.Add(i,eventType);
                i++;

                StreamWriter sw = new StreamWriter(@"F:\bloodbowl\BB2Replays\Coach-87421-398df405e5e28b66852e002b29e0fc17_2016-01-27_22_30_57\log.txt");
                foreach (KeyValuePair<int,string> kvp in replaysteplog)
                {
                    sw.WriteLine(kvp.Value);
                }
                sw.Close();
            }
        }

    }
}
