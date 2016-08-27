using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.RoleStrategies
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