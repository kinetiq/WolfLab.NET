using System;
using System.Drawing;
using System.Net;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Factories;
using EatVillagers.WolfLab.Logic.GameLogic;
using EatVillagers.WolfLab.Logic.Graphics;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;
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
                Village.Day = Day; //refactor

                ExecuteTurn();

                WriteRoundResult();
            } while(!IsGameOver());

            //Logging
            if (IsEvilVictory())
                Log.EvilVictory();
            else
                Log.GoodVictory();

            WriteGameResult();
        }

        #region "Turn Logic"
        private void ExecuteTurn()
        {
            var dayLogic = new DayLogic(Options, Village, Rnd);
            var nightLogic = new NightLogic(Options, Village, Rnd);

            dayLogic.ExecuteDay();
            ShowDayLog();

            nightLogic.ExecuteNight();
            ShowNightLog();
        }
        #endregion

        private bool IsEvilVictory()
        {
            //If evil is destroyed, the village wins.
            if (Village.LivingPlayers().Count == 0)
                return false;

            if (Options.UseParityHunter && Village.IsAnyAlive(Roles.Hunter))
            {
                var evil = Village.LivingEvilPlayers().Count;
                var hunters = Village.LivingCount(Roles.Hunter);

                return (evil > hunters);
            }

            return Village.HasWerewolves();
        }

        private bool IsGameOver()
        {
            return Village.IsParity() || !Village.HasWerewolves();
        }

        #region "UI"
        private void WriteRoundResult()
        {
            if (Options.CrunchMode)
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
            if (Options.CrunchMode)
                return;

            Console.WriteLine();
            Console.WriteLine(Village.HasWerewolves()
            ? $"Werewolves win by parity, with {Village.LivingEvilPlayers().Count} wol(ves) remaining!"
            : $"Village wins, with {Village.LivingGoodPlayers().Count} alive!");

            Console.WriteLine();
            Console.WriteLine("Press any key...", Color.Gray);
            Console.ReadKey();
        }

        private void ShowDayLog()
        {
            if (Options.CrunchMode)
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
            if (Options.CrunchMode)
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
        #endregion
    }
}
