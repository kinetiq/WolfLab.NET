using System;
using System.Drawing;
using System.Linq;
using EatVillagers.WolfLab.Console.ProjectLoader;
using EatVillagers.WolfLab.Logic;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Factories;
using EatVillagers.WolfLab.Logic.Models.Enums;
using Humanizer;

namespace EatVillagers.WolfLab.Console
{
    class Program
    {
        static void Main(string[] args)
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

            var project = readOutcome.Value;
            var options = project.InitializeOptions();
            var game = new Game(options);
 
            foreach (var exp in project.Experiments)
            {
                //Modify Options based on variables in experiment.
                exp.UpdateGameOptions(options);

                //TODO: refactor the hell out of all this.
                for (var villagers = project.MinVillage; villagers < project.MaxVillage; villagers++)
                {
                    var maxWolves = (villagers / 2) - 1;

                    for (var wolves = 1; wolves <= maxWolves; wolves++)
                    {
                        var experiment = new Experiment()
                        {
                            Name = $"{exp.Name}: {villagers}V versus {wolves}W",
                            Villagers = villagers,
                            Wolves = wolves
                        };

                        Stats.StartNewExperiment(experiment);

                        options.VillageSize = villagers + wolves;
                        options.WolfCount = wolves;

                        Colorful.Console.WriteLine("Experiment: " + experiment.Name);

                        for (var i = 0; i <= project.SampleSize; i++)
                        {
                            game.ExecuteGameLoop();
                        }

                        Stats.CompleteExperiment();
                    }
                }
            }


            //Statistics
            Stats.WriteExperiments("D:/wolf.csv");
            
            Colorful.Console.WriteLine($"Good wins: {Stats.GoodWins()} {Stats.GoodWinPercent() * 100:00.0}%");
            Colorful.Console.WriteLine($"Evil wins: {Stats.EvilWins()} {Stats.EvilWinPercent() * 100:00.0}%");
            Colorful.Console.Write("Duration: " + Stats.GetTimespan().Humanize());

            Colorful.Console.WriteLine();
            Colorful.Console.WriteLine("Experiments Compete.", Color.Gray);
            Colorful.Console.ReadKey();
        }
    }
}
