using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class AilmentRestTests
    {
        [Test]
        public void Rest_ClearsFiveAilments_DeadStaysDead()
        {
            GameObject body = new GameObject("人");
            CharacterStatsManager stats = body.AddComponent<CharacterStatsManager>();
            stats.poisonMeter = 3f;
            stats.poisonTimeLeft = 9f;
            stats.poisonCapacity = 100f;
            stats.toxicMeter = 4f;
            stats.toxicTimeLeft = 8f;
            stats.bleedMeter = 5f;
            stats.frostMeter = 2f;
            stats.frostTimeLeft = 7f;
            stats.curseMeter = 6f;
            stats.currentHP = 0f;
            stats.isDead = true;

            AilmentRest.Clear(stats);

            Assert.AreEqual(0f, stats.poisonMeter);
            Assert.AreEqual(0f, stats.poisonTimeLeft);
            Assert.AreEqual(0f, stats.toxicMeter);
            Assert.AreEqual(0f, stats.toxicTimeLeft);
            Assert.AreEqual(0f, stats.bleedMeter);
            Assert.AreEqual(0f, stats.frostMeter);
            Assert.AreEqual(0f, stats.frostTimeLeft);
            Assert.AreEqual(0f, stats.curseMeter);
            Assert.AreEqual(100f, stats.poisonCapacity);
            Assert.AreEqual(0f, stats.currentHP);
            Assert.IsTrue(stats.isDead);
            Object.DestroyImmediate(body);
        }
    }
}
