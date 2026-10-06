using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class MeleeRestTests
    {
        [Test]
        public void MeleeIsKept_BossIsNot_RestoreWritesOriginAndFullHealth()
        {
            Assert.IsTrue(MeleeRest.Keep(false, NPCCombatStyle.melee));
            Assert.IsFalse(MeleeRest.Keep(true, NPCCombatStyle.melee));
            Assert.IsFalse(MeleeRest.Keep(false, NPCCombatStyle.archer));

            GameObject body = new GameObject("melee");
            body.transform.position = new Vector3(1f, 0f, 0f);
            CharacterStatsManager stats = body.AddComponent<CharacterStatsManager>();
            stats.maxHP = 20f;
            stats.currentHP = 0f;
            stats.isDead = true;
            Vector3 origin = new Vector3(5f, 0f, 2f);
            body.SetActive(false);
            Assert.IsFalse(body.activeSelf);

            MeleeRest.Restore(body, origin, stats);
            Assert.IsTrue(body.activeSelf);
            Assert.AreEqual(origin, body.transform.position);
            Assert.IsFalse(stats.isDead);
            Assert.AreEqual(20f, stats.currentHP);

            Object.DestroyImmediate(body);
        }
    }
}
