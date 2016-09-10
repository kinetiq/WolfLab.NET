using System;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;
using EatVillagers.WolfLab.Logic.RandomNumbers;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public class SuspicionGenerator
    {
        public static Levels Generate(PlayerModel player)
        {
            var ceiling = 100;

            if (player.Team() == Teams.Evil)
                ceiling = ceiling + 10;

            ceiling = ceiling - player.Skill;

            var roll = Rng.RollD(ceiling);

            if (roll >= 90)
                return Levels.High;

            if (roll >= 75)
                return Levels.Medium;

            if (roll >= 70)
                return Levels.Low;

            return Levels.None;
        }
    }
}
