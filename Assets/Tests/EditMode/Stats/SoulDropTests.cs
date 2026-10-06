using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SoulDropTests
    {
        [Test]
        public void PositiveSouls_LeaveARemnant_ZeroDoesNot()
        {
            GameObject root = new GameObject("souls");
            PlayerStatsManager stats = root.AddComponent<PlayerStatsManager>();
            stats.soulCount = 40;
            stats.ForfeitSouls();
            Assert.AreEqual(0, stats.soulCount);
            Assert.IsTrue(stats.hasSoulRemnant);
            Assert.AreEqual(40, stats.soulRemnant);

            stats.soulCount = 0;
            stats.hasSoulRemnant = false;
            stats.soulRemnant = 0;
            stats.ForfeitSouls();
            Assert.AreEqual(0, stats.soulCount);
            Assert.IsFalse(stats.hasSoulRemnant);
            Assert.AreEqual(0, stats.soulRemnant);

            Object.DestroyImmediate(root);
        }
    }
}
