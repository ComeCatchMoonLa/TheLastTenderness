namespace CatchMoon
{
    public static class ShieldKick
    {
        public static bool IsKick(bool sprinting, bool movingForward)
        {
            return !sprinting && movingForward;
        }

        public static float Stamina(bool kick, float lightCost)
        {
            return kick ? lightCost : 0f;
        }

        public static bool BreaksShield(bool kickArmed, bool blocking)
        {
            return kickArmed && blocking;
        }
    }
}
