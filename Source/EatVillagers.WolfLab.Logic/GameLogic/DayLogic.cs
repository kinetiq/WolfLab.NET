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
