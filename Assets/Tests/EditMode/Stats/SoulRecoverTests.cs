using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SoulRecoverTests
    {
        [Test]
        public void Recover_AddsTheRemnant_ReturnFromDeath_ClearsDeathAtTheFire()
        {
            GameObject root = new GameObject("recover");
            PlayerStatsManager stats = root.AddComponent<PlayerStatsManager>();
            stats.soulCount = 0;
            stats.hasSoulRemnant = true;
            stats.soulRemnant = 40;
            Assert.IsTrue(stats.RecoverRemnant());
            Assert.AreEqual(40, stats.soulCount);
            Assert.IsFalse(stats.hasSoulRemnant);
            Assert.IsFalse(stats.RecoverRemnant());
            Assert.AreEqual(40, stats.soulCount);

            GameObject fire = new GameObject("fire");
            fire.transform.position = new Vector3(3f, 0f, 4f);
            root.transform.position = Vector3.zero;
            stats.isDead = true;
            stats.ReturnFromDeath(fire.transform, root.transform);
            Assert.IsFalse(stats.isDead);
            Assert.AreEqual(fire.transform.position, root.transform.position);

            Object.DestroyImmediate(fire);
            Object.DestroyImmediate(root);
        }
    }
}
