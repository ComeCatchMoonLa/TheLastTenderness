namespace CatchMoon
{
    public static class LockMark
    {
        public static bool Shown(bool locked, bool targetDead)
        {
            return locked && !targetDead;
        }
    }
}
