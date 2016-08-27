using EatVillagers.Village.Logic.Models;
using EatVillagers.Village.Logic.Models.Enums;

namespace EatVillagers.Village.Logic.RoleStrategies
{
    public class VillagerRole : RoleBase
    {
        public VillagerRole(PlayerModel player) : base(player)
        {
        }

        public override Roles Role()
        {
            return Roles.Villager;
        }
    }
}