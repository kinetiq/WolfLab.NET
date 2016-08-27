using System;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public class SuspicionGenerator
    {
        public static Levels Generate(PlayerModel player, Random rnd)
        {
            var topRange = GetTopRange(player);
            var roll = rnd.Next(1, topRange);

            if (roll >= 95)
                return Levels.High;

            if (roll >= 90)
                return Levels.Medium;

            if (roll >= 85)
                return Levels.Low;

            return Levels.None;
        }

        private static int GetTopRange(PlayerModel player)
        {
            //higher rolls are bad, skill reduces it.
            if (player.Team() == Teams.Evil)
                return 110 - player.Skill;

            return 101 - player.Skill;
        }
    }
}
