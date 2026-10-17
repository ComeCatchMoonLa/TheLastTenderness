using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class AmmoStatusTests
    {
        [Test]
        public void PoisonRaisesOnlyPoison_BleedRaisesOnlyBleed_PlainRaisesNone()
        {
            CharacterStatsManager stats = new GameObject("目标").AddComponent<CharacterStatsManager>();
            stats.poisonCapacity = 100f;
            stats.bleedCapacity = 100f;

            Assert.IsTrue(AmmoStatus.Apply(stats, AmmoStatusSlot.Poison, 10f));
            Assert.AreEqual(10f, stats.poisonMeter);
            Assert.AreEqual(0f, stats.bleedMeter);

            Assert.IsTrue(AmmoStatus.Apply(stats, AmmoStatusSlot.Bleed, 8f));
            Assert.AreEqual(8f, stats.bleedMeter);
            Assert.AreEqual(10f, stats.poisonMeter);

            Assert.IsFalse(AmmoStatus.Apply(stats, AmmoStatusSlot.None, 10f));
            Assert.AreEqual(10f, stats.poisonMeter);
            Assert.AreEqual(8f, stats.bleedMeter);
            Object.DestroyImmediate(stats.gameObject);
        }
    }
}
