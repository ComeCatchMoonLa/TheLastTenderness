namespace CatchMoon
{
    public static class WeaponBlock
    {
        public static bool Use(bool twoHanding, WeaponType type)
        {
            return twoHanding
                && type != WeaponType.melee_OH_Shield
                && type != WeaponType.unarmed
                && type != WeaponType.bow;
        }
    }
}
