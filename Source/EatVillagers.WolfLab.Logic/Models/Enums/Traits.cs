using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EatVillagers.WolfLab.Logic.Models.Attributes;

namespace EatVillagers.WolfLab.Logic.Models.Enums
{
    public enum Traits
    {      
        // Presence
        [Quality("Presence")]
        Overbearing,

        [Quality("Presence")]
        Quiet,

        //Insight
        [Quality("Insight")]
        Perceptive,

        [Quality("Insight")]
        Oblivious,

        //Charm
        [Quality("Persuasion")]
        Convincing,

        [Quality("Persuasion")]
        LooksGuilty,

        //Investigation Styles

        //[Quality("GoodStyle")]
        //InterrogationHunter,

        //[Quality("GoodStyle")]
        //PatternHunter,

        //[Quality("GoodStyle")]
        //RandomHunter,

        ////Wolfkill Styles

        //[Quality("WolfStyle")]
        //ClearKiller,

        //[Quality("WolfStyle")]
        //SkillKiller,

        //[Quality("WolfStyle")]
        //RandomKiller
    }
}
