using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public static class Responses
    {
        public static void HandleGoodResponseToSuspiciousBehavior(PlayerModel player, Levels level)
        {
            if (level == Levels.None)
                return;

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
