namespace CatchMoon
{
    public static class RingBonus
    {
        public static float Attack(Item[] slots)
        {
            float sum = 0f;
            int n = Count(slots);
            for (int i = 0; i < n; i++)
            {
                if (slots[i] != null) sum += slots[i].attackBonus;
            }
            return sum;
        }

        public static float Absorption(Item[] slots)
        {
            float sum = 0f;
            int n = Count(slots);
            for (int i = 0; i < n; i++)
            {
                if (slots[i] != null) sum += slots[i].absorptionBonus;
            }
            return sum;
        }

        public static int Attribute(Item[] slots)
        {
            int sum = 0;
            int n = Count(slots);
            for (int i = 0; i < n; i++)
            {
                if (slots[i] != null) sum += slots[i].attributeBonus;
            }
            return sum;
        }

        public static float Physical(float damage, float attack, float absorption)
        {
            float next = damage + attack - absorption;
            return next < 0f ? 0f : next;
        }

        static int Count(Item[] slots)
        {
            if (slots == null) return 0;
            return slots.Length < EquipmentLayout.RingSlots ? slots.Length : EquipmentLayout.RingSlots;
        }
    }
}
