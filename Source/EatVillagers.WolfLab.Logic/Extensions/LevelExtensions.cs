using System;
using EatVillagers.Village.Logic.Models.Enums;

namespace EatVillagers.Village.Logic.Extensions
{
    public static class LevelExtensions
    {
        public static string ToDegree(this Levels level)
        {
            switch (level)
            {
                case Levels.None:
                    return "not at all";
                case Levels.Low:
                    return "slightly";
                case Levels.Medium:
                    return "moderately";
                case Levels.High:
                    return "very";
                default:
                    throw new InvalidOperationException("Unexpected Level: " + level);
            }            
        }

        public static decimal ToSuspicionAmount(this Levels level)
        {
            switch (level)
            {
                case Levels.None:
                    return 0;
                case Levels.Low:
                    return .1M;
                case Levels.Medium:
                    return .25M;
                case Levels.High:
                    return .4M;
                default:
                    throw new InvalidOperationException("Unexpected Level: " + level);
            }
        }
    }
}
