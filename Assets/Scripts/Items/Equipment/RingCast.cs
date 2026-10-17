namespace CatchMoon
{
    public static class RingCast
    {
        public static int Spell(Item[] slots)
        {
            int sum = 0;
            int n = Count(slots);
            for (int i = 0; i < n; i++)
            {
                if (slots[i] != null) sum += slots[i].spellBonus;
            }
            return sum;
        }

        public static float Critical(Item[] slots)
        {
            float sum = 0f;
            int n = Count(slots);
            for (int i = 0; i < n; i++)
            {
                if (slots[i] != null) sum += slots[i].criticalBonus;
            }
            return sum;
        }

        static int Count(Item[] slots)
        {
            if (slots == null) return 0;
            return slots.Length < EquipmentLayout.RingSlots ? slots.Length : EquipmentLayout.RingSlots;
        }
    }
}
