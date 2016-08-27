using System;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public class PlayerChecks
    {
        public static bool CheckNoticeSlip(PlayerModel player, Levels level, Random rnd)
        {
            level = InvertLevel(level); //a "high" should be "low" difficulty
            var difficulty = Difficulty(level);
            var roll = rnd.Next(1, 101);

            return roll >= difficulty;
        }

        private static Levels InvertLevel(Levels level)
        {
            switch (level)
            {
                case Levels.None:
                    return Levels.None;
                case Levels.Low:
                    return Levels.High;
                case Levels.Medium:
                    return Levels.Medium;
                case Levels.High:
                    return Levels.Low;
                default:
                    throw new InvalidOperationException("Unexpected: " + level);
            }
        }

        private static int Difficulty(Levels level)
        {
            switch (level)
            {
                case Levels.None:
                    return 0;
                case Levels.Low:
                    return 20;
                case Levels.Medium:
                    return 50;
                case Levels.High:
                    return 80;
                default:
                    throw new InvalidOperationException("Unexpected: " + level);
            }            
        }
    }
}
