using System;
using System.Linq;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models;

namespace EatVillagers.WolfLab.Logic.GameLogic.TrialSystems
{
    public class MobTrial : TrialBase
    {
        public MobTrial(VillageModel village) : base(village)
        {
        }

        public override void ExecuteTrial()
        {
            var voters = Village.LivingPlayers().Count - 1;
            var lynchRequirement = Math.Ceiling((decimal) voters / 2) + 1;

            //not really attempting to model the particulars of the mod mentality here.
            //yet. We start with a random candidate and vote to kill.

            var candidates = Village.LivingPlayers()
                                    .OrderByDescending(x => x.AverageAggro())
                                    .ThenBy(x => new Guid()) //randomize ties
                                    .ToList();

            foreach (var candidate in candidates)
            {
                Log.Write($"{candidate.Name} draws the attention of the mob... ({candidate.AverageAggro()} aggro)");

                var defenseResult = candidate.RoleLogic.ExecuteTrialDefense();
                base.ApplyDefenseResult(defenseResult);
                
                var lynchers = Village.LivingPlayers()
                                      .Where(x => x.WouldLynch(candidate)).ToList();                

                if (lynchers.Count > 0)
                {                    
                    Log.Write($"\t{string.Join(", ", lynchers.Select(x => x.Name))} ({lynchers.Count}) vote to kill ({lynchRequirement} required)!");
                    Log.Write("");
                }

                if (lynchers.Count >= lynchRequirement)
                {
                    Lynch(candidate);
                    return;
                }
            }

            Log.Write("The mob fails to lynch anyone today.");
        }

        private void Lynch(PlayerModel target)
        {
            Log.Write($"{target.Name} is dragged off by the mob and executed!");
            target.Kill();
        }
    }
}