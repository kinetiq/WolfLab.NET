using System;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.RoleStrategies
{
    /// <summary>
    /// This exists as a placeholder for players who haven't been assigned a role
    /// yet. If this role makes it into the game somehow, it will throw exceptions
    /// becuase that shouldn't happen.
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