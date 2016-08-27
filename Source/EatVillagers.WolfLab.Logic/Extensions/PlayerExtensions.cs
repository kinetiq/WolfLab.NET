using System;
using System.Collections.Generic;
using System.Linq;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Attributes;
using EatVillagers.WolfLab.Logic.Models.Enums;
using Z.Core.Extensions;
using Z.Reflection.Extensions;

namespace EatVillagers.WolfLab.Logic.Extensions
{
    public static class PlayerExtensions
    {
        /// <summary>
        /// Team is determined by role. 
        /// </summary>
        public static Teams Team(this PlayerModel player)
        {
            return player.Role
                         .GetCustomAttribute<TeamAttribute>()
                         .Team;
        }

        public static bool WouldLynch(this PlayerModel player, PlayerModel target)
        {
            if (player == target)
                return false;

            switch (player.Team())
            {
                case Teams.Good:
                {
                    var o = player.GetGoodOpinionOf(target);
                    return (!o.IsCleared && o.Suspicion > .6m); //TODO: more interesting threshold
                }
                case Teams.Evil:
                {
                    var o = player.GetEvilOpinionOf(target);
                
                    return (!o.IsEvil);
                }
                default:
                    throw new InvalidOperationException();
            }
        }

        public static decimal AverageSuspicion(this PlayerModel player)
        {
            var village = player.Village;
            var good = village.GoodOpinions
                           .Where(x => x.Target == player && 
                                       x.Owner.IsAlive)
                           .Sum(x => x.Suspicion);

            var evil = village.EvilOpinions.Where(x => x.Target == player &&
                                                       x.Owner.IsAlive)
                                           .Sum(x => x.EatPriority);

            return good + evil;
        }

        public static PlayerModel ExecuteBrutalKill(this PlayerModel player)
        {
            var shadiest = player.GetLeastTrusted();
            shadiest.IsAlive = false;

            return shadiest;
        }

        public static GoodOpinion GetGoodOpinionOf(this PlayerModel player, PlayerModel target)
        {
            return player.Village
                         .GoodOpinions
                         .Single(x => x.Owner == player && x.Target == target);
        }

        public static PlayerModel GetBestSeerCandidate(this PlayerModel player)
        {
            var village = player.Village;

            var opinion = village.GoodOpinions
                                .Where(o => o.Owner == player && 
                                                   o.Target.IsAlive && 
                                                   !o.IsCleared && 
                                                   !o.IsEvil)
                                .OrderByDescending(o => o.Suspicion)
                                .ThenByDescending(o => Guid.NewGuid()) //random tiebreaker
                                .FirstOrDefault();

            return opinion?.Target;
        }

        public static PlayerModel GetLeastTrusted(this PlayerModel player)
        {
            var village = player.Village;

            var target = village.GoodOpinions
                                .Where(opinion => opinion.Owner == player &&
                                                  opinion.Target.IsAlive &&
                                                  !opinion.IsCleared)
                                .OrderByDescending(opinion => opinion.Suspicion)
                                .ThenByDescending(opinion => Guid.NewGuid()) //random tiebreaker
                                .First()
                                .Target;

            return target;
        }

        public static PlayerModel GetYummiest(this PlayerModel player)
        {
            var village = player.Village;

            var target = village.EvilOpinions
                                .Where(opinion => opinion.Owner == player &&
                                                   opinion.IsEvil == false && 
                                                   opinion.Target.IsAlive)
                                .OrderByDescending(opinion => opinion.EatPriority)
                                .ThenByDescending(opinion => Guid.NewGuid()) //random tiebreaker
                                .First()
                                .Target;

            return target;
        }

        public static EvilOpinion GetEvilOpinionOf(this PlayerModel player, PlayerModel target)
        {
            return player.Village
                         .EvilOpinions
                         .Single(x => x.Owner == player && x.Target == target);
        }

        public static string Symbol(this PlayerModel player)
        {
            return player.Role
                         .ToString()
                         .Left(1)
                         .ToUpper();
        }

        public static List<GoodOpinion> GoodOpinionsOfLiving(this PlayerModel player)
        {
            if (player.Team() == Teams.Evil)
                throw new InvalidOperationException("Evil players do not have GoodOpinions.");

            return player.Village
                         .GoodOpinions
                         .Where(x => x.Owner == player && x.Target.IsAlive)
                         .ToList();
        }

        public static List<EvilOpinion> EvilOpinionsOfLiving(this PlayerModel player)
        {
            if (player.Team() == Teams.Good)
                throw new InvalidOperationException("Good players do not have EvilOpinions.");

            return player.Village
                         .EvilOpinions
                         .Where(x => x.Owner == player && x.Target.IsAlive)
                         .ToList();
        }

        public static int WolvesScannedCount(this PlayerModel player)
        {
            if (player.Role != Roles.Seer)
                throw new InvalidOperationException();

            return player.GoodOpinionsOfLiving()
                         .Count(x => x.IsEvil);
        }

        public static List<PlayerModel> ScannedWolves(this PlayerModel player)
        {
            return player.GoodOpinionsOfLiving()
                         .Where(x => x.IsEvil)
                         .Select(x => x.Target)
                         .ToList();
        }

        public static List<PlayerModel> ClearedVillagers(this PlayerModel player)
        {
            return player.GoodOpinionsOfLiving()
                         .Where(x => x.IsCleared)
                         .Select(x => x.Target)
                         .ToList();
        }


        public static int VillagersClearedCount(this PlayerModel player)
        {
            return player.GoodOpinionsOfLiving()
                         .Count(x => x.IsCleared);
        }

        public static bool CheckSeerReveal(this PlayerModel player)
        {
            if (player.Role != Roles.Seer)
                throw new InvalidOperationException();

            if (player.IsClaimed) //reveal every turn after claiming!
                return true; 

            var village = player.Village;
            var totalAlive = village.LivingPlayers().Count;
            var wolvesAlive = village.LivingWolvesCount();
            var wolvesCaught = player.WolvesScannedCount();
            var cleared = player.VillagersClearedCount();
            var totalScanned = wolvesCaught + cleared + 1; //+1 for self-knowledge!

            var percentScanned = (decimal) totalScanned / (totalAlive);
            var halfTheWolves = (decimal) wolvesAlive/2;

            if (percentScanned >= .5m) //at >= .5, that's a lot of valuable data.
                return true;

            if (wolvesCaught >= halfTheWolves) //half the wolves caught - time to reveal.
                return true;

            return false; 
        }
    }
}
