using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ListedConsumableTests
    {
        [Test]
        public void Moss_ClearsItsAilment_BlessingSkipsFullOrDead()
        {
            CharacterStatsManager stats = ScriptableObjectFree();
            stats.bleedMeter = 4f;
            stats.poisonTimeLeft = 3f;
            stats.toxicTimeLeft = 8f;
            Assert.IsTrue(ConsumableEffect.Apply(ListedConsumable.redMoss, stats, 0f));
            Assert.AreEqual(0f, stats.bleedMeter);
            Assert.AreEqual(3f, stats.poisonTimeLeft);

            Assert.IsTrue(ConsumableEffect.Apply(ListedConsumable.purpleMoss, stats, 0f));
            Assert.AreEqual(0f, stats.poisonTimeLeft);
            Assert.AreEqual(8f, stats.toxicTimeLeft);

            stats.poisonTimeLeft = 2f;
            Assert.IsTrue(ConsumableEffect.Apply(ListedConsumable.bloomingMoss, stats, 0f));
            Assert.AreEqual(0f, stats.poisonTimeLeft);
            Assert.AreEqual(0f, stats.toxicTimeLeft);

            stats.frostMeter = 5f;
            stats.frostTimeLeft = 6f;
            Assert.IsTrue(ConsumableEffect.Apply(ListedConsumable.blueMoss, stats, 0f));
            Assert.AreEqual(0f, stats.frostMeter);
            Assert.AreEqual(0f, stats.frostTimeLeft);

            stats.curseMeter = 4f;
            stats.curseCapacity = 10f;
            Assert.IsTrue(ConsumableEffect.Apply(ListedConsumable.blessing, stats, 0f));
            Assert.AreEqual(0f, stats.curseMeter);

            stats.curseMeter = 10f;
            Assert.IsFalse(ConsumableEffect.Apply(ListedConsumable.blessing, stats, 0f));
            Assert.AreEqual(10f, stats.curseMeter);

            stats.isDead = true;
            stats.curseMeter = 4f;
            Assert.IsFalse(ConsumableEffect.Apply(ListedConsumable.blessing, stats, 0f));
            Assert.IsTrue(stats.isDead);
            Assert.AreEqual(4f, stats.curseMeter);
            Object.DestroyImmediate(stats.gameObject);
        }

        [Test]
        public void Blossom_LastsSixtySeconds_CoinReplacesCoin()
        {
            CharacterStatsManager stats = ScriptableObjectFree();
            Assert.IsTrue(ConsumableEffect.Apply(ListedConsumable.greenBlossom, stats, 10f));
            Assert.AreEqual(70f, stats.blossomUntil);
            Assert.AreEqual(6f, stats.blossomPerSecond);
            Assert.AreEqual(0.3f, ConsumableEffect.BlossomGain(10f, stats.blossomUntil, stats.blossomPerSecond, 0.05f), 0.0001f);
            Assert.AreEqual(0f, ConsumableEffect.BlossomGain(70f, stats.blossomUntil, stats.blossomPerSecond, 0.05f));

            Assert.IsTrue(ConsumableEffect.Apply(ListedConsumable.buddingBlossom, stats, 10f));
            Assert.AreEqual(9f, stats.blossomPerSecond);

            Assert.IsTrue(ConsumableEffect.Apply(ListedConsumable.rustedCoin, stats, 10f));
            Assert.AreEqual(70f, stats.coinUntil);
            Assert.AreEqual(50, stats.coinBonus);
            Assert.AreEqual(50, ConsumableEffect.CoinBonus(10f, stats.coinUntil, stats.coinBonus));

            Assert.IsTrue(ConsumableEffect.Apply(ListedConsumable.rustedGoldCoin, stats, 12f));
            Assert.AreEqual(72f, stats.coinUntil);
            Assert.AreEqual(100, stats.coinBonus);
            Assert.AreEqual(0, ConsumableEffect.CoinBonus(72f, stats.coinUntil, stats.coinBonus));
            Object.DestroyImmediate(stats.gameObject);
        }

        static CharacterStatsManager ScriptableObjectFree()
        {
            GameObject root = new GameObject("人");
            return root.AddComponent<CharacterStatsManager>();
        }
    }
}
