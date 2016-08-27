using EatVillagers.Village.Logic.Models.Attributes;

namespace EatVillagers.Village.Logic.Models.Enums
{
    public enum Roles
    {
        [GoodTeam]
        Villager,

        [GoodTeam]
        Seer,

        [GoodTeam]
        Hunter,

        [EvilTeam]
        Werewolf
    }
}
