using System;
using System.Collections.Generic;
using System.Linq;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Factories;
using EatVillagers.WolfLab.Logic.GameLogic.TrialSystems;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.Factories
{
    public class VillageFactory
    {
        private readonly VillageModel Village;
        private readonly CharacterFactory CharacterFactory;
        private readonly GameOptions Options;
        private readonly NextList<PlayerModel> Population; //raw players that need roles
        private readonly Random Rnd;

        private VillageFactory(GameOptions options, List<PlayerModel> population, Random rnd)
        {
            Options = options;
            Rnd = rnd;
            Population = new NextList<PlayerModel>(population);
            Village = new VillageModel();
            CharacterFactory = new CharacterFactory(options, Village, Rnd);          
        }

        public VillageModel Create()
        {
            if (Population.Count != Options.VillageSize)
                throw new InvalidOperationException($"Wrong population size: {Population.Count} (expected: {Options.VillageSize})");

            //Setup
            ResetPopulation();

            //Deal out roles!
            PopulateEvil();
            PopulateSpecialGood();
            PopulateVillagers();
            PopulateOpinions();

            Village.Players = Village.Players.Shuffle().ToList();
            Village.TrialStrategy = GetTrialStrategy();

            return Village;
        }

        /// <summary>
        /// This helps us re-use a population across many games.
        /// </summary>
        private void ResetPopulation()
        {
            Population.ForEach(x => x.Reset());           
        }

        private TrialBase GetTrialStrategy()
        {
            switch (Options.TrialType)
            {
                case TrialTypes.Mob:
                    return new MobTrial(Village);
                default:
                    throw new NotImplementedException();
            }
        }

        private void PopulateEvil()
        {
            for (var i = 0; i < Options.WolfCount; i++)
            {
                var wolf = CharacterFactory.CreateWolf(Population.GetNext(), i);
                Village.Players.Add(wolf);
            }                
        }

        private void PopulateSpecialGood()
        {
            var seer = CharacterFactory.CreateSeer(Population.GetNext(), 0);
            Village.Players.Add(seer);

            var hunter = CharacterFactory.CreateHunter(Population.GetNext(), 0);
            Village.Players.Add(hunter);
        }

        private void PopulateVillagers()
        {
            var villagerCount = (Options.VillageSize - Village.Players.Count);

            for (var i = 0; i < villagerCount; i++)
            {
                var villager = CharacterFactory.CreateVillager(Population.GetNext(), i);
                Village.Players.Add(villager);
            }                
        }

        public void PopulateOpinions()
        {
            foreach (var owner in Village.Players)
                foreach (var target in owner.OtherLivingPlayers())
                   AddOpinion(owner, target);
        }

        private void AddOpinion(PlayerModel owner, PlayerModel target)
        {
            if (owner.Equals(target))
                throw new InvalidOperationException("This should never happen.");

            var opinion = new Opinion()
            {
                Owner = owner,
                Target = target,
            };

            //Evil players know who the other evil people are.
            if (owner.Team() == Teams.Evil)
                opinion.IsEvil = (target.Team() == Teams.Evil);            

            Village.Opinions.Add(opinion);
        }

        public static VillageModel CreateVillage(GameOptions options, Random rnd)
        {
            var playerFactory = new PlayerFactory(options, rnd);
            var population = playerFactory.CreatePopulation();

            return CreateVillage(options, population, rnd);
        }

        /// <summary>
        /// Creates a village using a pre-generated population of players. The players
        /// will be assigned roles, but their traits and skill level could be persisted
        /// across games,
        /// </summary>
        public static VillageModel CreateVillage(GameOptions options, List<PlayerModel> population, Random rnd)
        {
            population = population.Shuffle(); //Re-ordering the players is like making the players
                                               //move to different seats instead of shuffling the deck.
            var factory = new VillageFactory(options, population, rnd);

            return factory.Create();
        }
    }
}
