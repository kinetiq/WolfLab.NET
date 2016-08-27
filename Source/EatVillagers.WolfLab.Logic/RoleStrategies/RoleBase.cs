using EatVillagers.Village.Logic.Models;
using EatVillagers.Village.Logic.Models.Enums;

namespace EatVillagers.Village.Logic.RoleStrategies
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
            //TODO: basic villager action.
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

    public class TrialImpact
    {
        public PlayerModel Defendant;
        public decimal OpinionShift = 0;

        public TrialImpact(PlayerModel defendant)
        {
            Defendant = defendant;
        }
    }
}
