using EatVillagers.WolfLab.Logic.Models.Attributes;

namespace EatVillagers.WolfLab.Logic.Models.Enums
{
    public enum Roles
    {
        [GoodTeam]
        Villager,

        [GoodTeam]
        Seer,

        [NoTeam]
        Spectator,

        [GoodTeam]
        Hunter,

        [EvilTeam]
        Werewolf
    }
}
