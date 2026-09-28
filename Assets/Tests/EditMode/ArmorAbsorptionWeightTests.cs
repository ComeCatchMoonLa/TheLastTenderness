using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ArmorAbsorptionWeightTests
    {
        [Test]
        public void PartialHeadAbsorption_ReducesPhysicalDamageToSeventy()
        {
            CharacterStatsManager stats = NewStats(100f);
            stats.headArmorPDA = 0.5f;

            stats.TakeDamage(null, 100f, 0f, 0f, 0f, 0f, false);

            Assert.AreEqual(30f, stats.currentHP, 0.001f);
        }

        [Test]
        public void ZeroAbsorption_SubtractsTheFullAmount()
        {
            CharacterStatsManager stats = NewStats(100f);

            stats.TakeDamage(null, 40f, 0f, 0f, 0f, 0f, false);

            Assert.AreEqual(60f, stats.currentHP, 0.001f);
        }

        [Test]
        public void FullAbsorption_DoesNotReduceHealth()
        {
            CharacterStatsManager stats = NewStats(100f);
            stats.headArmorPDA = 1f;
            stats.torsoArmorPDA = 1f;
            stats.hipsArmorPDA = 1f;

            stats.TakeDamage(null, 40f, 0f, 0f, 0f, 0f, false);

            Assert.AreEqual(100f, stats.currentHP, 0.001f);
        }

        static CharacterStatsManager NewStats(float currentHP)
        {
            var root = new GameObject("stats");
            var character = root.AddComponent<CharacterManager>();
            var stats = root.AddComponent<CharacterStatsManager>();
            character.cStats = stats;
            typeof(CharacterStatsManager).GetField("character", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(stats, character);
            stats.currentHP = currentHP;
            return stats;
        }
    }
}
