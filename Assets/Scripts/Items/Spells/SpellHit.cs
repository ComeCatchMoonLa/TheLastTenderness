namespace CatchMoon
{
    public static class SpellHit
    {
        public static DamageSegments Of(SpellType type, int amount)
        {
            DamageSegments segments = default;
            if (type == SpellType.pyromancy) segments.Fire = amount;
            else if (type == SpellType.sorcery) segments.Magic = amount;
            else if (type == SpellType.miracle) segments.Lightning = amount;
            else if (type == SpellType.dark) segments.Dark = amount;
            return segments;
        }
    }
}
