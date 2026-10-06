using UnityEngine;

namespace CatchMoon
{
    public static class BossThickness
    {
        public static bool TryRaise(float solo, bool allyPresent, float multiplier, string assetName, out float max)
        {
            max = solo;
            if (!allyPresent) return false;
            if (multiplier <= 0f)
            {
                Debug.LogError($"{assetName}: allyHealthMultiplier 未填");
                return false;
            }
            max = solo * multiplier;
            return true;
        }
    }
}
