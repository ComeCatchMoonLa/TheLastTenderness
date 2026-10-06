namespace CatchMoon
{
    public static class KeyedDoor
    {
        public static bool Allowed(bool requiresKey, bool carrying)
        {
            if (!requiresKey) return true;
            return carrying;
        }
    }
}
