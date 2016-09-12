using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.GameLogic.Perceptions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public static class Responses
    {
        public static void HandleGoodResponseToSignal(PlayerModel player, Signal signal)
        {
            if (signal.Level == Levels.None)
                return;

            switch (signal.Polarity)
            {
                case Polarities.Neutral:
                    return; //nothing to do.
                case Polarities.Positive:
                    ProcessPositiveSignal(player, signal);
                    return;
                case Polarities.Negative:
                    ProcessNegativeSignal(player, signal);
                    return;
            }
        }

        private static void ProcessPositiveSignal(PlayerModel player, Signal signal)
        {
            var level = signal.Level;

            Log.Write("");
            Log.Write($"{player.Name} makes a {level.ToDegree()} persuasive play!");

            foreach (var reactor in player.OtherGoodLivingPlayers())
            {
                var opinion = reactor.GetOpinionOf(player);
                opinion.Aggro -= level.ToSuspicionAmount(); 

                Log.Write($"\t{reactor.Name} is less suspicious of {player.Name} ({opinion.Aggro * 100:0}%). ");
            }
        }

        private static void ProcessNegativeSignal(PlayerModel player, Signal signal)
        {
            var level = signal.Level;

            Log.Write("");
            Log.Write($"{player.Name} makes a mistake and seems {level.ToDegree()} suspicious!");

            foreach (var reactor in player.OtherGoodLivingPlayers())
            {
                var slipNoticed = PlayerChecks.CheckNoticeSlip(reactor, level);

                if (!slipNoticed)
                    continue;

                var opinion = reactor.GetOpinionOf(player);

                if (opinion.IsCleared)
                {
                    Log.Write($"\t{reactor.Name} noticed, but thinks {player.Name} is clear.");
                    continue;
                }

                opinion.Aggro += level.ToSuspicionAmount();
                           

                Log.Write($"\t{reactor.Name} noticed, and is more suspicious of {player.Name} ({opinion.Aggro * 100:0}%). ");
            }
        }
    }
}
