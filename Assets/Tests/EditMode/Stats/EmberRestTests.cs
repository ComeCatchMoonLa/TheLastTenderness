using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class EmberRestTests
    {
        [Test]
        public void EmberStaysUntilDeath_ThenMaxReturnsToTheBase()
        {
            GameObject root = new GameObject("ember");
            PlayerStatsManager stats = root.AddComponent<PlayerStatsManager>();
            stats.maxHP = 100f;
            Assert.IsTrue(stats.ApplyEmber(0.2f, "余烬"));
            Assert.IsTrue(stats.emberLit);
            Assert.AreEqual(120f, stats.maxHP);
            Assert.AreEqual(100f, stats.emberBaseMaxHP);

            Assert.AreEqual(120f, stats.maxHP);

            stats.ExtinguishEmber();
            Assert.IsFalse(stats.emberLit);
            Assert.AreEqual(100f, stats.maxHP);
            Assert.AreEqual(100f, stats.currentHP);

            Object.DestroyImmediate(root);
        }
    }
}
