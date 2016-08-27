using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.RoleStrategies
{
    public class HunterRole : RoleBase
    {
        public HunterRole(PlayerModel player) : base(player)
        {
        }

        public override Roles Role()
        {
            return Roles.Hunter;
        }

        public override void Kill()
        {
            var hunterVictom = Player.ExecuteBrutalKill();

            Log.Write($"{Player.Name}, the hunter, brutally kills {hunterVictom.Name}, a {hunterVictom.Role}.");

            base.Kill();
        }
    }
}