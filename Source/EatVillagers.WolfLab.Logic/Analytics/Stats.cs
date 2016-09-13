using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using EatVillagers.WolfLab.Logic.Analytics.Export;
using WolfLab.Core.Configuration;

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

        public static void WriteRaw(string path)
        {
            using (var writer = File.CreateText(path))
            {
                var csv = new CsvWriter(writer);

                csv.Configuration.RegisterClassMap<ExperimentMap>();
                csv.WriteRecords(Experiments);
            }
        }


        public static void WritePivoted(string path, ProjectModel project)
        {
            using (var writer = File.CreateText(path))
            {
                writer.Write(GetCsvBody(project));
            }
        }

        private static string GetCsvBody(ProjectModel project)
        {
            var builder = new StringBuilder();

            //header
            builder.Append("Villagers, Wolves, ");
            
            Stats.Experiments
                 .GroupBy(x => x.Name)     
                 .Select(x => x.First())              //this gets distinct names
                 .OrderBy(x => x.Number)              //sorts them
                 .Aggregate(builder, (current, ex) => //concatenates them 
                            current.Append(Sanitize(ex.Name) + ", "));

            builder.AppendLine();

            //COMPLEXITY!
            for (var villagers = project.MinVillage; villagers <= project.MaxVillage; villagers++)
            {
                var maxWolves = (villagers/2) - 1;

                for (var wolves = 1; wolves <= maxWolves; wolves++)
                {
                    builder.Append($"{villagers}, {wolves}, ");

                    var experiments = Experiments.Where(x => x.Village == villagers &&
                                                             x.Wolves == wolves)
                                                 .OrderBy(x => x.Number)
                                                 .ToList();

                    experiments.Aggregate(builder, (current, ex) =>
                                                   current.Append(ex.GoodWinPercentage + ", "));

                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }

        private static string Sanitize(string source)
        {
            return source.Replace("\"", "'")
                         .Replace(",", "");
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
