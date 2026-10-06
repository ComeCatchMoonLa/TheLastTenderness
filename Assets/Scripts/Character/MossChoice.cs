namespace CatchMoon
{
    public static class MossChoice
    {
        public static bool Apply(bool blooming, ref float poisonTime, ref float toxicTime)
        {
            if (blooming)
            {
                if (toxicTime <= 0f) return false;
                toxicTime = 0f;
                return true;
            }
            if (poisonTime <= 0f) return false;
            poisonTime = 0f;
            return true;
        }
    }
}
