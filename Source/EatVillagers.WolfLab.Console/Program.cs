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
            var options = new GameOptions() { ComputationMode = false };
            var game = new Game(options);

            Stats.StartNewExperiment(new Experiment());

            for (var i = 0; i < 2; i++)
            {
                game.ExecuteGameLoop();
            }


            Stats.CompleteExperiment();
            
            Colorful.Console.WriteLine($"Good wins: {Stats.GoodWins()} {Stats.GoodWinPercent() * 100:00.0}%");
            Colorful.Console.WriteLine($"Evil wins: {Stats.EvilWins()} {Stats.EvilWinPercent() * 100:00.0}%");
            Colorful.Console.Write("Duration: " + Stats.GetTimespan().Humanize());

            Colorful.Console.WriteLine();
            Colorful.Console.WriteLine("Experiments Compete.", Color.Gray);
            Colorful.Console.ReadKey();
        }
    }
}
