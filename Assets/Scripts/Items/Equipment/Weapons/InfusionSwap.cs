namespace CatchMoon
{
    public static class InfusionSwap
    {
        public static void Replace(WeaponItem weapon, string next)
        {
            weapon.infusion = next;
        }
    }
}
