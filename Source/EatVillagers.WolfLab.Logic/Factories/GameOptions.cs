using EatVillagers.Village.Logic.Models.Enums;

namespace EatVillagers.Village.Logic.Factories
{
    public class GameOptions
    {
        public int GoodSkillBonus { get; set; } = 0;
        public int EvilSkillBonus { get; set; } = 0;
        public int AverageSkill { get; set; } = 5;
        public int VillageSize { get; set; } = 10;
        public int WolfCount { get; set; } = 2;

        public TrialTypes TrialType { get; set; } = TrialTypes.Mob;
    }
}
