using System.Collections.Generic;

namespace EatVillagers.Village.Logic.Analytics
{
    /// <summary>
    /// Singleton used to log gameplay.
    /// </summary>
    public class LogSingleton
    {
        public static readonly LogSingleton Instance = new LogSingleton();

        public List<string> GameLog;
        public List<string> TurnLog;

        private LogSingleton()
        {
            GameLog = new List<string>();
            TurnLog = new List<string>();
        }
    }

    /// <summary>
    /// Interface to the log singleton.
    /// </summary>
    public static class Log
    {
        public static void Write(string log)
        {
            LogSingleton.Instance.GameLog.Add(log);
            LogSingleton.Instance.TurnLog.Add(log);
        }

        /// <summary>
        /// Reads the turn log, and then clears it.
        /// </summary>
        public static List<string> FlushTurnLog()
        {
            var result = new List<string>(LogSingleton.Instance.TurnLog);
            LogSingleton.Instance.TurnLog.Clear();

            return result;
        }
    }
}
