using UnityEngine;

namespace CatchMoon
{
    public static class BowShot
    {
        public static bool FullDraw(string assetName, bool timeFilled, float fullDrawTime, float held)
        {
            if (!timeFilled || fullDrawTime <= 0f)
            {
                Debug.LogError($"{assetName}: fullDrawTime 未填");
                return false;
            }
            return held >= fullDrawTime;
        }

        public static float Damage(string assetName, bool fullDraw, int quickDamage, bool fullFilled, int fullDamage)
        {
            if (!fullDraw) return quickDamage;
            if (!fullFilled || fullDamage <= 0)
            {
                Debug.LogError($"{assetName}: fullDrawDamage 未填");
                return quickDamage;
            }
            return fullDamage;
        }
    }
}
