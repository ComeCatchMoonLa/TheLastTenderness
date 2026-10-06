namespace CatchMoon
{
    public static class WeaponNeed
    {
        public static bool TryStrength(WeaponItem weapon, out int value)
        {
            value = weapon.strengthNeed;
            return weapon.strengthNeedFilled;
        }

        public static bool TryDexterity(WeaponItem weapon, out int value)
        {
            value = weapon.dexterityNeed;
            return weapon.dexterityNeedFilled;
        }
    }
}
