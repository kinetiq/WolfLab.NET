using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;
using EatVillagers.WolfLab.Logic.RoleStrategies;

namespace EatVillagers.WolfLab.Logic.Factories
{
    public class PlayerFactory
    {
        private readonly GameOptions Options;

        public PlayerFactory(GameOptions options)
        {
            Options = options;
        }

        public List<PlayerModel> CreatePopulation()
        {
            var players = new List<PlayerModel>();

            for (var i = 0; i < Options.VillageSize; i++)
                players.Add(InitializePlayer(i));

            return players;
        }

        private PlayerModel InitializePlayer(int count)
        {
            var result = new PlayerModel()
            {
                ID = $"P{count}",
                Name = $"P{count}",
                Skill = SkillBuilder.GetSkill(Teams.Good, Options),
                Traits = GenerateTraits()
            };

            result.RoleLogic = new SpectatorRole(result);

            return result;
        }

        private List<Traits> GenerateTraits()
        {
            return new List<Traits>();
        }

    }
}
