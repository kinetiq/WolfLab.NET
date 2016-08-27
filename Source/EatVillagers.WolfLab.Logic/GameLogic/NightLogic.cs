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