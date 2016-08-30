﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EatVillagers.WolfLab.Logic.Analytics
{
    public static class Stats
    {
        public static Experiment CurrentExperiment => StatsSingleton.Instance.CurrentExperiment;
        public static List<Experiment> Experiments => StatsSingleton.Instance.Experiments;

        public static void StartNewExperiment(Experiment experiment)
        {
            StatsSingleton.Instance.StartNewExperiment(experiment);
        }

        public static void CompleteExperiment()
        {
            StatsSingleton.Instance.CompleteExperiment();
        }

        #region "Calculations"

        public static decimal GoodWinPercent()
        {
            var goodWins = GoodWins();
            var evilWins = EvilWins();

            return ((decimal)goodWins / (goodWins + evilWins));
        }

        public static int GoodWins()
        {
            return Experiments.Sum(x => x.GoodWins);
        }

        public static decimal EvilWinPercent()
        {
            var goodWins = GoodWins();
            var evilWins = EvilWins();

            return ((decimal)evilWins / (goodWins + evilWins));
        }

        public static int EvilWins()
        {
            return Experiments.Sum(x => x.EvilWins);
        }

        public static TimeSpan GetTimespan()
        {
            return TimeSpan.FromMilliseconds(StatsSingleton.Instance.Experiments.Sum(x => x.Duration));
        }
        #endregion

    }
}
