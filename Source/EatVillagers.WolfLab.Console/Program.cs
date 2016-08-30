using System;
using System.Drawing;
using System.Linq;
using EatVillagers.WolfLab.Logic;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Factories;
 using Humanizer;

namespace EatVillagers.WolfLab.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new GameOptions() { ComputationMode = true };
            var game = new Game(options);

            

            //options.VillageSize = 10;

            for (var villagers = 4; villagers < 20; villagers++)
            {
                var maxWolves = (villagers/2) - 1;

                for (var wolves = 1; wolves <= maxWolves; wolves++)
                {
                    var experiment = new Experiment()
                    {
                        Name = $"{villagers}v versus {wolves}w",
                        Villagers = villagers,
                        Wolves = wolves
                    };

                    Stats.StartNewExperiment(experiment);

                    options.VillageSize = villagers + wolves;
                    options.WolfCount = wolves;

                    Colorful.Console.WriteLine("Running: " + experiment.Name);

                    for (var i = 0; i < 1000; i++)
                    {                        
                        game.ExecuteGameLoop();
                    }

                    Stats.CompleteExperiment();
                }
            }

      
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
