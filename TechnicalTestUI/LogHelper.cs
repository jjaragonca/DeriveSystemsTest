using System;
using System.IO;
using NUnit.Framework;

namespace TechnicalTestUI
{
    public class LogHelper
    {
        private static string LogPath;
        private static StreamWriter _streaw = null;

        public static string NowFileName()
        {
            return string.Format("{0:yyyMMddHHmmssffff}", DateTime.Now);
        }

        public static void CreateLogFile()
        {
            string dir = "../../../Logs/";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            LogPath = dir + NowFileName() + ".log";
            _streaw = File.AppendText(LogPath);
        }

        public static void Write(string logMessage)
        {
            string time = String.Format("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
            _streaw.WriteLine("{0}\t\t{1}", time, logMessage);
            _streaw.Flush();
        }

        public static void CloseLogFile()
        {
            TestContext.AddTestAttachment(LogPath, "Log file");
            _streaw.Close();
        }
    }
}
