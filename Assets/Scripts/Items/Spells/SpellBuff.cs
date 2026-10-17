using UnityEngine;

namespace CatchMoon
{
    public static class SpellBuff
    {
        public static bool TryCoat(WeaponItem weapon, float duration, DamageSegments extra, out WeaponBuffState state)
        {
            state = default;
            if (weapon == null) return false;
            return WeaponBuff.TryApply(weapon.name, weapon.infusion, weapon.cannotBuff, duration, extra, weapon, out state);
        }
    }
}
