using EatVillagers.WolfLab.Logic.Analytics;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.GameLogic;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.RoleStrategies
{
    public abstract class RoleBase
    {
        protected readonly PlayerModel Player;

        protected RoleBase(PlayerModel player)
        {
            Player = player;
        }

        public abstract Roles Role();

        public virtual void ExecuteDayAction()
        {
            //Each day, villagers should have a chance to look more (or less) suspicious 
            //to the other players. 
            //
            //
            //

            var suspicionLevel = SuspicionGenerator.Generate(Player);
            Responses.HandleGoodResponseToSuspiciousBehavior(Player, suspicionLevel);

            return;
        }
        

        public virtual void ExecuteNightAction()
        {
            return;
        }

        public virtual TrialImpact ExecuteTrialDefense()
        {
            return new TrialImpact(Player);
        }

        public virtual void Kill()
        {
            Player.IsAlive = false;
        }
    }
}
