using EatVillagers.WolfLab.Logic.Models;

namespace EatVillagers.WolfLab.Logic.GameLogic.TrialSystems
{
    /// <summary>
    /// Contains details on how much potential a trial has for swaying
    /// other players. 
    /// </summary>
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