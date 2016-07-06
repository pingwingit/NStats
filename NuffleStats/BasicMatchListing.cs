using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuffleStats
{
    public class BasicMatchListing
    {
        public string datetime{get;set;}
        public string homeTeam{get;set;}
        public string awayTeam{get;set;}
        public string homeScore{get;set;}
        public string awayScore { get; set; }
        public string replayFile { get; set; }

        public BasicMatchListing(string dt, string dt2, string ht, string at, string hscore, string ascore, string rf)
        {
            datetime = dt;
            homeTeam = ht;
            awayTeam = at;
            homeScore = hscore;
            awayScore = ascore;
            replayFile = rf;

            if ((dt == null || dt == "") && dt2 != "" && dt2 != null) 
                datetime = dt2.Substring(0,19);

            if ((dt == null || dt == "") && (dt2 == null || dt2 == ""))
            {
                datetime = "-";
            }
        }
    }
}
