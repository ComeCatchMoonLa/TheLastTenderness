namespace CatchMoon
{
    public static class EmberHealth
    {
        public static bool TryRaise(float maxHP, float ratio, out float newMax)
        {
            if (ratio <= 0f)
            {
                newMax = maxHP;
                return false;
            }
            newMax = maxHP * (1f + ratio);
            return true;
        }
    }
}
