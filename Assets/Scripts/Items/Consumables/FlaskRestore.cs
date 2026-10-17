namespace CatchMoon
{
    public static class FlaskRestore
    {
        public static bool RestoresFocus(FlaskType type)
        {
            return type == FlaskType.ashen;
        }

        public static void Apply(FlaskType type, int amount, ref float hp, ref float mp, float maxHp, float maxMp)
        {
            if (amount <= 0) return;
            if (RestoresFocus(type))
            {
                if (mp >= maxMp) return;
                float raised = mp + amount;
                mp = raised < maxMp ? raised : maxMp;
                return;
            }

            if (hp >= maxHp) return;
            float next = hp + amount;
            hp = next < maxHp ? next : maxHp;
        }
    }
}
