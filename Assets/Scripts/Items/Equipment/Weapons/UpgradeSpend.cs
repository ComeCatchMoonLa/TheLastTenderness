using UnityEngine;

namespace CatchMoon
{
    public static class UpgradeSpend
    {
        public static int Cap(UpgradePath path)
        {
            if (path == UpgradePath.scale) return ReinforceCap.Scale;
            if (path == UpgradePath.twinkling) return ReinforceCap.Twinkling;
            return ReinforceCap.Normal;
        }

        public static bool Try(string assetName, bool countFilled, int count, int held, int level, int cap, out int nextHeld, out int nextLevel)
        {
            nextHeld = held;
            nextLevel = level;
            if (!countFilled || count <= 0)
            {
                Debug.LogError($"{assetName}: upgradeCount 未填");
                return false;
            }
            if (level >= cap || held < count) return false;
            nextHeld = held - count;
            nextLevel = level + 1;
            return true;
        }
    }
}
