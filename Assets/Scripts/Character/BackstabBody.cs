namespace CatchMoon
{
    public static class BackstabBody
    {
        public static bool Allows(bool boss, bool large)
        {
            return Allows(boss, large, false);
        }

        public static bool Allows(bool boss, bool large, bool beast)
        {
            if (boss || large || beast) return false;
            return true;
        }
    }
}
