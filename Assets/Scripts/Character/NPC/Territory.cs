namespace CatchMoon
{
    public static class Territory
    {
        public static bool PulledOut(bool boss, float distance, float activity)
        {
            if (boss || activity <= 0f) return false;
            return distance > activity;
        }

        public static float Refill(float current, float max)
        {
            return max;
        }

        public static bool Resume(bool returning, bool hit, float distance, float engage)
        {
            if (!returning || !hit || engage <= 0f) return false;
            return distance <= engage;
        }
    }
}
