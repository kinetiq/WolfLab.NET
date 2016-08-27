using EatVillagers.WolfLab.Logic;
using EatVillagers.WolfLab.Logic.Factories;

namespace EatVillagers.WolfLab.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new GameOptions();
            var game = new Game(options);

            game.ExecuteGameLoop();
        }
    }
}
