namespace CatchMoon
{
    public enum AmmoStatusSlot
    {
        None,
        Poison,
        Bleed,
        Frost,
        Curse
    }

    public static class AmmoStatus
    {
        public static bool Apply(CharacterStatsManager stats, AmmoStatusSlot slot, float gain)
        {
            if (stats == null || slot == AmmoStatusSlot.None || gain <= 0f) return false;
            if (slot == AmmoStatusSlot.Poison) stats.GainPoison(gain, 0f);
            else if (slot == AmmoStatusSlot.Bleed) stats.GainBleed(gain, 0f);
            else if (slot == AmmoStatusSlot.Frost) stats.GainFrost(gain, 0f);
            else stats.GainCurse(gain, 0f);
            return true;
        }
    }
}
