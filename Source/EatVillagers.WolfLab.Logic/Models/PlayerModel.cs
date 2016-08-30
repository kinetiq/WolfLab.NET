using System.Collections.Generic;
using System.Linq;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models.Enums;
using EatVillagers.WolfLab.Logic.RoleStrategies;

namespace EatVillagers.WolfLab.Logic.Models
{
    public class PlayerModel
    {
        public string Name { get; set; } = "";
        public int Skill { get; set; }

        public RoleBase RoleLogic { get; set; }
        public Roles Role => RoleLogic.Role();

        public bool IsAlive { get; set; } = true;
        public bool IsClaimed { get; set; } = false;
        public VillageModel Village;

        public void Kill()
        {
            RoleLogic.Kill();
        }
    }
}
