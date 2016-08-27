using EatVillagers.Village.Logic.Analytics;
using EatVillagers.Village.Logic.Extensions;
using EatVillagers.Village.Logic.Models;
using EatVillagers.Village.Logic.Models.Enums;

namespace EatVillagers.Village.Logic.RoleStrategies
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