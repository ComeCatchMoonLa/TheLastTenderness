using UnityEngine;

namespace CatchMoon
{
    public static class BleedMeter
    {
        public static float Add(string assetName, float meter, float gain, float resist, float capacity)
        {
            if (capacity <= 0f)
            {
                Debug.LogError($"{assetName}: capacity 未填");
                return meter;
            }
            if (resist < 0f) resist = 0f;
            if (resist > 1f) resist = 1f;
            float next = meter + gain * (1f - resist);
            if (next > capacity) next = capacity;
            if (next < 0f) next = 0f;
            return next;
        }

        public static bool TryProc(float meter, float capacity, out float cleared)
        {
            cleared = meter;
            if (capacity <= 0f || meter < capacity) return false;
            cleared = 0f;
            return true;
        }

        public static float ApplyChunk(float health, float chunk)
        {
            float next = health - chunk;
            if (next < 0f) next = 0f;
            return next;
        }

        public static float Clear()
        {
            return 0f;
        }
    }
}
