using System;
using System.Drawing;
using EatVillagers.Village.Logic.Analytics;
using EatVillagers.Village.Logic.Extensions;
using EatVillagers.Village.Logic.Factories;
using EatVillagers.Village.Logic.GameLogic;
using EatVillagers.Village.Logic.Graphics;
using EatVillagers.Village.Logic.Models;
using Console = Colorful.Console;

namespace EatVillagers.Village.Logic
{
    public class Game
    {
        public VillageModel Village;
        public int Day;
        private readonly GameOptions Options;
        private readonly Random Rnd;

        public Game(GameOptions options)
        {
            Options = options;
            Rnd = new Random();           
            Day = 0;
            
        }

        public void ExecuteGameLoop()
        {
            Day = 0;
            Village = VillageFactory.CreateVillage(Options, Rnd);

            do
            {
                Day++;
                ExecuteTurn();

                Console.WriteLine();
                DrawVillage();

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Press any key...", Color.Gray);
                Console.ReadKey();  
            } while(!GameOver());

            Console.WriteLine();

            Console.WriteLine(Village.HasWerewolves()
                ? $"Werewolves win by parity, with {Village.LivingEvilPlayers().Count} wol(ves) remaining!"
                : $"Village wins, with {Village.LivingGoodPlayers().Count} alive!");

            Console.WriteLine();
            Console.WriteLine("Press any key...", Color.Gray);
            Console.ReadKey();
        }

        private void DrawVillage()
        {
            var visualizer = new VillageVisualizer();
            visualizer.Show(Village.Players);
        }

        private bool GameOver()
        {
            return Village.IsParity() || !Village.HasWerewolves();
        }

        public void ExecuteTurn()
        {
            var dayLogic = new DayLogic(Options, Village, Rnd);
            var nightLogic = new NightLogic(Options, Village, Rnd);

            dayLogic.ExecuteDay();

            Console.WriteLine();
            Console.WriteLine($"*** DAYBREAK {Day} ***", Color.BlanchedAlmond);  
            ShowLog();

            nightLogic.ExecuteNight();

            Console.WriteLine();
            Console.WriteLine($"*** NIGHTFALL {Day} *** ", Color.DimGray);
            Console.WriteLine();
            ShowLog();
        }

        public void ShowLog()
        {
            var turnLog = Log.FlushTurnLog();

            foreach (var entry in turnLog)
            {
                Console.WriteLine(entry);
            }
        }
    }
}
