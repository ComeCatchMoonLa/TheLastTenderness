namespace CatchMoon
{
    public static class SpellPower
    {
        public static bool TryRead(WeaponItem catalyst, out float power)
        {
            power = catalyst.spellPower;
            return catalyst.spellPowerFilled;
        }
    }
}
