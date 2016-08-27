using System;
using System.Linq;
using EatVillagers.Village.Logic.Analytics;
using EatVillagers.Village.Logic.Extensions;
using EatVillagers.Village.Logic.Models;
using EatVillagers.Village.Logic.Models.Enums;

namespace EatVillagers.Village.Logic.RoleStrategies
{
    class SeerRole : RoleBase
    {
        public SeerRole(PlayerModel player) : base(player)
        {
        }

        public override Roles Role()
        {
            return Roles.Seer;
        }

        public override void ExecuteDayAction()
        {
            if (Player.CheckSeerReveal())
                ClaimAndReveal();
            else
                base.ExecuteDayAction(); //normal villager behavior.

            //Secretly pass info?
        }

        public override TrialImpact ExecuteTrialDefense()
        {
            var village = Player.Village;
            var voters = village.LivingPlayers().Count - 1;
            var lynchRequirement = Math.Ceiling((decimal) voters/2) + 1;
            var lynchers = Player.OtherLivingPlayers()
                                 .Count(x => x.WouldLynch(Player));

            //TODO: make this more skill based.
            if (lynchers >= lynchRequirement)
            {
                Log.Write($"\t{Player.Name} panics!");

                ClaimAndReveal(indent: true);

                return new TrialImpact(Player) { OpinionShift = 0 }; //already handled.
            }

            return new TrialImpact(Player) { OpinionShift = 0 };
        }

        private void ClaimAndReveal(bool indent = false) 
        {
            var village = base.Player.Village;
            var seer = Player; //just an alias; improves readability.

            var prefix = indent ? "\t" : "";

            //REVEAL!

            Log.Write($"{prefix}{seer.Name}, the Seer, claims and reveals!");
            Log.Write($"{prefix}\tCleared: " + string.Join(", ", seer.ClearedVillagers().Select(x => x.Name)));
            Log.Write($"{prefix}\tAccused: " + string.Join(", ", seer.ScannedWolves().Select(x => x.Name)));

            //Seer is top priority kill for all evil players.
            foreach (var p in village.LivingEvilPlayers())
            {
                var opinion = p.GetEvilOpinionOf(seer);
                opinion.EatPriority = 1;
                opinion.IsEvil = false;
            }

            //Seer is cleared for all good players.
            foreach (var p in seer.OtherGoodLivingPlayers())
            {
                var opinion = p.GetGoodOpinionOf(seer);
                opinion.Suspicion = 0;
                opinion.IsCleared = true;
                opinion.IsEvil = false;

                //set the opinion of the wolf.
                foreach (var wolf in seer.ScannedWolves())
                {
                    var wolfOpinion = p.GetGoodOpinionOf(wolf);
                    wolfOpinion.IsCleared = false;
                    wolfOpinion.IsEvil = true;
                    wolfOpinion.Suspicion = 1;
                }
            }
        }

        public override void ExecuteNightAction()
        {
            AwakenAndSearchForWerewolves();  
        }

        private void AwakenAndSearchForWerewolves()
        {
            var seer = Player;

            var target = seer.GetBestSeerCandidate();

            if (target == null) //everyone has been scanned!
                return;

            var opinion = seer.GetGoodOpinionOf(target);

            var isPositive = (target.Role == Roles.Werewolf);
            var scanText = isPositive ? "WEREWOLF!" : "not a wolf";

            Log.Write($"{seer.Name}, the Seer, awakens and scans {target.Name}: {scanText}");

            if (target.Role == Roles.Werewolf)
            {
                opinion.IsCleared = false;
                opinion.IsEvil = true;
                opinion.Suspicion = 1;
            }
            else
            {
                opinion.IsCleared = true;
                opinion.IsEvil = false;
                opinion.Suspicion = 0;
            }
        }
    }
}
