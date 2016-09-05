using System;
using System.Linq;
using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

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

            //not really attempting to model the particulars of the mob mentality here.
            //yet. We start with the most aggro candidates and vote until we kill,
            //or we run out of candidates.

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



            switch (Village.Options.LynchRules)
            {
                case LynchRules.NoRules:
                    Log.Write("The mob fails to lynch anyone today.");
                    break;
                case LynchRules.MustLynchAlways:
                    var topCandidate = candidates.First();

                    Log.Write("The mob must lynch every day, so it turned on the most suspicious person...");
                    Lynch(topCandidate);

                    break;
                case LynchRules.MustLynchFirstDay:
                    if (Village.Day == 1)
                    {
                        Log.Write("The mob must lynch on Day 1, so it turned on the most suspicious person...");
                        Lynch(candidates.First());
                        break;
                    }

                    Log.Write("The mob fails to lynch anyone today.");

                    break;
                default:
                    throw new InvalidOperationException("Unexpected: " + Village.Options.LynchRules);
            }

        }

        private void Lynch(PlayerModel target)
        {
            Log.Write($"{target.Name} is dragged off by the mob and executed!");
            target.Kill();
        }
    }
}