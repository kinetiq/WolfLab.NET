using System;
using System.Drawing;
using EatVillagers.WolfLab.Console.ProjectLoader;
using EatVillagers.WolfLab.Logic;
using EatVillagers.WolfLab.Logic.Analytics;
using Humanizer;
using Z.Core.Extensions;

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

            var totalExperiments = project.Experiments.Count;
            var currentExperiment = 0; 
 
            foreach (var exp in project.Experiments)
            {
                currentExperiment++;

                //Modify Options based on variables in experiment.
                exp.UpdateGameOptions(options);

                //TODO: refactor the hell out of all this.
                for (var villagers = project.MinVillage; villagers <= project.MaxVillage; villagers++)
                {
                    var maxWolves = (villagers / 2) - 1; 

                    for (var wolves = 1; wolves <= maxWolves; wolves++)
                    {
                        var experiment = new Experiment()
                        {
                            Name = $"{exp.Name}: {villagers}V with {wolves}W",
                            Village = villagers,
                            Wolves = wolves
                        };

                        Stats.StartNewExperiment(experiment);

                        if (options.CrunchMode)
                            DrawStatusTick(experiment, currentExperiment, totalExperiments);

                        options.VillageSize = villagers;
                        options.WolfCount = wolves;

                        for (var i = 0; i <= project.SampleSize + 1; i++)
                        {
                            game.ExecuteGameLoop();

                            if (options.CrunchMode)
                            {    
                                UpdateProgress(experiment, i, project.SampleSize);
                            }                               
                        }

                        Stats.CompleteExperiment();
                    }
                }
            }

            ProcessEnding();        
        }

        private static void ProcessEnding()
        {
            //Statistics
            Colorful.Console.Clear();
            Colorful.Console.WriteLine("Experiments Complete!", Color.LightGray);
            Colorful.Console.WriteLine("Saving...", Color.LightGray);

            Stats.WriteExperiments("D:/wolf.csv");

            Colorful.Console.WriteLine($"Good wins: {Stats.GoodWins()} {Stats.GoodWinPercent() * 100:00.0}%", Color.LightGray);
            Colorful.Console.WriteLine($"Evil wins: {Stats.EvilWins()} {Stats.EvilWinPercent() * 100:00.0}%", Color.LightGray);
            Colorful.Console.Write("Duration: " + Stats.GetTimespan().Humanize(), Color.LightGray);

            Colorful.Console.WriteLine();
            Colorful.Console.WriteLine();
            Colorful.Console.WriteLine("Done! Exiting...", Color.Gray);
            Colorful.Console.ReadKey();
        }

        private static int LastTicks = 0;

        static void UpdateProgress(Experiment experiment, int current, int total)
        {
            var percent = (decimal) current/total;
            var ticks = Math.Floor(percent*10).ToInt32();

            if (ticks <= LastTicks)
                return;
           
            Colorful.Console.SetCursorPosition(1, 1);
            Colorful.Console.Write("*".Repeat(ticks), Color.SlateGray);

            Colorful.Console.SetCursorPosition(13, 1);

            Colorful.Console.Write(current + " / " + total + " samples", Color.LightGray);
            Colorful.Console.SetCursorPosition(0, 4);

            Colorful.Console.Write("Good Win Rate: ", Color.LightGray);

            var goodWinRate = experiment.GoodWinPercentage;

            Colorful.Console.WriteLine(goodWinRate.ToString("0%"), 
                                   GetColor(goodWinRate));

            LastTicks = ticks;
        }

        private static Color GetColor(decimal percent)
        {
            var ticks = Math.Floor((percent * 100) / 5) * 5;
            var nearestFivePercent = (decimal) ticks/100;

            int col = (245 * nearestFivePercent).ToInt32();

            //There's a weird problem with the graphics library. It
            //gets stuck if we push it past 110.
            if (col > 98)
                col = 110;

            return Color.FromArgb(col, col, 255);
        }

        private static void DrawStatusTick(Experiment experiment, int current, int total)
        {
            Colorful.Console.Clear();

            Colorful.Console.Write(experiment.Name, Color.LightGray);
            Colorful.Console.Write(" (", Color.SlateBlue);

            Colorful.Console.Write($"experiment {current} of {total}", Color.DarkGray);

            Colorful.Console.Write(")", Color.SlateBlue);
            Colorful.Console.WriteLine();
            
            Colorful.Console.Write("[", Color.SlateBlue);
            Colorful.Console.Write(" ".Repeat(10));
            Colorful.Console.Write("]", Color.SlateBlue);
           
            LastTicks = 0;
        }
    }
}
