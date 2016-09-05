using System;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Factories;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public class DayLogic
    {
        private GameOptions Options;
        private readonly VillageModel Village;
        private readonly Random Rnd;

        public DayLogic(GameOptions options, VillageModel village, Random rnd)
        {
            Options = options;
            Village = village;
            Rnd = rnd;
        }

        public void ExecuteDay()
        {         
            foreach (var player in Village.LivingPlayers().Shuffle())
            {
                player.RoleLogic.ExecuteDayAction();
            }

            Village.TrialStrategy.ExecuteTrial();
        }
    }
}
