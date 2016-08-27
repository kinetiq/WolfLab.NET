using System;
using System.Linq;
using EatVillagers.WolfLab.Logic.Extensions;
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
        private readonly Random Rnd;

        private VillageFactory(GameOptions options, Random rnd)
        {
            Options = options;
            Rnd = rnd;
            Village = new VillageModel();
            CharacterFactory = new CharacterFactory(options, Village, Rnd);          
        }

        private VillageModel Create()
        {
            PopulateEvil();
            PopulateSpecialGood();
            PopulateVillagers();
            PopulateOpinions();


            Village.Players = Village.Players.Shuffle().ToList();
            Village.TrialStrategy = GetTrialStrategy();

            return Village;
        }

        public TrialBase GetTrialStrategy()
        {
            switch (Options.TrialType)
            {
                case TrialTypes.Mob:
                    return new MobTrial(Village);
                default:
                    throw new NotImplementedException();
            }
        }

        public void PopulateEvil()
        {
            for (var i = 0; i < Options.WolfCount; i++)
                Village.Players.Add(CharacterFactory.CreateWolf(i));
        }

        public void PopulateSpecialGood()
        {
            Village.Players.Add(CharacterFactory.CreateSeer(0));
            Village.Players.Add(CharacterFactory.CreateHunter(0));
        }

        public void PopulateVillagers()
        {
            var villagerCount = (Options.VillageSize - Village.Players.Count);

            for (var i = 0; i < villagerCount; i++)
                Village.Players.Add(CharacterFactory.CreateVillager(i));
        }

        public void PopulateOpinions()
        {
            foreach (var source in Village.Players)
                foreach (var target in Village.Players.Where(x => !x.Equals(source)))
                   AddOpinion(source, target);
        }

        private void AddOpinion(PlayerModel owner, PlayerModel target)
        {
            if (owner.Equals(target))
                throw new InvalidOperationException("This should never happen.");

            switch (owner.Team())
            {
                case Teams.Good:
                    Village.GoodOpinions.Add(new GoodOpinion()
                    {
                        Owner = owner,
                        Target = target
                    });

                    return;
                case Teams.Evil:
                    Village.EvilOpinions.Add(new EvilOpinion()
                    {
                        Owner = owner,
                        Target = target,
                        IsEvil = (target.Role == Roles.Werewolf)
                    });

                    return;
                default:
                    throw new InvalidOperationException("Unexpected Team: " + owner.Team());
            }
        }

        public static VillageModel CreateVillage(GameOptions options, Random rnd)
        {
            var factory = new VillageFactory(options, rnd);

            return factory.Create();
        }
    }
}
