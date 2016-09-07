using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EatVillagers.WolfLab.Logic.Analytics;
using Z.Core.Extensions;

namespace EatVillagers.WolfLab.Console.UI
{
    public static class ProgressUpdater
    {
        private static int _lastTicks = 0;

        public static void DrawStatusArea(Experiment experiment, int current, int total)
        {
            Colorful.Console.Clear();

            Colorful.Console.Write(experiment.Name, Color.LightGray);
            Colorful.Console.Write(" (", Color.SlateBlue);

            Colorful.Console.Write($"experiment {current} of {total}", Color.DarkGray);

            Colorful.Console.Write(")", Color.SlateBlue);
            Colorful.Console.WriteLine();

            Colorful.Console.Write("[", Color.SlateBlue);
            Colorful.Console.Write(" ".Repeat(10));
            Colorful.Console.Write("]", Color.SlateBlue);

            //reset the state of our progress ticker, so that UpdateProgress starts
            //drawing ticks in the right place.
            _lastTicks = 0;
        }

        public static void UpdateProgress(Experiment experiment, int current, int total)
        {
            //Figure out how many tenths of a percent we've completed.
            //Only proceed with updating progress at increments of 10%.
            var percent = (decimal)current / total;
            var ticks = Math.Floor(percent * 10).ToInt32();

            if (ticks <= _lastTicks)
                return;

            Colorful.Console.SetCursorPosition(1, 1);
            Colorful.Console.Write("*".Repeat(ticks), Color.SlateGray);

            Colorful.Console.SetCursorPosition(13, 1);

            Colorful.Console.Write(current + " / " + total + " samples", Color.LightGray);
            Colorful.Console.SetCursorPosition(0, 4);

            Colorful.Console.Write("Good Win Rate: ", Color.LightGray);

            var goodWinRate = experiment.GoodWinPercentage;

            Colorful.Console.WriteLine(goodWinRate.ToString("0%"),
                                   GetColor(goodWinRate));

            _lastTicks = ticks;
        }
        
        /// <summary>
        /// Generates a blue-ish color that brightens according to percent.
        /// </summary>
        private static Color GetColor(decimal percent)
        {
            var ticks = Math.Floor((percent * 100) / 5) * 5;
            var nearestFivePercent = (decimal)ticks / 100;

            int col = (245 * nearestFivePercent).ToInt32();

            //There's a weird problem with the graphics library. It
            //gets stuck if we push it past 110.
            if (col > 98)
                col = 110;

            return Color.FromArgb(col, col, 255);
        }
    }
}
