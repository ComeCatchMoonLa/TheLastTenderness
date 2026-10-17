using UnityEngine;

namespace CatchMoon
{
    public static class StanceDeriveFocus
    {
        public static bool TryLaunch(float mp, int cost, out float next)
        {
            next = mp;
            if (cost <= 0) return false;
            if (mp < cost) return false;
            next = mp - cost;
            return true;
        }

        public static bool Commit(CharacterManager character, bool light)
        {
            if (!character.stanceHolding) return true;

            WeaponItem weapon = character.cInventory != null
                ? character.cInventory.currentItemBeingUsed as WeaponItem
                : null;
            if (weapon == null)
            {
                character.stanceDerive = WeaponStance.TakeSwing(true, light);
                character.stanceHolding = false;
                return true;
            }

            bool filled = light ? weapon.lightDeriveFocusFilled : weapon.heavyDeriveFocusFilled;
            int cost = light ? weapon.lightDeriveFocus : weapon.heavyDeriveFocus;
            string field = light ? "lightDeriveFocus" : "heavyDeriveFocus";
            if (!filled || cost <= 0)
            {
                Debug.LogError($"{weapon.name}: {field} 未填");
                return false;
            }

            if (!TryLaunch(character.cStats.currentMP, cost, out float next))
                return false;

            character.cStats.DeductMP(character.cStats.currentMP - next);
            character.stanceDerive = WeaponStance.TakeSwing(true, light);
            character.stanceHolding = false;
            return true;
        }
    }
}
