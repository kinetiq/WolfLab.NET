using System;
using EatVillagers.Village.Logic.Extensions;
using EatVillagers.Village.Logic.Models;
using EatVillagers.Village.Logic.Models.Enums;

namespace EatVillagers.Village.Logic.GameLogic
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
