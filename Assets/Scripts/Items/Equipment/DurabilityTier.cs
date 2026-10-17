using UnityEngine;

namespace CatchMoon
{
    public static class DurabilityTier
    {
        public static float Factor(string assetName, bool filled, float current, float max)
        {
            if (!filled || max <= 0f)
            {
                Debug.LogError($"{assetName}: durabilityMax 未填");
                return 1f;
            }
            if (current <= 0f) return 0.5f;
            if (current * 10f < max * 3f) return 0.7f;
            return 1f;
        }

        public static float Mitigation(float absorption, float factor)
        {
            return absorption * factor;
        }

        public static DamageSegments Scale(DamageSegments damage, float factor)
        {
            damage.Physical *= factor;
            damage.Fire *= factor;
            damage.Magic *= factor;
            damage.Lightning *= factor;
            damage.Dark *= factor;
            return damage;
        }

        public static DamageSegments WeaponDamage(DamageSegments segments, WeaponItem weapon)
        {
            if (weapon == null) return segments;
            return Scale(segments, Factor(weapon.name, weapon.durabilityFilled, weapon.durability, weapon.durabilityMax));
        }

        public static float Piece(ArmorItem armor)
        {
            if (armor == null) return 1f;
            return Factor(armor.name, armor.durabilityFilled, armor.durability, armor.durabilityMax);
        }

        public static float AfterRest(string assetName, bool filled, float current, float max)
        {
            if (!filled || max <= 0f)
            {
                Debug.LogError($"{assetName}: durabilityMax 未填");
                return current;
            }
            if (current <= 0f) return 0f;
            return max;
        }

        public static void RepairWeapon(WeaponItem weapon)
        {
            if (weapon == null) return;
            weapon.durability = AfterRest(weapon.name, weapon.durabilityFilled, weapon.durability, weapon.durabilityMax);
        }

        public static void RepairArmor(ArmorItem armor)
        {
            if (armor == null) return;
            armor.durability = AfterRest(armor.name, armor.durabilityFilled, armor.durability, armor.durabilityMax);
        }

        public static void Swing(WeaponItem weapon)
        {
            if (weapon == null) return;
            if (!weapon.durabilityFilled || weapon.durabilityMax <= 0f)
            {
                Debug.LogError($"{weapon.name}: durabilityMax 未填");
                return;
            }
            if (weapon.durability <= 0f) return;
            if (!weapon.swingsPerPointFilled || weapon.swingsPerPoint <= 0)
            {
                Debug.LogError($"{weapon.name}: swingsPerPoint 未填");
                return;
            }
            weapon.swingCount++;
            if (weapon.swingCount < weapon.swingsPerPoint) return;
            weapon.swingCount = 0;
            weapon.durability -= 1f;
            if (weapon.durability < 0f) weapon.durability = 0f;
        }

        public static void ArmorHit(ArmorItem armor)
        {
            if (armor == null) return;
            if (!armor.durabilityFilled || armor.durabilityMax <= 0f) return;
            if (!armor.lossPerHitFilled || armor.lossPerHit <= 0f)
            {
                Debug.LogError($"{armor.name}: lossPerHit 未填");
                return;
            }
            if (armor.durability <= 0f) return;
            armor.durability -= armor.lossPerHit;
            if (armor.durability < 0f) armor.durability = 0f;
        }
    }
}
