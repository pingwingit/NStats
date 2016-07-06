using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuffleStats.DataObjects
{
    public class BoardState
    {
        public Dictionary<Point, ReplayReplayStepBoardStateListTeamsTeamStateListPitchPlayersPlayerState> playerPositions { get; set; }

        public BoardState(ReplayReplayStepBoardState boardState)
        {
            playerPositions = new Dictionary<Point,ReplayReplayStepBoardStateListTeamsTeamStateListPitchPlayersPlayerState>();

            foreach ( var team in boardState.ListTeams)
            {
                foreach (var player in team.ListPitchPlayers)
                {
                    playerPositions.Add(new Point(player.Cell[0].x, player.Cell[0].y), player);
                }
            }            
        }

        public int GetIDOfPlayerAtCell(Point p)
        {
            int result;

            ReplayReplayStepBoardStateListTeamsTeamStateListPitchPlayersPlayerState ps = null;
            foreach (KeyValuePair<Point,ReplayReplayStepBoardStateListTeamsTeamStateListPitchPlayersPlayerState> kvp in playerPositions)
            {
                if ( kvp.Key.x == p.x && kvp.Key.y == p.y)
                {
                    ps = kvp.Value;
                    break;
                }
            }

            //var ps = playerPositions[p];
            
            result = int.Parse(ps.Id);

            return result;
        }

        public int GetIDOfPlayerAtCell(Cell c)
        {
            return GetIDOfPlayerAtCell(new Point(c.x, c.y));
        }

        public int GetIDOfPlayerAtCell(ReplayReplayStepRulesEventBoardActionOrderCellFrom c)
        {
            return GetIDOfPlayerAtCell(new Point(c.x, c.y));
        }

    }
}
