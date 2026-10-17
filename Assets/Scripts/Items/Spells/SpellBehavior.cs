using UnityEngine;

namespace CatchMoon
{
    public static class SpellBehavior
    {
        public static int Strikes(int count)
        {
            return count < 1 ? 1 : count;
        }

        public static bool TryAreaDamage(string assetName, bool percentFilled, float percent, float maxHp, out float damage)
        {
            damage = 0f;
            if (!percentFilled || percent <= 0f)
            {
                Debug.LogError($"{assetName}: areaPercent 未填");
                return false;
            }

            damage = maxHp * percent;
            return true;
        }
    }
}
