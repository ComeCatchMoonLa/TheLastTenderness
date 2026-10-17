namespace CatchMoon
{
    public enum WeaponArtSpell
    {
        WeaponArt,
        NoSpell,
        Cast
    }

    public static class WeaponCatalyst
    {
        public static WeaponArtSpell Decide(CatalystKind kind, SpellType spell, bool hasSpell)
        {
            if (kind == CatalystKind.none) return WeaponArtSpell.WeaponArt;
            if (!hasSpell || !CatalystMatch.Allows(kind, spell)) return WeaponArtSpell.NoSpell;
            return WeaponArtSpell.Cast;
        }
    }
}
