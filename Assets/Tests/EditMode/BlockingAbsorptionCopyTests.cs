using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class BlockingAbsorptionCopyTests
    {
        [Test]
        public void CopyMatchesWeapon_ClearWritesZero()
        {
            var root = new GameObject("stats");
            var stats = root.AddComponent<CharacterStatsManager>();
            var weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.physicalDA = 0.1f;
            weapon.fireDA = 0.2f;
            weapon.magicDA = 0.3f;
            weapon.lightningDA = 0.4f;
            weapon.darkDA = 0.5f;
            weapon.blockingStabilityRating = 0.6f;

            CharacterCombatManager.CopyBlockingAbsorption(stats, weapon);

            Assert.AreEqual(0.1f, stats.blockingPDA);
            Assert.AreEqual(0.2f, stats.blockingFDA);
            Assert.AreEqual(0.3f, stats.blockingMDA);
            Assert.AreEqual(0.4f, stats.blockingLDA);
            Assert.AreEqual(0.5f, stats.blockingDDA);
            Assert.AreEqual(0.6f, stats.blockingStabilityRating);

            CharacterCombatManager.ClearBlockingAbsorption(stats);

            Assert.AreEqual(0f, stats.blockingPDA);
            Assert.AreEqual(0f, stats.blockingFDA);
            Assert.AreEqual(0f, stats.blockingMDA);
            Assert.AreEqual(0f, stats.blockingLDA);
            Assert.AreEqual(0f, stats.blockingDDA);
            Assert.AreEqual(0f, stats.blockingStabilityRating);

            Object.DestroyImmediate(weapon);
            Object.DestroyImmediate(root);
        }
    }
}
