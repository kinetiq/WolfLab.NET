using System;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Factories;
using EatVillagers.WolfLab.Logic.Models;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public class NightLogic
    {
        private GameOptions Options;
        private readonly VillageModel Village;

        public NightLogic(GameOptions options, VillageModel village)
        {
            Options = options;
            Village = village;
        }

        public void ExecuteNight()
        {
            foreach (var p in Village.LivingPlayers())
            {
                p.RoleLogic.ExecuteNightAction();
            }
        }
    }
}