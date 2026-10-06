using UnityEngine;

namespace CatchMoon
{
    public static class CycleGate
    {
        public static bool TryRecord(ref string recorded, string ending, string assetName)
        {
            if (!string.IsNullOrEmpty(recorded)) return false;
            if (string.IsNullOrEmpty(ending))
            {
                Debug.LogError($"{assetName}: ending 未填");
                return false;
            }
            recorded = ending;
            return true;
        }

        public static bool Allow(string recorded)
        {
            return !string.IsNullOrEmpty(recorded);
        }
    }
}
