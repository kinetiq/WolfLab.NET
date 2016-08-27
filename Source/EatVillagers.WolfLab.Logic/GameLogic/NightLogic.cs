using System;
using EatVillagers.Village.Logic.Extensions;
using EatVillagers.Village.Logic.Factories;
using EatVillagers.Village.Logic.Models;

namespace EatVillagers.Village.Logic.GameLogic
{
    public class NightLogic
    {
        private GameOptions Options;
        private readonly VillageModel Village;
        private readonly Random Rnd;

        public NightLogic(GameOptions options, VillageModel village, Random rnd)
        {
            Options = options;
            Village = village;
            Rnd = rnd;
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