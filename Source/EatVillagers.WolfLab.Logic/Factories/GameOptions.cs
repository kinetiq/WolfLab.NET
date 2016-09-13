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


        //The seer will reveal if this percent of the village is scanned,
        //even if there's no evil.
        public decimal SeerPercentScannedThreshold { get; set; } = .5m;

        //The seer will reveal if this percent of the living werewolves are scanned. 
        public decimal SeerWolfPercentThreshold { get; set; } = .5m;

        //The seer will reveal if this count of scanned people are alive.
        public int SeerLivingScanCountThreshold { get; set; } = 99;
        
        //The seer will reveal if this count of werewolves are scanned. Set to 99
        //by default, which inactives it.
        public int SeerWolfCountThreshold { get; set; } = 99;
    }
}
