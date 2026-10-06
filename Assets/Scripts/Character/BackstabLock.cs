namespace CatchMoon
{
    public static class BackstabLock
    {
        public static bool TryEnter(bool inRange, float dot, bool alreadyLocked, out bool locked)
        {
            locked = alreadyLocked;
            if (alreadyLocked || !inRange) return false;
            if (dot < -1f || dot > -0.8f) return false;
            locked = true;
            return true;
        }
    }
}
