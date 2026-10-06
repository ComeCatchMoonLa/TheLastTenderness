using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class DamageSegmentsTests
    {
        [Test]
        public void FiveSegments_SumAfterZeroAbsorption()
        {
            CharacterStatsManager stats = NewStats(100f);

            bool applied = stats.TakeDamage(null, new DamageSegments
            {
                Physical = 10f,
                Fire = 10f,
                Magic = 10f,
                Lightning = 10f,
                Dark = 10f
            }, false);

            Assert.IsTrue(applied);
            Assert.AreEqual(50f, stats.currentHP, 0.001f);
        }

        [Test]
        public void DeadOrInvulnerable_DoesNotReduceHealth()
        {
            CharacterStatsManager dead = NewStats(100f);
            dead.isDead = true;
            Assert.IsFalse(dead.TakeDamage(null, new DamageSegments { Physical = 10f }, false));
            Assert.AreEqual(100f, dead.currentHP, 0.001f);

            CharacterStatsManager immune = NewStats(100f);
            immune.isInvulnerable = true;
            Assert.IsFalse(immune.TakeDamage(null, new DamageSegments { Physical = 10f }, false));
            Assert.AreEqual(100f, immune.currentHP, 0.001f);
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
