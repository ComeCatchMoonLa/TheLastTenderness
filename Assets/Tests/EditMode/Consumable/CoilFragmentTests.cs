using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class CoilFragmentTests
    {
        [Test]
        public void TryUse_ReturnsWithoutSpending_PickupDoesNotStack_BossBarRefuses()
        {
            HomewardState used = new HomewardState
            {
                bones = 1,
                lastFire = "甲",
                place = "乙",
                souls = 40,
                emberLit = true,
                hp = 10f,
                maxHp = 100f,
                mp = 5f,
                maxMp = 80f,
                estus = 0,
                estusShare = 3,
                ash = 1,
                ashShare = 2
            };
            Assert.IsTrue(CoilFragment.TryUse(used));
            Assert.AreEqual("甲", used.place);
            Assert.AreEqual(1, used.bones);
            Assert.AreEqual(100f, used.hp);
            Assert.AreEqual(80f, used.mp);
            Assert.AreEqual(3, used.estus);
            Assert.AreEqual(2, used.ash);
            Assert.AreEqual(40, used.souls);
            Assert.IsTrue(used.emberLit);
            Assert.IsTrue(used.resetEnemies);

            Assert.AreEqual(1, CoilFragment.Pickup(0));
            Assert.AreEqual(1, CoilFragment.Pickup(1));

            var root = new GameObject("inventory");
            var inventory = root.AddComponent<PlayerInventoryManager>();
            inventory.items = new List<Item>();
            inventory.consumables = new List<ConsumableItem>();
            CoilFragmentItem fragment = ScriptableObject.CreateInstance<CoilFragmentItem>();
            inventory.AddItem(fragment);
            inventory.AddItem(fragment);
            Assert.AreEqual(1, inventory.items.Count);
            Assert.AreEqual(1, inventory.consumables.Count);

            HomewardState refused = new HomewardState
            {
                bossBarVisible = true,
                bones = 1,
                lastFire = "甲",
                place = "乙",
                souls = 40,
                emberLit = true,
                hp = 10f
            };
            Assert.IsFalse(CoilFragment.TryUse(refused));
            Assert.AreEqual("乙", refused.place);
            Assert.AreEqual(1, refused.bones);
            Assert.AreEqual(40, refused.souls);
            Assert.IsTrue(refused.emberLit);
        }
    }
}
