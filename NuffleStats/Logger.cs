using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NuffleStats
{
    
    public class Logger
    {
        public bool logging = true;

        public string logPath = "";

        public Logger()
        {
            logPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\NuffleStats.log";
            StreamWriter sw = new StreamWriter(logPath, false, Encoding.Default);
            sw.WriteLine("NuffleStats: " + DateTime.Now.ToShortDateString());
            sw.Close();
            LogMessage("NuffleStats Logger started");

        }

        public void LogMessage(string m)
        {
            if (!logging) return;
            StreamWriter sw = new StreamWriter(logPath, true, Encoding.Default);
            sw.WriteLine(m);
            sw.Close();
        }
    }
}
