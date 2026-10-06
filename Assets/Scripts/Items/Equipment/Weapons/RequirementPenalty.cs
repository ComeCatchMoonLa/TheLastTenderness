using UnityEngine;

namespace CatchMoon
{
    public static class RequirementPenalty
    {
        public static bool TryScale(
            string assetName,
            WeaponItem weapon,
            int strengthLevel,
            int dexterityLevel,
            int intelligenceLevel,
            int faithLevel,
            int luckLevel,
            float speedFactor,
            float damageFactor,
            int damage,
            out float speed,
            out float scaledDamage)
        {
            speed = 1f;
            scaledDamage = damage;
            bool strengthShort = WeaponNeed.TryStrength(weapon, out int strengthNeed) && strengthLevel < strengthNeed;
            bool dexterityShort = WeaponNeed.TryDexterity(weapon, out int dexterityNeed) && dexterityLevel < dexterityNeed;
            bool intelligenceShort = WeaponNeed.TryIntelligence(weapon, out int intelligenceNeed) && intelligenceLevel < intelligenceNeed;
            bool faithShort = WeaponNeed.TryFaith(weapon, out int faithNeed) && faithLevel < faithNeed;
            bool luckShort = WeaponNeed.TryLuck(weapon, out int luckNeed) && luckLevel < luckNeed;
            if (!strengthShort && !dexterityShort && !intelligenceShort && !faithShort && !luckShort) return true;
            if (speedFactor <= 0f)
            {
                Debug.LogError($"{assetName}: speedFactor 未填");
                return false;
            }
            if (damageFactor <= 0f)
            {
                Debug.LogError($"{assetName}: damageFactor 未填");
                return false;
            }
            speed = speedFactor;
            scaledDamage = damage * damageFactor;
            return true;
        }
    }
}
