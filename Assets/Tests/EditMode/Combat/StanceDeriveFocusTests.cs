using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class StanceDeriveFocusTests
    {
        [Test]
        public void FullCost_LaunchesAndSpends()
        {
            Assert.IsTrue(StanceDeriveFocus.TryLaunch(40f, 20, out float next));
            Assert.AreEqual(20f, next, 0.001f);
            Assert.IsTrue(StanceDeriveFocus.TryLaunch(17f, 17, out next));
            Assert.AreEqual(0f, next, 0.001f);
        }

        [Test]
        public void BelowFull_DoesNotLaunch()
        {
            Assert.IsFalse(StanceDeriveFocus.TryLaunch(9f, 20, out float next));
            Assert.AreEqual(9f, next, 0.001f);
            Assert.IsFalse(StanceDeriveFocus.TryLaunch(15f, 20, out next));
            Assert.AreEqual(15f, next, 0.001f);
            Assert.IsFalse(StanceDeriveFocus.TryLaunch(8f, 17, out next));
            Assert.AreEqual(8f, next, 0.001f);
        }

        [Test]
        public void Unfilled_StaysInStanceAndLogs()
        {
            var root = new GameObject("derive");
            CharacterManager character = root.AddComponent<CharacterManager>();
            CharacterStatsManager stats = root.AddComponent<CharacterStatsManager>();
            CharacterInventoryManager inventory = root.AddComponent<CharacterInventoryManager>();
            character.cStats = stats;
            character.cInventory = inventory;
            stats.currentMP = 40f;
            character.stanceHolding = true;
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.name = "Stance Sword";
            inventory.currentItemBeingUsed = weapon;

            LogAssert.Expect(LogType.Error, "Stance Sword: lightDeriveFocus 未填");
            Assert.IsFalse(StanceDeriveFocus.Commit(character, true));
            Assert.IsTrue(character.stanceHolding);
            Assert.AreEqual(40f, stats.currentMP, 0.001f);

            Object.DestroyImmediate(weapon);
            Object.DestroyImmediate(root);
        }
    }
}
