namespace CatchMoon
{
    public static class SpellHeal
    {
        public static float Apply(float current, float max, float amount)
        {
            if (amount <= 0f) return current;
            float next = current + amount;
            return next > max ? max : next;
        }
    }
}
