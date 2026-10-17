namespace CatchMoon
{
    public static class InfusionSwap
    {
        public static bool Replace(WeaponItem weapon, string next)
        {
            if (weapon == null) return false;
            if (!InfusionGate.Allows(weapon.weaponType, weapon.torch)) return false;
            weapon.infusion = next;
            return true;
        }
    }
}
