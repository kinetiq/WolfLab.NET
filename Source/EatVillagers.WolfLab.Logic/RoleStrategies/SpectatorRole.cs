using System;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.RoleStrategies
{
    /// <summary>
    /// This exists as a placeholder for players who haven't been assigned a role
    /// yet. It throws if the role isn't assigned. 
    /// </summary>
    public class SpectatorRole : RoleBase
    {
        public SpectatorRole(PlayerModel player) : base(player)
        {          
        }

        public override void ExecuteDayAction()
        {
            throw new InvalidOperationException("No players with 'SpectatorRole' should be present in the game.");
        }

        public override Roles Role()
        {
            return Roles.Villager;
        }
    }
}