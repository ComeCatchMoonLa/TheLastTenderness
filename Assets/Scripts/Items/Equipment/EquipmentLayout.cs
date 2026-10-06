namespace CatchMoon
{
    public static class EquipmentLayout
    {
        public const int WeaponsPerHand = 3;
        public const int ArmorSlots = 4;
        public const int RingSlots = 4;
        public static readonly string[] ArmorSlotNames = { "头", "胸", "手", "腿" };

        public static T[] Ensure<T>(T[] slots, int count)
        {
            if (slots != null && slots.Length >= count) return slots;
            T[] next = new T[count];
            if (slots == null) return next;
            int n = slots.Length < count ? slots.Length : count;
            for (int i = 0; i < n; i++) next[i] = slots[i];
            return next;
        }
    }
}
