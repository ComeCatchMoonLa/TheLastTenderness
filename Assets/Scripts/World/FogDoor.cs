namespace CatchMoon
{
    public static class FogDoor
    {
        public static void Begin(ref float current, float max)
        {
            if (max <= 0f) return;
            current = max;
        }

        public static bool CanLeave(bool fightActive)
        {
            return !fightActive;
        }
    }
}
