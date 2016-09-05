using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EatVillagers.WolfLab.Logic.Factories;
using EatVillagers.WolfLab.Logic.Models.Enums;
using WolfLab.Core.Configuration;

namespace EatVillagers.WolfLab.Console.ProjectLoader
{
    public static class ProjectExtensions
    {
        public static GameOptions InitializeOptions(this ProjectModel project)
        {
            var options = new GameOptions()
            {
                CrunchMode = project.CrunchMode,
                VillageSize = project.MinVillage,
            };

            return options;
        }
    }
}
