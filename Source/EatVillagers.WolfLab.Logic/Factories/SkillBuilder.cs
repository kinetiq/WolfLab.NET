using System;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.Factories
{
    public static class SkillBuilder
    {
        public static int GetSkill(Teams team, GameOptions options, Random rnd)
        {
            var baseSkill = GetBaseSkill(options, rnd);

            switch (team)
            {
                case Teams.Good:
                    baseSkill += options.GoodSkillBonus;
                    break;
                case Teams.Evil:
                    baseSkill += options.EvilSkillBonus;
                    break;
                default:
                    throw new InvalidOperationException("Invalid: " + team);
            }

            if (baseSkill <= 0)
                return 1;

            if (baseSkill > 10)
                return 10;

            return baseSkill;
        }

        private static int GetBaseSkill(GameOptions options, Random rnd)
        {
            var skill = options.AverageSkill;

            var roll = rnd.Next(1, 17);

            //Poor man's gaussian random!
            switch (roll)
            {
                case 1:
                    return skill;
                case 2:
                    return skill;
                case 3:
                    return skill;
                case 4:
                    return skill;
                case 5:
                    return skill;
                case 6:
                    return skill;
                case 7:
                    return skill;
                case 8:
                    return skill;
                case 9:
                    return skill - 1;
                case 10:
                    return skill - 1;
                case 11:
                    return skill - 2;
                case 12:
                    return skill - 3;
                case 13:
                    return skill + 1;
                case 14:
                    return skill + 1;
                case 15:
                    return skill + 2;
                case 16:
                    return skill + 3;
                default:
                    throw new InvalidOperationException("Unexpected: " + roll);
            }

        }

    }
}
