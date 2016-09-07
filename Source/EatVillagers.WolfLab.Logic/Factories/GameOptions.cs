using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.Factories
{
    public class GameOptions
    {
        public int GoodSkillBonus { get; set; } = 0;
        public int EvilSkillBonus { get; set; } = 0;
        public int AverageSkill { get; set; } = 5;
        public int VillageSize { get; set; } = 8;
        public int WolfCount { get; set; } = 2;
        public int SeerCount { get; set; } = 1;
        public int HunterCount { get; set; } = 1;

        public bool CrunchMode { get; set; } = false;

        public bool UseParityHunter { get; set; } = false;
        public TrialTypes TrialType { get; set; } = TrialTypes.Mob;
        public LynchRules LynchRules { get; set; } = LynchRules.NoRules;


    }
}
