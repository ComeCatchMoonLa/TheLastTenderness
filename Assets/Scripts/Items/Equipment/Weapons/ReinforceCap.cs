namespace CatchMoon
{
    public static class ReinforceCap
    {
        public const int Normal = 10;
        public const int Scale = 5;
        public const int Twinkling = 5;

        public static bool TryRaise(WeaponItem weapon, int cap)
        {
            if (weapon.reinforceLevel >= cap) return false;
            weapon.reinforceLevel += 1;
            return true;
        }
    }
}
