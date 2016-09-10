namespace EatVillagers.WolfLab.Logic.RandomNumbers
{
    /// <summary>
    /// Wraps a Random in some sync-lock goo to make it thread-safe. I wouldn't encrypt
    /// credit cards with it, but it's servicable for our purposes.
    /// </summary>
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

        /// <summary>
        /// Returns a random value between 1 and diceSides. Unlike calls to Random,
        /// diceSides is actually a possible result of this method.
        /// </summary>
        public static int RollD(int diceSides)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(1, diceSides + 1);
            }
        }
    }
}
