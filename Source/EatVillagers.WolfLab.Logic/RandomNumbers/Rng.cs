namespace EatVillagers.WolfLab.Logic.RandomNumbers
{
    public static class Rng
    {
        private static readonly System.Random random = new System.Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }

        public static int RollD(int ceiling)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(1, ceiling + 1);
            }

        }

    }
}
