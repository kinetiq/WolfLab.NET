using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EatVillagers.WolfLab.Logic.Factories;
using EatVillagers.WolfLab.Logic.Models.Enums;
using WolfLab.Core.Configuration;
using Z.Core.Extensions;

namespace EatVillagers.WolfLab.Console.ProjectLoader
{
    public static class ExperimentExtensions
    {
        public static void UpdateGameOptions(this ExperimentModel experiment, GameOptions options)
        {
            //Okay, this probably isn't the optimal structure. Maybe this shouldn't be an extension
            //method.
            foreach (var variableName in experiment.Variables.Keys)
                ApplyVariable(experiment, variableName, options);
        }

        private static void ApplyVariable(ExperimentModel experiment, string variableName, GameOptions options)
        {
            var value = experiment.Variables[variableName];

            switch (variableName.ToLower())
            {
                case "lynchrule":
                    ApplyLynchRule(value, options);
                    break;
                default:
                    throw new InvalidOperationException("Unexpected Variable Name: " + variableName);
            }
        }

        private static void ApplyLynchRule(string value, GameOptions options)
        {
            var rule = value.ToEnum<LynchRules>();
            options.LynchRules = rule;
        }
    }
}
