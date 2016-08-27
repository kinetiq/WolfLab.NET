namespace EatVillagers.Village.Logic.Analytics
{
    /// <summary>
    /// Singleton used to record gameplay statistics.
    /// </summary>
    public class Stats
    {
        public static readonly Stats Instance = new Stats();

        private Stats()
        {
        }
    }
}
