using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Colorful;
using EatVillagers.WolfLab.Logic.Extensions;
using EatVillagers.WolfLab.Logic.Models;
using EatVillagers.WolfLab.Logic.Models.Enums;
using Console = Colorful.Console;


namespace EatVillagers.WolfLab.Logic.Graphics
{
    public class VillageVisualizer
    {
        public void Show(List<PlayerModel> players)
        {
            var villageShape = GetList(players.Count);

            StyleSheet styleSheet = new StyleSheet(Color.White);
            styleSheet.AddStyle("W[0-9]*", Color.DarkRed);
            styleSheet.AddStyle("V[0-9]*", Color.BlanchedAlmond);
            styleSheet.AddStyle("H[0-9]*|S[0-9]*", Color.LightBlue);

            foreach (var row in villageShape)
            {
                var output = row;

                for (var i = 0; i < players.Count; i++)
                {
                    output = output.Replace("{" + i + "}", GetSymbol(players[i]));
                }



                Console.WriteLineStyled(output, styleSheet);
            }

            var playersByTeam = players.OrderBy(p => p.Team() != Teams.Evil).ToList();

            Console.WriteLine();
            Console.WriteLine("Net Aggro for living: ");

            var stats = playersByTeam.Where(p => p.IsAlive).Select(p => p.Name + ": " + p.AverageSuspicion()).ToList();
            Console.WriteLineStyled(string.Join(", ", stats), styleSheet);

            if (players.Any(p => !p.IsAlive))
            {
                Console.WriteLine();
                Console.WriteLine("Net Aggro for dead: ");
                var deadStats = playersByTeam.Where(p => !p.IsAlive).Select(p => p.Name + ": " + p.AverageSuspicion()).ToList();
                Console.WriteLineStyled(string.Join(", ", deadStats), styleSheet);
            }



        }

        private string GetSymbol(PlayerModel player)
        {
            if (!player.IsAlive)
                return "";

            return player.Symbol();
        }

        private List<string> GetList(int count)
        {
            switch (count)
            {
                case 3:
                    return Three();
                case 4:
                    return Four();
                case 5:
                    return Five();
                case 6:
                    return Six();
                case 7:
                    return Seven();
                case 8:
                    return Eight(); 
                case 9:
                    return Nine();
                case 10:
                    return Ten();
                case 11:
                    return Eleven();
                case 12:
                    return Twelve();
                default:
                    return new List<string>();
            }
        }

        public List<string> Three()
        {
            return new List<string>()
            {
                "  {0} ",
                "",
                "{1} {2}"
            };
        }

        public List<string> Four()
        {
            return new List<string>()
            {
                "{0} {1}",
                "",
                "{2} {3}"
            };
        }

        public List<string> Five()
        {
            return new List<string>()
            {
                "   {0} {1}",
                "  {2}    {3}",
                "     {4}"
            };
        }

        public List<string> Six()
        {
            return new List<string>()
            {
                "   {0} {1}",
                " {2}     {3}",
                "   {4} {5}"
            };
        }

        public List<string> Seven()
        {
            return new List<string>()
            {
                "    {0}   {1}",
                "  {2}       {3}",
                "   {4} {6} {5}",
            };
        }

        public List<string> Eight()
        {
            return new List<string>()
            {
                "    {0} {1}  {2}",
                "  {3}         {4}",
                "    {5} {6} {7}",
            };
        }

        public List<string> Nine()
        {
            return new List<string>()
            {
                "    {0} {1} {2}",
                "  {3}         {4}",
                "  {5}         {6}",
                "     {7}  {8}",
            };
        }

        public List<string> Ten()
        {
            return new List<string>()
            {
                "    {0} {1} {2}",
                "  {3}        {4}",
                "  {5}        {6}",
                "    {7} {8} {9}",
            };
        }

        public List<string> Eleven()
        {
            return new List<string>()
            {
                "    {0} {1} {2}",
                "   {3}       {4}",
                "  {5}         {6}",
                "   {7}       {8}",
                "      {9} {10}",
            };
        }

        public List<string> Twelve()
        {
            return new List<string>()
            {
                "    {0} {1} {2}",
                "   {3}       {4}",
                "  {5}         {6}",
                "   {7}       {8}",
                "    {9} {10} {11}",
            };
        }



    }
}
