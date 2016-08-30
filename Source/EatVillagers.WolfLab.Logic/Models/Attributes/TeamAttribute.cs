using System;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.Models.Attributes
{
    public class EvilTeamAttribute : TeamAttribute
    {
        public EvilTeamAttribute()
        {
            base.Team = Teams.Evil;
        }
    }

    public class GoodTeamAttribute : TeamAttribute
    {
        public GoodTeamAttribute()
        {
            base.Team = Teams.Good;
        }
    }

    public class NoTeamAttribute : TeamAttribute
    {
        public NoTeamAttribute()
        {
            base.Team = Teams.Spectator;
        }
    }

    public abstract class TeamAttribute : Attribute
    {
        public Teams Team { get; set; }
    }
}
