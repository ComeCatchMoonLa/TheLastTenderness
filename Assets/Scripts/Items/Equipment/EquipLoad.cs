namespace CatchMoon
{
    public static class EquipLoad
    {
        public static float Total(WeaponItem[] left, WeaponItem[] right, Item[] rings)
        {
            return Sum(left) + Sum(right) + Sum(rings);
        }

        static float Sum(Item[] items)
        {
            if (items == null) return 0f;
            float sum = 0f;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null) sum += items[i].weight;
            }
            return sum;
        }
    }
}
