namespace CatchMoon
{
    public static class AmmoMatch
    {
        public static bool Allows(WeaponType weapon, AmmoType ammo)
        {
            if (weapon == WeaponType.bow) return ammo == AmmoType.arrow;
            if (weapon == WeaponType.crossbow) return ammo == AmmoType.bolt;
            if (weapon == WeaponType.greatbow) return ammo == AmmoType.greatArrow;
            return false;
        }
    }
}
