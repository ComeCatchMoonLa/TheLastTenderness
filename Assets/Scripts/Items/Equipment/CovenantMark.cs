namespace CatchMoon
{
    public static class CovenantMark
    {
        public static Item Active(Item[] slots)
        {
            if (slots == null) return null;
            int n = slots.Length < EquipmentLayout.RingSlots ? slots.Length : EquipmentLayout.RingSlots;
            for (int i = 0; i < n; i++)
            {
                if (slots[i] == null || !slots[i].isCovenantMark) continue;
                return slots[i];
            }
            return null;
        }
    }
}
