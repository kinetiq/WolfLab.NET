using System.Collections.Generic;
using System.Linq;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;
using Z.Core.Extensions;

namespace EatVillagers.WolfLab.Logic.Extensions
{
    public static class VillageExtensions
    {

        public static int GoodPercent(this VillageModel village)
        {
            var villageSize = village.Players.Count;
            var goodSize = village.Players.Count(x => x.Team() == Teams.Good && 
                                                      x.IsAlive);

            if (villageSize == 0 || goodSize == 0)
                return 0;

            return goodSize / villageSize;
        }


        /// <summary>
        /// Statistics: What percentage of the village is currently evil?
        /// </summary>
        public static int EvilPercent(this VillageModel village)
        {
            var villageSize = village.Players.Count;
            var evilSize = village.Players.Count(x => x.Team() == Teams.Evil &&
                                                      x.IsAlive);

            if (villageSize == 0 || evilSize == 0)
                return 0;

            return evilSize/villageSize;
        }

        /// <summary>
        /// Is the total village count odd?
        /// </summary>
        public static bool IsOdd(this VillageModel village)
        {
            return village.Players
                          .Count
                          .IsOdd();

        }

        /// <summary>
        /// Is the total village count even?
        /// </summary>
        public static bool IsEven(this VillageModel village)
        {
            return village.Players
                          .Count
                          .IsEven();
        }

        /// <summary>
        /// The most skilled wolf will be calling the nightkills.
        /// </summary>
        public static PlayerModel GetLeadWerewolf(this VillageModel village)
        {
            return village.Players
                          .Where(x => x.Role == Roles.Werewolf && x.IsAlive)
                          .OrderByDescending(x => x.Skill)
                          .FirstOrDefault();
        }

        /// <summary>
        /// Get the Seer (if present and alive).
        /// </summary>
        public static PlayerModel GetSeer(this VillageModel village)
        {
            return village.Players
                          .FirstOrDefault(x => x.Role == Roles.Seer && x.IsAlive);
        }

        public static bool IsParity(this VillageModel village)
        {
            var good = village.Players.Count(x => x.Team() == Teams.Good && x.IsAlive);
            var evil = village.Players.Count(x => x.Team() == Teams.Evil && x.IsAlive);

            return (evil >= good);
        }

        public static bool HasWerewolves(this VillageModel village)
        {
            return village.LivingWolvesCount() > 0;
        }

        public static int LivingWolvesCount(this VillageModel village)
        {
            return village.Players.Count(x => x.IsAlive &&
                                              x.Role == Roles.Werewolf);
        }

        public static List<PlayerModel> LivingPlayers(this VillageModel village)
        {
            return village.Players
                          .Where(x => x.IsAlive)
                          .ToList();

        }

        public static List<PlayerModel> LivingGoodPlayers(this VillageModel village)
        {
            return village.Players
                          .Where(x => x.IsAlive && x.Team() == Teams.Good)
                          .ToList();
        }

        public static List<PlayerModel> LivingEvilPlayers(this VillageModel village)
        {
            return village.Players
                          .Where(x => x.IsAlive && x.Team() == Teams.Evil)
                          .ToList();
        }
    }
}
