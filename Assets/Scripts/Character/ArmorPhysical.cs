namespace CatchMoon
{
    public static class ArmorPhysical
    {
        public static float Of(ArmorItem armor, PhysicalKind kind)
        {
            if (armor == null) return 0f;
            if (kind == PhysicalKind.Strike && armor.strikeFilled) return armor.strikeDA;
            if (kind == PhysicalKind.Slash && armor.slashFilled) return armor.slashDA;
            if (kind == PhysicalKind.Thrust && armor.thrustFilled) return armor.thrustDA;
            return armor.physicalDA;
        }
    }
}
