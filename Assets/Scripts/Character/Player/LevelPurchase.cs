using UnityEngine;

namespace CatchMoon
{
    public static class LevelPurchase
    {
        public static bool TryConfirm(string assetName, int souls, int cost, int level, out int newSouls, out int newLevel)
        {
            newSouls = souls;
            newLevel = level;
            if (cost <= 0)
            {
                Debug.LogError($"{assetName}: cost 未填");
                return false;
            }
            if (souls < cost) return false;
            newSouls = souls - cost;
            newLevel = level + 1;
            return true;
        }
    }
}
