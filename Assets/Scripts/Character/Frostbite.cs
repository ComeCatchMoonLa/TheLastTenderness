using UnityEngine;

namespace CatchMoon
{
    public static class Frostbite
    {
        public static bool TryProc(
            string assetName,
            float meter,
            float capacity,
            float duration,
            float chunk,
            float damageTaken,
            float staminaRegen,
            float health,
            out float nextHealth,
            out float timeLeft,
            out float clearedMeter)
        {
            nextHealth = health;
            timeLeft = 0f;
            clearedMeter = meter;
            if (capacity <= 0f || meter < capacity) return false;
            if (duration <= 0f)
            {
                Debug.LogError($"{assetName}: frostDuration 未填");
                return false;
            }
            if (chunk <= 0f)
            {
                Debug.LogError($"{assetName}: frostChunk 未填");
                return false;
            }
            if (damageTaken <= 0f)
            {
                Debug.LogError($"{assetName}: frostDamageTaken 未填");
                return false;
            }
            if (staminaRegen <= 0f)
            {
                Debug.LogError($"{assetName}: frostStaminaRegen 未填");
                return false;
            }
            clearedMeter = 0f;
            timeLeft = duration;
            nextHealth = BleedMeter.ApplyChunk(health, chunk);
            return true;
        }

        public static float Taken(string assetName, float damage, bool active, float multiplier)
        {
            if (!active) return damage;
            if (multiplier <= 0f)
            {
                Debug.LogError($"{assetName}: frostDamageTaken 未填");
                return damage;
            }
            return damage * multiplier;
        }

        public static float Regen(string assetName, float amount, bool active, float multiplier)
        {
            if (!active) return amount;
            if (multiplier <= 0f)
            {
                Debug.LogError($"{assetName}: frostStaminaRegen 未填");
                return amount;
            }
            return amount * multiplier;
        }

        public static bool ClearedByFire(float fireDamage, float timeLeft)
        {
            return timeLeft > 0f && fireDamage > 0f;
        }

        public static bool ClearWithBlueMoss(float meter, float timeLeft, out float nextMeter, out float nextTime)
        {
            nextMeter = meter;
            nextTime = timeLeft;
            if (meter <= 0f && timeLeft <= 0f) return false;
            nextMeter = 0f;
            nextTime = 0f;
            return true;
        }

        public static float Tick(float timeLeft, float delta)
        {
            if (timeLeft <= 0f || delta <= 0f) return timeLeft;
            float next = timeLeft - delta;
            if (next < 0f) next = 0f;
            return next;
        }
    }
}
