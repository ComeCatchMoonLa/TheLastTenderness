namespace CatchMoon
{
    public static class RingRecovery
    {
        public static float Stamina(Item[] slots)
        {
            float sum = 0f;
            int n = Count(slots);
            for (int i = 0; i < n; i++)
            {
                if (slots[i] != null) sum += slots[i].staminaRegenBonus;
            }
            return sum;
        }

        public static int Estus(Item[] slots)
        {
            return Sum(slots, true);
        }

        public static int Ashen(Item[] slots)
        {
            return Sum(slots, false);
        }

        static int Sum(Item[] slots, bool estus)
        {
            int sum = 0;
            int n = Count(slots);
            for (int i = 0; i < n; i++)
            {
                if (slots[i] == null) continue;
                sum += estus ? slots[i].estusBonus : slots[i].ashenBonus;
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
