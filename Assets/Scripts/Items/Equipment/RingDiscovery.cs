namespace CatchMoon
{
    public static class RingDiscovery
    {
        public static int Total(int luck, Item[] slots)
        {
            int sum = 100 + luck;
            if (slots == null) return sum;
            int n = slots.Length < EquipmentLayout.RingSlots ? slots.Length : EquipmentLayout.RingSlots;
            for (int i = 0; i < n; i++)
            {
                if (slots[i] != null) sum += slots[i].discoveryBonus;
            }
            return sum;
        }
    }
}
