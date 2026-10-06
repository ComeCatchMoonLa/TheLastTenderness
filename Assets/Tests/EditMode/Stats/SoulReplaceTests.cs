using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SoulReplaceTests
    {
        [Test]
        public void SecondDeath_ReplacesTheOldRemnant_ZeroClearsIt()
        {
            GameObject root = new GameObject("replace");
            PlayerStatsManager stats = root.AddComponent<PlayerStatsManager>();
            stats.soulCount = 40;
            stats.ForfeitSouls();
            stats.soulCount = 10;
            stats.ForfeitSouls();
            Assert.IsTrue(stats.hasSoulRemnant);
            Assert.AreEqual(10, stats.soulRemnant);
            Assert.AreEqual(0, stats.soulCount);

            stats.soulCount = 0;
            stats.ForfeitSouls();
            Assert.IsFalse(stats.hasSoulRemnant);
            Assert.AreEqual(0, stats.soulRemnant);

            Object.DestroyImmediate(root);
        }
    }
}
