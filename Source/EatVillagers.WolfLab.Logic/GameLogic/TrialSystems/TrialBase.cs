using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.RoleStrategies;

namespace EatVillagers.WolfLab.Logic.GameLogic.TrialSystems
{
    public abstract class TrialBase
    {
        protected VillageModel Village;

        protected TrialBase(VillageModel village)
        {
            Village = village;
        }

        public abstract void ExecuteTrial();

        protected void ApplyDefenseResult(TrialImpact impact)
        {
            var defendant = impact.Defendant;

            foreach (var p in defendant.OtherGoodLivingPlayers())
            {
                var o = p.GetGoodOpinionOf(defendant);
                o.Aggro += impact.OpinionShift;
            }

            foreach (var p in defendant.OtherEvilLivingPlayers())
            {
                var o = p.GetOpinionOf(defendant);
                o.Aggro -= impact.OpinionShift; //eat the shiny!
            }
        }
    }
}
