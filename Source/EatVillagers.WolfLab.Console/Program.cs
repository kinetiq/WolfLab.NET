using System;
using System.Drawing;
using EatVillagers.WolfLab.Console.ProjectLoader;
using EatVillagers.WolfLab.Console.UI;
using EatVillagers.WolfLab.Logic;
using EatVillagers.WolfLab.Logic.Analytics;
using Humanizer;
using WolfLab.Core.Configuration;
using Z.Core.Extensions;

namespace EatVillagers.WolfLab.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            var project = LoadProjectFile(); //contains experiment details
            var options = project.InitializeGameOptions();
            var game = new Game(options);

            var totalExperiments = project.Experiments.Count;
            var currentExperiment = 0;

            //In our settings, we specify a lot of permutations: a range of villagers, 
            //a set of experiments, and a count of samples to run per permutation. If
            //minVillagers is 5, maxVillagers is 10, samples is 1000, and we specify 
            //2 experiments... 
            //Permutations is (6 villager sets * 1000 samples * 2 experiments) games. 
            foreach (var exp in project.Experiments)
            {
                currentExperiment++;

                //Modify Options based on variables in experiment.
                exp.UpdateGameOptions(options);

                var experimentIndex = 0;

                //TODO: refactor this. 
                for (var villagers = project.MinVillage; villagers <= project.MaxVillage; villagers++)
                {
                    var maxWolves = (villagers / 2) - 1; 

                    for (var wolves = 1; wolves <= maxWolves; wolves++)
                    {                        
                        var experiment = new Experiment()
                        {
                            Name = $"{exp.Name}",  //: {villagers}V with {wolves}W"
                            Number = experimentIndex,
                            Village = villagers,
                            Wolves = wolves
                        };

                        experimentIndex++;

                        Stats.StartNewExperiment(experiment);

                        if (options.CrunchMode)
                            ProgressUpdater.DrawStatusArea(experiment, currentExperiment, totalExperiments);

                        options.VillageSize = villagers;
                        options.WolfCount = wolves;

                        for (var i = 0; i <= project.SampleSize + 1; i++)
                        {
                            game.ExecuteGameLoop();

                            if (options.CrunchMode)
                                ProgressUpdater.UpdateProgress(experiment, i, project.SampleSize);
                        }

                        Stats.CompleteExperiment();
                    }
                }
            }

            ProcessEnding(project);        
        }

        private static ProjectModel LoadProjectFile()
        {
            var loader = new FileLoader();
            var readOutcome = loader.Read(@"d:\experiment.txt");

            if (readOutcome.Failure)
            {
                Colorful.Console.Write(readOutcome.ToMultiLine(Environment.NewLine));
                System.Console.WriteLine();
                System.Console.WriteLine(@"Press any key...");
                System.Console.ReadKey();
            }

            return readOutcome.Value;
        }

        /// <summary>
        /// Write the end of game text and save data to a spreadsheet.
        /// </summary>
        private static void ProcessEnding(ProjectModel project)
        {
            //Statistics
            Colorful.Console.Clear();
            Colorful.Console.WriteLine("Experiments Complete!", Color.LightGray);

            Colorful.Console.WriteLine($"Good wins: {Stats.GoodWins()} {Stats.GoodWinPercent() * 100:00.0}%", Color.LightGray);
            Colorful.Console.WriteLine($"Evil wins: {Stats.EvilWins()} {Stats.EvilWinPercent() * 100:00.0}%", Color.LightGray);
            Colorful.Console.WriteLine("Duration: " + Stats.GetTimespan().Humanize(), Color.LightGray);
            Colorful.Console.WriteLine("Saving...", Color.LightGray);

            Stats.WriteRaw("D:/wolf_raw.csv");
            Stats.WritePivoted("D:/wolf.csv", project);

            Colorful.Console.WriteLine();
            Colorful.Console.WriteLine();
            Colorful.Console.WriteLine("Done! Exiting...", Color.Gray);
            Colorful.Console.ReadKey();
        }
    }
}
