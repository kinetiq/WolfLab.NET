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
        private readonly Random Rnd;

        public CharacterFactory(GameOptions options, VillageModel village, Random rnd)
        {
            Options = options;
            Village = village;
            Rnd = rnd;
        }

        public PlayerModel CreateSeer(int count)
        {
            var result = new PlayerModel()
            {
                Name = $"S{count}",
                Skill = SkillBuilder.GetSkill(Teams.Good, Options, Rnd),              
                Village = Village
            };

            result.RoleLogic = new SeerRole(result);

            return result;
        }

        public PlayerModel CreateHunter(int count)
        {
            var result = new PlayerModel()
            {
                Name = $"H{count}",
                Skill = SkillBuilder.GetSkill(Teams.Good, Options, Rnd),
                Village = Village
            };

            result.RoleLogic = new HunterRole(result);

            return result;
        }

        public PlayerModel CreateVillager(int count)
        {
            var result = new PlayerModel()
            {
                Name = $"V{count}",
                Skill = SkillBuilder.GetSkill(Teams.Good, Options, Rnd),
                Village = Village
            };

            result.RoleLogic = new VillagerRole(result);

            return result;
        }

        public PlayerModel CreateWolf(int count)
        {
            var result = new PlayerModel()
            {
                Name = $"W{count}",
                Skill = SkillBuilder.GetSkill(Teams.Evil, Options, Rnd),
                Village = Village
            };

            result.RoleLogic = new WerewolfRole(result);

            return result;
        }
    }
}
