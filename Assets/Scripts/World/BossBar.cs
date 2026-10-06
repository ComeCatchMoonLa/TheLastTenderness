using UnityEngine;

namespace CatchMoon
{
    public static class BossBar
    {
        public static bool TryShow(string bossName, float current, float max, string assetName, out string shown, out float length)
        {
            shown = null;
            length = 0f;
            if (string.IsNullOrEmpty(bossName))
            {
                Debug.LogError($"{assetName}: cName 未填");
                return false;
            }
            if (max <= 0f)
            {
                Debug.LogError($"{assetName}: maxHP 未填");
                return false;
            }
            shown = bossName;
            length = current / max;
            if (length < 0f) length = 0f;
            if (length > 1f) length = 1f;
            return true;
        }
    }
}
