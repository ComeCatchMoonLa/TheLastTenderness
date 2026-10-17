namespace CatchMoon
{
    public static class ParryShield
    {
        public static bool UseLeft(bool twoHanding, WeaponType leftType, bool leftStanceArt, bool leftArtIsParry)
        {
            return !twoHanding
                && leftType == WeaponType.melee_OH_Shield
                && !leftStanceArt
                && leftArtIsParry;
        }
    }
}
