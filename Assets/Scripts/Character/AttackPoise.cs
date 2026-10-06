namespace CatchMoon
{
    public static class AttackPoise
    {
        public static bool Holds(float poiseLeft, float hit, out float next)
        {
            next = poiseLeft - hit;
            if (next < 0f) next = 0f;
            return next > 0f;
        }
    }
}
