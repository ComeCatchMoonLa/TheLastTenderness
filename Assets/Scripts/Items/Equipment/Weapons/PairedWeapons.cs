namespace CatchMoon
{
    public static class PairedWeapons
    {
        public static bool IsPaired(WeaponType type)
        {
            return type == WeaponType.melee_OH_DualWield;
        }

        public static bool AsTwoHand(WeaponType attacking, bool alreadyTwoHanding)
        {
            return IsPaired(attacking) || alreadyTwoHanding;
        }

        public static bool ShieldBlocked(WeaponType right, WeaponType left)
        {
            bool paired = IsPaired(right) || IsPaired(left);
            bool shield = right == WeaponType.melee_OH_Shield || left == WeaponType.melee_OH_Shield;
            return paired && shield;
        }
    }
}
