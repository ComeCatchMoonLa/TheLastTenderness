namespace CatchMoon
{
    public static class EmptyDraw
    {
        public const string Clip = "Aiming - Start";

        public static bool Plays(int remaining)
        {
            return remaining <= 0;
        }
    }
}
