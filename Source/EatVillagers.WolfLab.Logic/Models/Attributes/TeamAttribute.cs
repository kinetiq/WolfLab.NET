using System;
using EatVillagers.Village.Logic.Models.Enums;

namespace EatVillagers.Village.Logic.Models.Attributes
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

    public abstract class TeamAttribute : Attribute
    {
        public Teams Team { get; set; }
    }
}
