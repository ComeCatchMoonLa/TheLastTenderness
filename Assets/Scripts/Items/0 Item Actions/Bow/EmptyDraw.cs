namespace CatchMoon
{
    public static class EmptyDraw
    {
        public const string Clip = "Shoot Arrow";

        public static bool Plays(int remaining)
        {
            return remaining <= 0;
        }
    }
}
