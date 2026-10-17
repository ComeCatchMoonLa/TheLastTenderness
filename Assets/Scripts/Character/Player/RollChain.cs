using UnityEngine;

namespace CatchMoon
{
    public static class RollChain
    {
        public static float Cost(string assetName, bool stepFilled, float step, bool pauseFilled, float pause, float sinceLast, int streak, float baseCost, out int nextStreak)
        {
            nextStreak = 0;
            if (!stepFilled || step <= 0f)
            {
                Debug.LogError($"{assetName}: rollChainStep 未填");
                return baseCost;
            }
            if (!pauseFilled || pause <= 0f)
            {
                Debug.LogError($"{assetName}: rollChainPause 未填");
                return baseCost;
            }

            int used = streak > 0 && sinceLast <= pause ? streak : 0;
            nextStreak = used + 1;
            return baseCost + step * used;
        }
    }
}
