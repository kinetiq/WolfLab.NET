using System.Collections.Generic;
using EatVillagers.Village.Logic.GameLogic.TrialSystems;

namespace EatVillagers.Village.Logic.Models
{
    public class VillageModel
    {
        public List<PlayerModel> Players = new List<PlayerModel>();
        public int GameRound = 0;
        public List<string> Log;
        public List<GoodOpinion> GoodOpinions = new List<GoodOpinion>();
        public List<EvilOpinion> EvilOpinions = new List<EvilOpinion>();
        public TrialBase TrialStrategy;

    }
}
