using UnityEngine;

namespace CatchMoon
{
    public static class ToxicDoT
    {
        public static bool TryStart(
            string assetName,
            float meter,
            float capacity,
            float duration,
            float damagePerSecond,
            float staminaRegen,
            out float timeLeft,
            out float clearedMeter)
        {
            timeLeft = 0f;
            clearedMeter = meter;
            if (capacity <= 0f || meter < capacity) return false;
            if (duration <= 0f)
            {
                Debug.LogError($"{assetName}: toxicDuration 未填");
                return false;
            }
            if (damagePerSecond <= 0f)
            {
                Debug.LogError($"{assetName}: toxicDamagePerSecond 未填");
                return false;
            }
            if (staminaRegen <= 0f)
            {
                Debug.LogError($"{assetName}: toxicStaminaRegen 未填");
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
                Debug.LogError($"{assetName}: toxicDamagePerSecond 未填");
                return health;
            }
            float step = delta < timeLeft ? delta : timeLeft;
            float next = health - damagePerSecond * step;
            if (next < 0f) next = 0f;
            nextTime = timeLeft - step;
            return next;
        }

        public static float Regen(string assetName, float amount, bool active, float multiplier)
        {
            if (!active) return amount;
            if (multiplier <= 0f)
            {
                Debug.LogError($"{assetName}: toxicStaminaRegen 未填");
                return amount;
            }
            return amount * multiplier;
        }
    }
}
