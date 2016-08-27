namespace EatVillagers.Village.Logic.Extensions
{
    public static class MathExtensions
    {
        public static int UpperCap(this int value, int cap)
        {
            return value > cap ? cap : value;
        }

        public static int LowerCap(this int value, int cap)
        {
            return value < cap ? cap : value;
        }

        public static decimal UpperCap(this decimal value, decimal cap)
        {
            return value > cap ? cap : value;
        }

        public static decimal LowerCap(this decimal value, decimal cap)
        {
            return value < cap ? cap : value;
        }
    }
}
