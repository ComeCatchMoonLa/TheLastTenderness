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

        public static bool TryIntelligence(WeaponItem weapon, out int value)
        {
            value = weapon.intelligenceNeed;
            return weapon.intelligenceNeedFilled;
        }

        public static bool TryFaith(WeaponItem weapon, out int value)
        {
            value = weapon.faithNeed;
            return weapon.faithNeedFilled;
        }

        public static bool TryLuck(WeaponItem weapon, out int value)
        {
            value = weapon.luckNeed;
            return weapon.luckNeedFilled;
        }
    }
}
