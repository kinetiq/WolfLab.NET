using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.RoleStrategies
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

        public override void ExecuteNightAction()
        {
            EatIfLeadWolf();
        }

        private void EatIfLeadWolf()
        {
            var village = Player.Village;
            var wolf = Player;

            if (wolf != village.GetLeadWerewolf()) //the smartest wolf will control the eating.
                return;

            if (wolf.OtherGoodLivingPlayers().Count == 0)
                return;
          
            var target = wolf.GetYummiest();
            Log.Write($"The wolves devour {target.Name}, a {target.Role}.");
            target.Kill();            
        }
    }
}