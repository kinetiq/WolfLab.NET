using System.Collections.Generic;
using System.Linq;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models.Enums;
using EatVillagers.WolfLab.Logic.RoleStrategies;

namespace EatVillagers.WolfLab.Logic.Models
{
    public class PlayerModel
    {
        public string ID { get; set; } = ""; //Unique ID that persists across games.
        public int Skill { get; set; }
        public int Score { get; set; } = 0; //for tournaments
        public List<Traits> Traits { get; set; } = new List<Traits>();

        public string Name { get; set; } = ""; //Game-specific ID like W1 (wolf 1).
        public RoleBase RoleLogic { get; set; }
        public Roles Role => RoleLogic.Role();
        public bool IsAlive { get; set; } = true;
        public bool IsClaimed { get; set; } = false;
        public VillageModel Village;

        public void Kill()
        {
            RoleLogic.Kill();
        }

        /// <summary>
        /// Resets any game-specific flags. This allows the player to be re-used
        /// across many games.
        /// </summary>
        public void Reset()
        {
            Name = ID; //not really important. 
            RoleLogic = new SpectatorRole(this);
            IsAlive = true;
            IsClaimed = false;
        }
    }
}
