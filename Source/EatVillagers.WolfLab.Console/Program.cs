using EatVillagers.Village.Logic;
using EatVillagers.Village.Logic.Factories;

namespace EatVillagers.Village.Console
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
