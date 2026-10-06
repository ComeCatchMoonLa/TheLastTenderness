namespace CatchMoon
{
    public static class CatalystMatch
    {
        public static bool Allows(CatalystKind catalyst, SpellType spell)
        {
            if (catalyst == CatalystKind.staff) return spell == SpellType.sorcery;
            if (catalyst == CatalystKind.chime || catalyst == CatalystKind.talisman) return spell == SpellType.miracle;
            if (catalyst == CatalystKind.flame) return spell == SpellType.pyromancy;
            return false;
        }
    }
}
