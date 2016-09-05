using System.Collections.Generic;
using EatVillagers.WolfLab.Logic.Factories;
using EatVillagers.WolfLab.Logic.GameLogic.TrialSystems;

namespace EatVillagers.WolfLab.Logic.Models
{
    public class VillageModel
    {
        public List<PlayerModel> Players = new List<PlayerModel>();
        public int Day = 0;
        public List<string> Log;
        public List<Opinion> Opinions = new List<Opinion>();
        public TrialBase TrialStrategy;
        public GameOptions Options;

    }
}
