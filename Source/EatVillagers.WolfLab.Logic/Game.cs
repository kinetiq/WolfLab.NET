using System;
using System.Drawing;
using System.Net;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Factories;
using EatVillagers.WolfLab.Logic.GameLogic;
using EatVillagers.WolfLab.Logic.Graphics;
using EatVillagers.WolfLab.Logic.Models;
using Console = Colorful.Console;

namespace EatVillagers.WolfLab.Logic
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

                WriteRoundResult();
            } while(!IsGameOver());

            //Logging
            if (Village.HasWerewolves())
                Log.EvilVictory();
            else
                Log.GoodVictory();

            WriteGameResult();
        }

        private void WriteRoundResult()
        {
            if (Options.ComputationMode)
                return;

            Console.WriteLine();
            DrawVillage();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key...", Color.Gray);
            Console.ReadKey();
        }

        private void WriteGameResult()
        {
            if (Options.ComputationMode)
                return;

            Console.WriteLine();
            Console.WriteLine(Village.HasWerewolves()
            ? $"Werewolves win by parity, with {Village.LivingEvilPlayers().Count} wol(ves) remaining!"
            : $"Village wins, with {Village.LivingGoodPlayers().Count} alive!");

            Console.WriteLine();
            Console.WriteLine("Press any key...", Color.Gray);
            Console.ReadKey();
        }

        private bool IsGameOver()
        {
            return Village.IsParity() || !Village.HasWerewolves();
        }

        private void ExecuteTurn()
        {
            var dayLogic = new DayLogic(Options, Village, Rnd);
            var nightLogic = new NightLogic(Options, Village, Rnd);

            dayLogic.ExecuteDay();
            ShowDayLog();

            nightLogic.ExecuteNight();
            ShowNightLog();
        }

        private void ShowDayLog()
        {
            if (Options.ComputationMode)
            {
                Log.FlushTurnLog();
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"*** DAYBREAK {Day} ***", Color.BlanchedAlmond);
            Console.WriteLine();
            ShowLog();
        }

        private void ShowNightLog()
        {
            if (Options.ComputationMode)
            {
                Log.FlushTurnLog();
                return;
            } 

            Console.WriteLine();
            Console.WriteLine($"*** NIGHTFALL {Day} *** ", Color.DimGray);
            Console.WriteLine();
            ShowLog();
        }

        private void ShowLog()
        {               
            var turnLog = Log.FlushTurnLog();

            foreach (var entry in turnLog)
                Console.WriteLine(entry);
        }

        private void DrawVillage()
        {
            var visualizer = new VillageVisualizer();
            visualizer.Show(Village.Players);
        }
    }
}
