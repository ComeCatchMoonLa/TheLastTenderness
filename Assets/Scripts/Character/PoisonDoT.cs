using UnityEngine;

namespace CatchMoon
{
    public static class PoisonDoT
    {
        public static bool TryStart(string assetName, float meter, float capacity, float duration, out float timeLeft, out float clearedMeter)
        {
            timeLeft = 0f;
            clearedMeter = meter;
            if (capacity <= 0f || meter < capacity) return false;
            if (duration <= 0f)
            {
                Debug.LogError($"{assetName}: poisonDuration 未填");
                return false;
            }
            clearedMeter = 0f;
            timeLeft = duration;
            return true;
        }

        public static float Tick(string assetName, float health, float timeLeft, float damagePerSecond, float delta, out float nextTime)
        {
            nextTime = timeLeft;
            if (timeLeft <= 0f || delta <= 0f) return health;
            if (damagePerSecond <= 0f)
            {
                Debug.LogError($"{assetName}: poisonDamagePerSecond 未填");
                return health;
            }
            float step = delta < timeLeft ? delta : timeLeft;
            float next = health - damagePerSecond * step;
            if (next < 0f) next = 0f;
            nextTime = timeLeft - step;
            return next;
        }

        public static float Stop()
        {
            return 0f;
        }
    }
}
