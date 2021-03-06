﻿using System;
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

            var opinion = player.GetOpinionOf(target);

            switch (player.Team())
            {
                case Teams.Good:
                    return (!opinion.IsCleared && opinion.Aggro >= .6m); //TODO: more interesting threshold
                case Teams.Evil:
                    return (!opinion.IsEvil);
                default:
                    throw new InvalidOperationException();
            }
        }

        public static decimal NetAggro(this PlayerModel player)
        {
            var village = player.Village;

            return  village.Opinions
                           .Where(x => x.Target == player && 
                                       x.Owner.IsAlive)
                           .Sum(x => x.Aggro);
        }

        public static PlayerModel ExecuteBrutalKill(this PlayerModel player)
        {
            var shadiest = player.GetLeastTrusted();
            shadiest.IsAlive = false;

            return shadiest;
        }

        public static PlayerModel GetBestSeerCandidate(this PlayerModel player)
        {
            var village = player.Village;

            var opinion = village.Opinions
                                .Where(o => o.Owner == player && 
                                                   o.Target.IsAlive && 
                                                   !o.IsCleared && 
                                                   !o.IsEvil)
                                .OrderByDescending(o => o.Aggro)
                                .ThenByDescending(o => Guid.NewGuid()) //random tiebreaker
                                .FirstOrDefault();

            return opinion?.Target;
        }

        public static PlayerModel GetLeastTrusted(this PlayerModel player)
        {
            var village = player.Village;

            var target = village.Opinions
                                .Where(opinion => opinion.Owner == player &&
                                                  opinion.Target.IsAlive &&
                                                  !opinion.IsCleared)
                                .OrderByDescending(opinion => opinion.Aggro)
                                .ThenByDescending(opinion => Guid.NewGuid()) //random tiebreaker
                                .First()
                                .Target;

            return target;
        }

        public static PlayerModel GetYummiest(this PlayerModel player)
        {
            var village = player.Village;

            var target = village.Opinions
                                .Where(opinion => opinion.Owner == player &&
                                                   opinion.IsEvil == false && 
                                                   opinion.Target.IsAlive)
                                .OrderByDescending(opinion => opinion.Aggro)
                                .ThenByDescending(opinion => Guid.NewGuid()) //random tiebreaker
                                .First()
                                .Target;

            return target;
        }

        public static Opinion GetOpinionOf(this PlayerModel player, PlayerModel target)
        {
            return player.Village
                         .Opinions
                         .Single(x => x.Owner == player && x.Target == target);
        }

        public static string Symbol(this PlayerModel player)
        {
            return player.Role
                         .ToString()
                         .Left(1)
                         .ToUpper();
        }

        public static List<Opinion> GoodOpinionsOfLiving(this PlayerModel player)
        {
            if (player.Team() == Teams.Evil)
                throw new InvalidOperationException("Evil players do not have GoodOpinions.");

            return player.Village
                         .Opinions
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

            var options = player.Village.Options;
            var village = player.Village;
            var totalAlive = village.LivingPlayers().Count;
            var wolvesAlive = village.LivingWolvesCount();
            var wolvesCaught = player.WolvesScannedCount();
            var cleared = player.VillagersClearedCount();
            var totalScanned = wolvesCaught + cleared + 1; //+1 for self-knowledge!

            var percentScanned = (decimal) totalScanned / totalAlive;
            var percentCaught = (wolvesCaught == 0) ? 0m : (decimal) wolvesAlive/ wolvesCaught;
            //var halfTheWolves = (decimal) wolvesAlive/2;

            if (percentScanned >= options.SeerPercentScannedThreshold) //default: 50%
                return true;

            if (percentCaught >= options.SeerWolfPercentThreshold) //defualt: 50%
                return true;

            if (wolvesCaught >= options.SeerWolfCountThreshold) //default: never happens.
                return true;

            return false; 
        }

        public static List<PlayerModel> OtherLivingPlayers(this PlayerModel player)
        {
            return player.Village
                         .Players
                         .Where(x => x != player && x.IsAlive)
                         .ToList();
        }

        public static List<PlayerModel> OtherGoodLivingPlayers(this PlayerModel player)
        {
            return player.Village
                         .Players
                         .Where(x => x != player && x.IsAlive && x.Team() == Teams.Good)
                         .ToList();
        }

        public static List<PlayerModel> OtherEvilLivingPlayers(this PlayerModel player)
        {
            return player.Village
                         .Players
                         .Where(x => x != player && x.IsAlive && x.Team() == Teams.Evil)
                         .ToList();
        }
    }
}
