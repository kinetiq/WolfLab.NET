using System.Collections.Generic;
using System.Linq;
using EatVillagers.Village.Logic.Extensions;
using EatVillagers.Village.Logic.Models.Enums;
using EatVillagers.Village.Logic.RoleStrategies;

namespace EatVillagers.Village.Logic.Models
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

        public List<PlayerModel> OtherLivingPlayers()
        {
            return Village.Players.Where(x => x != this).ToList();
        }

        public List<PlayerModel> OtherGoodLivingPlayers()
        {
            return Village.Players
                          .Where(x => x != this && x.Team() == Teams.Good && x.IsAlive)
                          .ToList();
        }

        public List<PlayerModel> OtherEvilLivingPlayers()
        {
            return Village.Players
                          .Where(x => x != this && x.Team() == Teams.Evil && x.IsAlive)
                          .ToList();
        }

        public void Kill()
        {
            RoleLogic.Kill();
        }
    }
}
