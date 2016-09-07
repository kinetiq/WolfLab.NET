using System;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;
using EatVillagers.WolfLab.Logic.RoleStrategies;

namespace EatVillagers.WolfLab.Logic.Factories
{
    public class CharacterFactory
    {
        private readonly GameOptions Options;
        private readonly VillageModel Village;

        public CharacterFactory(GameOptions options, VillageModel village)
        {
            Options = options;
            Village = village;
        }

        public PlayerModel CreateSeer(PlayerModel player, int count)
        {
            player.Name = $"S{count}";
            player.Village = Village;
            player.RoleLogic = new SeerRole(player);

            return player;
        }

        public PlayerModel CreateHunter(PlayerModel player, int count)
        {
            player.Name = $"H{count}";
            player.Village = Village;
            player.RoleLogic = new HunterRole(player);

            return player;
        }

        public PlayerModel CreateVillager(PlayerModel player, int count)
        {
            player.Name = $"V{count}";
            player.Village = Village;
            player.RoleLogic = new VillagerRole(player);

            return player;
        }

        public PlayerModel CreateWolf(PlayerModel player, int count)
        {
            player.Name = $"W{count}";
            player.Village = Village;
            player.RoleLogic = new WerewolfRole(player);

            return player;
        }
    }
}
