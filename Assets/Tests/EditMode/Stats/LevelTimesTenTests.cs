using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class LevelTimesTenTests
    {
        [Test]
        public void TenBecomesOneHundred_ZeroStaysZero()
        {
            Assert.AreEqual(100f, CharacterStatsManager.LevelTimesTen(10));
            Assert.AreEqual(0f, CharacterStatsManager.LevelTimesTen(0));

            var root = new GameObject("stats");
            var stats = root.AddComponent<CharacterStatsManager>();
            Apply(stats, 10);
            Assert.AreEqual(100f, stats.maxHP);
            Assert.AreEqual(100f, stats.maxStamina);
            Assert.AreEqual(100f, stats.maxMP);

            Apply(stats, 0);
            Assert.AreEqual(0f, stats.maxHP);
            Assert.AreEqual(0f, stats.maxStamina);
            Assert.AreEqual(0f, stats.maxMP);
            Object.DestroyImmediate(root);
        }

        static void Apply(CharacterStatsManager stats, int level)
        {
            stats.healthLevel = level;
            stats.staminaLevel = level;
            stats.focusLevel = level;
            Invoke(stats, "SetMaxHealthFromHealthLevel");
            Invoke(stats, "SetMaxStaminaFromStaminaLevel");
            Invoke(stats, "SetMaxFocusPointsFromFocusLevel");
        }

        static void Invoke(CharacterStatsManager stats, string methodName)
        {
            typeof(CharacterStatsManager).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(stats, null);
        }
    }
}
