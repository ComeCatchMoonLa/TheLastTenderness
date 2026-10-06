namespace CatchMoon
{
    public static class SpellOnShield
    {
        public static bool UseShield(bool blocking, bool fromFront)
        {
            return blocking && fromFront;
        }

        public static float StaminaCost(float damageSum, float guardBreak, float stabilityRating, float weaponStability)
        {
            return damageSum * guardBreak * (1f - stabilityRating) * (1f - weaponStability);
        }

        public static DamageSegments Absorb(DamageSegments incoming, DamageSegments rates)
        {
            return new DamageSegments
            {
                Physical = incoming.Physical * (1f - rates.Physical),
                Fire = incoming.Fire * (1f - rates.Fire),
                Magic = incoming.Magic * (1f - rates.Magic),
                Lightning = incoming.Lightning * (1f - rates.Lightning),
                Dark = incoming.Dark * (1f - rates.Dark)
            };
        }
    }
}
