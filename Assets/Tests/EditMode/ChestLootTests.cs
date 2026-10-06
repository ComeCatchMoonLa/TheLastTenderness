using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ChestLootTests
    {
        [Test]
        public void Give_AddsTheItemOnce_AndRestKeepsItOpen()
        {
            GameObject root = new GameObject("bag");
            PlayerInventoryManager inventory = root.AddComponent<PlayerInventoryManager>();
            inventory.items = new List<Item>();
            inventory.weapons = new List<WeaponItem>();

            WeaponItem item = ScriptableObject.CreateInstance<WeaponItem>();
            item.itemType = ItemType.equipment;
            item.equipmentType = EquipmentType.weapon;

            Assert.IsFalse(ChestLoot.Give(false, null, inventory));
            Assert.AreEqual(0, inventory.items.Count);

            Assert.IsTrue(ChestLoot.Give(false, item, inventory));
            Assert.AreEqual(1, inventory.items.Count);
            Assert.IsFalse(ChestLoot.Give(true, item, inventory));
            Assert.AreEqual(1, inventory.items.Count);
            Assert.IsTrue(ChestLoot.AfterRest(true));
            Assert.IsFalse(ChestLoot.AfterRest(false));

            Object.DestroyImmediate(item);
            Object.DestroyImmediate(root);
        }
    }
}
