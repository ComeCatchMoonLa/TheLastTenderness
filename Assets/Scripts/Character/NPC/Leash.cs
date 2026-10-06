using UnityEngine;

namespace CatchMoon
{
    public static class Leash
    {
        public static bool TooFar(float distance, float limit)
        {
            if (limit <= 0f) return false;
            return distance > limit;
        }

        public static bool Arrived(string assetName, float distance, float arrive, ref bool warned)
        {
            if (arrive <= 0f)
            {
                if (!warned)
                {
                    Debug.LogError($"{assetName}: returnArrive 未填");
                    warned = true;
                }
                return false;
            }
            return distance <= arrive;
        }
    }
}
