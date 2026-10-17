namespace CatchMoon
{
    public static class BackstabBody
    {
        public static bool Allows(bool boss, bool large)
        {
            if (boss || large) return false;
            return true;
        }
    }
}
