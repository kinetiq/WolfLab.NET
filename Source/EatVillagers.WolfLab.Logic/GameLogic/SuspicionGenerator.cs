using System;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;
using NDiceBag;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public class SuspicionGenerator
    {
        public static Levels Generate(PlayerModel player)
        {
            var roll = 1.d(100)
                        .Plus(player.Team() == Teams.Evil ? 10 : 0) //10% penalty for evil.
                        .Minus(player.Skill) //0%-10% bonus for skill.
                        .Roll();

            if (roll >= 95)
                return Levels.High;

            if (roll >= 90)
                return Levels.Medium;

            if (roll >= 85)
                return Levels.Low;

            return Levels.None;
        }
    }
}
