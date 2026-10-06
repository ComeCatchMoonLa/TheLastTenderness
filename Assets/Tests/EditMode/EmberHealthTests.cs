using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class EmberHealthTests
    {
        [Test]
        public void Apply_RaisesMaxAndCurrent_AndEmptyRatioStops()
        {
            GameObject root = new GameObject("stats");
            PlayerStatsManager stats = root.AddComponent<PlayerStatsManager>();
            stats.maxHP = 100f;
            stats.currentHP = 40f;

            LogAssert.Expect(LogType.Error, "Ember: healthRatio 未填");
            Assert.IsFalse(stats.ApplyEmber(0f, "Ember"));
            Assert.AreEqual(100f, stats.maxHP);
            Assert.AreEqual(40f, stats.currentHP);

            Assert.IsTrue(stats.ApplyEmber(0.5f, "Ember"));
            Assert.AreEqual(150f, stats.maxHP);
            Assert.AreEqual(150f, stats.currentHP);

            Object.DestroyImmediate(root);
        }
    }
}
