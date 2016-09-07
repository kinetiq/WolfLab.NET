using System.Collections.Generic;

namespace EatVillagers.WolfLab.Logic.Analytics
{
    /// <summary>
    /// Singleton used to log gameplay.
    /// </summary>
    public class LogSingleton
    {
        public static readonly LogSingleton Instance = new LogSingleton();
        
        public List<string> GameLog;
        public List<string> TurnLog;
        public bool Enabled = true;

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
        public static void Disable()
        {
            LogSingleton.Instance.Enabled = false;
        }

        public static void Write(string log)
        {
            if (!LogSingleton.Instance.Enabled)
                return;

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

        public static void ClearTurnLog()
        {
            LogSingleton.Instance.TurnLog.Clear();
        }

        public static void ClearLog()
        {
            LogSingleton.Instance.GameLog.Clear();
        }

        public static void GoodVictory()
        {
            Stats.CurrentExperiment.GoodWins++;
        }

        public static void EvilVictory()
        {
            Stats.CurrentExperiment.EvilWins++;
        }
    }
}
