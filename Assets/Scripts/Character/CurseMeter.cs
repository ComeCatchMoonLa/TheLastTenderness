using UnityEngine;

namespace CatchMoon
{
    public static class CurseMeter
    {
        public static float Decay(string assetName, float meter, float speed, float delta)
        {
            if (meter <= 0f) return 0f;
            if (speed <= 0f)
            {
                Debug.LogError($"{assetName}: curseDecay 未填");
                return meter;
            }
            if (delta <= 0f) return meter;
            float next = meter - speed * delta;
            if (next < 0f) next = 0f;
            return next;
        }

        public static bool TryKill(float meter, float capacity, out float cleared)
        {
            return BleedMeter.TryProc(meter, capacity, out cleared);
        }

        public static bool TryBless(float meter, float capacity, out float cleared)
        {
            cleared = meter;
            if (capacity > 0f && meter >= capacity) return false;
            cleared = BleedMeter.Clear();
            return true;
        }
    }
}
