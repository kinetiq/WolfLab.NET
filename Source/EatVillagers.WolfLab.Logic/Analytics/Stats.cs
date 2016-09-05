using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using EatVillagers.WolfLab.Logic.Analytics.Export;

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
        
        public static void WriteExperiments(string path)
        {
            using (var writer = File.CreateText(path))
            {
                var csv = new CsvWriter(writer);

                csv.Configuration.RegisterClassMap<ExperimentMap>();
                csv.WriteRecords(Experiments);
            }
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
