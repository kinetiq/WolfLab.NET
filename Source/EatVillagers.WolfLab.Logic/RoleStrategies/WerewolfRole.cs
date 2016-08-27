using EatVillagers.Village.Logic.Analytics;
using EatVillagers.Village.Logic.Extensions;
using EatVillagers.Village.Logic.Models;
using EatVillagers.Village.Logic.Models.Enums;

namespace EatVillagers.Village.Logic.RoleStrategies
{
    public class WerewolfRole : RoleBase
    {
        public WerewolfRole(PlayerModel player) : base(player)
        {
        }

        public override Roles Role()
        {
            return Roles.Werewolf;
        }

        public override void ExecuteDayAction()
        {
            //TODO: foment chaos.

            return;
        }

        public override void ExecuteNightAction()
        {
            EatIfLeadWolf();
        }

        private void EatIfLeadWolf()
        {
            var village = Player.Village;
            var wolf = Player;

            if (wolf != village.LeadWerewolf()) //the smartest wolf will control the eating.
                return; 
          
            var target = wolf.GetYummiest();
            Log.Write($"The wolves devour {target.Name}, a {target.Role}.");
            target.Kill();            
        }
    }
}