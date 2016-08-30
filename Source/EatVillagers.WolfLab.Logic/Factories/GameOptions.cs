using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.Factories
{
    public class GameOptions
    {
        public int GoodSkillBonus { get; set; } = 0;
        public int EvilSkillBonus { get; set; } = 0;
        public int AverageSkill { get; set; } = 5;
        public int VillageSize { get; set; } = 10;
        public int WolfCount { get; set; } = 2;
        public bool ComputationMode { get; set; } = false;

        public TrialTypes TrialType { get; set; } = TrialTypes.Mob;
    }
}
