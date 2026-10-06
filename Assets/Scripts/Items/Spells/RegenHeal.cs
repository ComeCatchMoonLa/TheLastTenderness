namespace CatchMoon
{
    public static class RegenHeal
    {
        public static float Apply(float current, float max, float perSecond, float seconds)
        {
            if (perSecond <= 0f || seconds <= 0f) return current;
            float next = current + perSecond * seconds;
            if (next > max) return max;
            return next;
        }
    }
}
