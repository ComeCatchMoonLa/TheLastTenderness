using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class DurabilityTierTests
    {
        [Test]
        public void Factor_FullAtThirty_ReducedBelow_HalfAtZero_UnfilledStaysOne()
        {
            Assert.AreEqual(1f, DurabilityTier.Factor("剑", true, 10f, 10f));
            Assert.AreEqual(1f, DurabilityTier.Factor("剑", true, 3f, 10f));
            Assert.AreEqual(0.7f, DurabilityTier.Factor("剑", true, 2f, 10f));
            Assert.AreEqual(0.5f, DurabilityTier.Factor("剑", true, 0f, 10f));
            Assert.AreEqual(0.28f, DurabilityTier.Mitigation(0.4f, 0.7f), 0.0001f);

            LogAssert.Expect(LogType.Error, "剑: durabilityMax 未填");
            Assert.AreEqual(1f, DurabilityTier.Factor("剑", false, 0f, 0f));
        }

        [Test]
        public void TakeDamage_UsesThePieceFactor()
        {
            CharacterStatsManager stats = NewStats(100f);
            stats.headArmorPDA = 0.5f;
            stats.headDurabilityFactor = 0.7f;

            stats.TakeDamage(null, new DamageSegments { Physical = 100f }, false);

            Assert.AreEqual(21f, stats.currentHP, 0.001f);
            Object.DestroyImmediate(stats.gameObject);
        }

        [Test]
        public void WeaponDamage_BrokenIsHalf_UnfilledIsFull()
        {
            WeaponItem broken = ScriptableObject.CreateInstance<WeaponItem>();
            broken.name = "破剑";
            broken.durabilityFilled = true;
            broken.durabilityMax = 10f;
            broken.durability = 0f;
            DamageSegments half = DurabilityTier.WeaponDamage(new DamageSegments { Physical = 10f, Kind = PhysicalKind.Slash }, broken);
            Assert.AreEqual(5f, half.Physical, 0.001f);
            Assert.AreEqual(PhysicalKind.Slash, half.Kind);

            WeaponItem empty = ScriptableObject.CreateInstance<WeaponItem>();
            empty.name = "空剑";
            LogAssert.Expect(LogType.Error, "空剑: durabilityMax 未填");
            DamageSegments full = DurabilityTier.WeaponDamage(new DamageSegments { Physical = 10f }, empty);
            Assert.AreEqual(10f, full.Physical, 0.001f);
        }

        [Test]
        public void Swing_DropsOnePointAfterFilledCount_UnfilledDoesNotDrop()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.name = "剑";
            weapon.durabilityFilled = true;
            weapon.durabilityMax = 10f;
            weapon.durability = 10f;
            weapon.swingsPerPointFilled = true;
            weapon.swingsPerPoint = 5;
            for (int i = 0; i < 4; i++)
                DurabilityTier.Swing(weapon);
            Assert.AreEqual(10f, weapon.durability);
            DurabilityTier.Swing(weapon);
            Assert.AreEqual(9f, weapon.durability);

            weapon.swingsPerPointFilled = false;
            LogAssert.Expect(LogType.Error, "剑: swingsPerPoint 未填");
            DurabilityTier.Swing(weapon);
            Assert.AreEqual(9f, weapon.durability);
        }

        [Test]
        public void ArmorHit_DropsFilledLoss_UnfilledDoesNot()
        {
            ArmorItem armor = ScriptableObject.CreateInstance<ArmorItem>();
            armor.name = "甲";
            armor.durabilityFilled = true;
            armor.durabilityMax = 10f;
            armor.durability = 5f;
            armor.lossPerHitFilled = true;
            armor.lossPerHit = 2f;
            DurabilityTier.ArmorHit(armor);
            Assert.AreEqual(3f, armor.durability);

            armor.lossPerHitFilled = false;
            LogAssert.Expect(LogType.Error, "甲: lossPerHit 未填");
            DurabilityTier.ArmorHit(armor);
            Assert.AreEqual(3f, armor.durability);
        }

        [Test]
        public void Rest_RepairsAboveZero_LeavesBroken_UnfilledStays()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.name = "剑";
            weapon.durabilityFilled = true;
            weapon.durabilityMax = 10f;
            weapon.durability = 4f;
            DurabilityTier.RepairWeapon(weapon);
            Assert.AreEqual(10f, weapon.durability);

            weapon.durability = 0f;
            DurabilityTier.RepairWeapon(weapon);
            Assert.AreEqual(0f, weapon.durability);

            weapon.durabilityFilled = false;
            weapon.durability = 4f;
            LogAssert.Expect(LogType.Error, "剑: durabilityMax 未填");
            DurabilityTier.RepairWeapon(weapon);
            Assert.AreEqual(4f, weapon.durability);
        }

        static CharacterStatsManager NewStats(float currentHP)
        {
            GameObject root = new GameObject("stats");
            CharacterManager character = root.AddComponent<CharacterManager>();
            CharacterStatsManager stats = root.AddComponent<CharacterStatsManager>();
            character.cStats = stats;
            typeof(CharacterStatsManager).GetField("character", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(stats, character);
            stats.currentHP = currentHP;
            return stats;
        }
    }
}
