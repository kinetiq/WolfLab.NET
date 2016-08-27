using System;
using EatVillagers.Village.Logic.Analytics;
using EatVillagers.Village.Logic.Extensions;
using EatVillagers.Village.Logic.Factories;
using EatVillagers.Village.Logic.Models;
using EatVillagers.Village.Logic.Models.Enums;

namespace EatVillagers.Village.Logic.GameLogic
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

                var suspicionLevel = SuspicionGenerator.Generate(player, Rnd);  
                HandleGoodResponseTSuspiciousBehavior(player, suspicionLevel);             
            }

            Village.TrialStrategy.ExecuteTrial();
        }

        private void HandleGoodResponseTSuspiciousBehavior(PlayerModel player, Levels level)
        {
            if (level == Levels.None)
                return;

            Log.Write("");
            Log.Write($"{player.Name} makes a mistake and seems {level.ToDegree()} suspicious!");

            foreach (var reactor in player.OtherGoodLivingPlayers())
            {
                var slipNoticed = PlayerChecks.CheckNoticeSlip(reactor, level, Rnd);

                if (!slipNoticed)
                    continue;

                var opinion = reactor.GetGoodOpinionOf(player);

                if (opinion.IsCleared)
                {
                    Log.Write($"\t{reactor.Name} noticed, but thinks {player.Name} is clear.");
                    continue;
                }

                opinion.Suspicion += level.ToSuspicionAmount();
                Log.Write($"\t{reactor.Name} noticed, and is more suspicious of {player.Name} ({opinion.Suspicion * 100:0}%). ");                
            }            
        }
    }
}
