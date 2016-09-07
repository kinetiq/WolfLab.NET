using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Factories;
using EatVillagers.WolfLab.Logic.Models;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public class DayLogic
    {
        private GameOptions Options;
        private readonly VillageModel Village;

        public DayLogic(GameOptions options, VillageModel village)
        {
            Options = options;
            Village = village;
        }

        public void ExecuteDay()
        {         
            foreach (var player in Village.LivingPlayers().Shuffle())
            {
                player.RoleLogic.ExecuteDayAction();
            }

            Village.TrialStrategy.ExecuteTrial();
        }
    }
}
