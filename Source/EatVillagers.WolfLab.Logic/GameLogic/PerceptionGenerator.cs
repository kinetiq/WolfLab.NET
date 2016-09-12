using System;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.GameLogic.Perceptions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;
using EatVillagers.WolfLab.Logic.RandomNumbers;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public class PerceptionGenerator
    {
        public static Signal Generate(PlayerModel player)
        {
            var roll = Rng.RollD(100);
            roll = ApplyModifiers(player, roll);

            if (roll >= 90)
                return new Signal()
                {
                    Polarity = Polarities.Negative,
                    Level = Levels.High
                };

            if (roll >= 75)
                return new Signal()
                {
                    Polarity = Polarities.Negative,
                    Level = Levels.Medium
                };

            if (roll >= 70)
                return new Signal()
                {
                    Polarity = Polarities.Negative,
                    Level = Levels.Low
                };

            if (roll <= 10)
                return new Signal()
                {
                    Polarity = Polarities.Positive,
                    Level = Levels.Low
                };

            if (roll <= 5)
                return new Signal()
                {
                    Polarity = Polarities.Positive,
                    Level = Levels.Medium
                };

            return new Signal()
            {
                Polarity = Polarities.Neutral,
                Level = Levels.None
            };
        }

        private static int ApplyModifiers(PlayerModel player, int roll)
        {
            if (player.Team() == Teams.Evil)
                roll += 10; //being bad is 10% more difficult.

            roll -= player.Skill; //skill makes things easier. 
                                  //TODO: a skill of 10 gives you a 10% bonus; this is
                                  //not quite accurate. 
            return roll; 
        }
    }
}
