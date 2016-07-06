using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.IO.Compression;

namespace NuffleStats
{
    public class DataLayer 
    {
        public BasicMatchListing selectedMatch { get; set; }


        public ReplayIndexxml replayIndexXML = null;
        string status = "";

        private List<BasicMatchListing> _matchList = null;
        public List<BasicMatchListing> matchList
        {
            get { return _matchList; }
            set 
            { 
                _matchList = value;
                //RaisePropertyChanged("matchList");
            }
            
        }

        public DataLayer()
        {
            App.logger.LogMessage("Initializing DataLayer");
            LoadIndexXML();
        }

        private string[] GetProfileFolders()
        {
            App.logger.LogMessage("Getting Profile Folders");
            return Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BB_WPFTest\Profiles");
        }

        public Replay replay { get; set; }
        public ReplayReplayStepRulesEventGameFinishedMatchResult matchResultXML { get; set; }

        public MatchResult matchResult;

        public void LoadReplayXML(string zipFilePath)
        {
            App.logger.LogMessage("Parsing Replay File: " + zipFilePath);

            XmlSerializer serializer = new XmlSerializer(typeof(Replay));

            string filename = @"F:\bloodbowl\BB2Replays\Coach-90995-a69d4914f53cb5244bf08ecdaf0872f6_2015-12-31_15_34_04\Replay.xml";
            //string filename = Environment.SpecialFolder.MyDocuments + @"\BloodBowl2\Profiles\3EAD1B73DBA10132713B8B56C1675837\Replays\ReplayIndex.xml";
            status = "About to read XML replay file";

            var za = ZipFile.OpenRead(zipFilePath+".bbrz");
           
            foreach (var entry in za.Entries)
            {
                using (var r = new StreamReader(entry.Open()))
                {
                   // using (Stream reader = new FileStream(r, FileMode.Open))
                   // {
                   //     status = "About to deserialize XML replay";
                        // Call the Deserialize method to restore the object's state.
                    replay = (Replay)serializer.Deserialize(r);
                    //}

                    
                    matchResultXML = replay.ReplayStep[replay.ReplayStep.Length - 1].RulesEventGameFinished[0];
                    ReplayReplayStepBoardStateListTeamsTeamState[] matchStartTeamStatesXML = replay.ReplayStep[0].BoardState[0].ListTeams;
                    matchResult = new MatchResult(matchResultXML, matchStartTeamStatesXML);

                    //MatchStats matchStats = new MatchStats(replay, matchResult);

                }
             }
            
           
        }



        public void LoadIndexXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ReplayIndexxml));
            XmlSerializer serializerOldFormat = new XmlSerializer(typeof(ReplayIndexOldFormat));
            XmlSerializer serializerOldFormatV2 = new XmlSerializer(typeof(ReplayIndexOldFormatV2));
            matchList = new List<BasicMatchListing>();

            //string filename = @"C:\Users\Andy\Documents\BloodBowl2\Profiles\3EAD1B73DBA10132713B8B56C1675837\Replays\ReplayIndex.xml";
            //string filename = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ) + @"\BloodBowl2\Profiles\3EAD1B73DBA10132713B8B56C1675837\Replays\ReplayIndex.xml";
            status = "About to read XML index file";

            

            foreach (string profileFolder in GetProfileFolders())
            {
                try
                {
                    replayIndexXML = null;

                    if (File.Exists(profileFolder + @"\Replays\ReplayIndex.xml"))
                    {
                        App.logger.LogMessage("Parsing Replay Index: " + profileFolder + @"\Replays\ReplayIndex.xml");

                        using (Stream reader = new FileStream(profileFolder + @"\Replays\ReplayIndex.xml", FileMode.Open))
                        {
                            status = "About to deserialize XML index";
                            //int x = int.Parse(status);
                            // Call the Deserialize method to restore the object's state.

                            byte[] buffer = new byte[20];

                            reader.Read(buffer, 0, 20);
                            reader.Position = 0;

                            string startOfFile = System.Text.Encoding.Default.GetString(buffer);

                            if (startOfFile.Contains("<ReplayIndexFile>"))
                                replayIndexXML = (ReplayIndexOldFormat)serializerOldFormat.Deserialize(reader);
                            else if (startOfFile.Contains("<ReplayIndex>"))
                                replayIndexXML = (ReplayIndexOldFormatV2)serializerOldFormatV2.Deserialize(reader);
                            else
                                replayIndexXML = (ReplayIndexxml)serializer.Deserialize(reader);  
                            
                        }

                        matchList = GetMatchListing(matchList, profileFolder + @"\Replays\");
                    }
                }
                catch (Exception doh)
                {
                    //properly handle error here
                    App.logger.LogMessage("Failed to parse index file: " + doh.ToString());
                }

                //if (replayIndexXML == null )
                //{
                //    try
                //    {
                //        if (File.Exists(profileFolder + @"\Replays\ReplayIndex.xml"))
                //        {
                //            App.logger.LogMessage("Parsing Replay Index as old format xml: " + profileFolder + @"\Replays\ReplayIndex.xml");

                //            using (Stream reader = new FileStream(profileFolder + @"\Replays\ReplayIndex.xml", FileMode.Open))
                //            {
                //                status = "About to deserialize XML index";
                //                //int x = int.Parse(status);
                //                // Call the Deserialize method to restore the object's state.
                //                replayIndexXML = (ReplayIndexOldFormat)serializerOldFormat.Deserialize(reader);
                //            }

                //            matchList = GetMatchListing(matchList, profileFolder + @"\Replays\");
                //        }
                //    }
                //    catch (Exception doh)
                //    {
                //        //properly handle error here
                //        App.logger.LogMessage("Failed to parse index file:" + doh.ToString());
                //    }
                //}
            }

            
        }

        private List<BasicMatchListing> GetMatchListing(List<BasicMatchListing> matchList, string currentPath)
        {
           

            foreach ( ReplayIndexxmlMatches matches in replayIndexXML.Items )
            {
                if (matches.MatchRecord != null)
                {
                    foreach (ReplayIndexxmlMatchesMatchRecord matchRecord in matches.MatchRecord)
                    {
                        matchList.Add(new BasicMatchListing(matchRecord.Started, matchRecord.Finished, matchRecord.TeamHomeName, matchRecord.TeamAwayName, matchRecord.HomeScore, matchRecord.AwayScore, currentPath + matchRecord.ReplayFileName));
                    }
                }

                if (matches.RowMatchRecord != null)
                {
                    foreach (ReplayIndexxmlMatchesMatchRecord matchRecord in matches.RowMatchRecord)
                    {
                        matchList.Add(new BasicMatchListing(matchRecord.Started, matchRecord.Finished, matchRecord.TeamHomeName, matchRecord.TeamAwayName, matchRecord.HomeScore, matchRecord.AwayScore, currentPath + matchRecord.ReplayFileName));
                    }
                }

            }

            return matchList;
        }
    }
}
