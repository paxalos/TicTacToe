namespace Utils
{
    public static class Extensions
    {
        public static int IncrementInRange(this int value, int max)
        {
            return (value + 1) % max;
        }
    }
}