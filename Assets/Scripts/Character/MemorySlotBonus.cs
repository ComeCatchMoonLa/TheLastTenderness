namespace CatchMoon
{
    public static class MemorySlotBonus
    {
        public static int Total(int focusLevel, int[] bonuses)
        {
            int extra = 0;
            int n = 0;
            if (bonuses != null)
                n = bonuses.Length < EquipmentLayout.RingSlots ? bonuses.Length : EquipmentLayout.RingSlots;
            for (int i = 0; i < n; i++) extra += bonuses[i];
            return CharacterStatsManager.MemorySlotCount(focusLevel) + extra;
        }
    }
}
