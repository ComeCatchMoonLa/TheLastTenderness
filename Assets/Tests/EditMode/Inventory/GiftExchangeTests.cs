using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class GiftExchangeTests
    {
        [Test]
        public void Give_RemovesTheWantedItemOnce_AndRejectsTheWrongOne()
        {
            GameObject root = new GameObject("gift");
            Talk talk = root.AddComponent<Talk>();
            PlayerInventoryManager inventory = root.AddComponent<PlayerInventoryManager>();
            PlayerManager player = root.AddComponent<PlayerManager>();
            player.pInventory = inventory;
            inventory.items = new List<Item>();
            inventory.consumables = new List<ConsumableItem>();

            Item wanted = ScriptableObject.CreateInstance<Item>();
            wanted.itemType = ItemType.consumable;
            Item other = ScriptableObject.CreateInstance<Item>();
            other.itemType = ItemType.consumable;
            inventory.items.Add(wanted);
            talk.wantedGift = wanted;

            Assert.IsFalse(talk.Give(player, other));
            Assert.AreEqual(1, inventory.items.Count);

            Assert.IsTrue(talk.Give(player, wanted));
            Assert.AreEqual(0, inventory.items.Count);
            Assert.IsTrue(talk.giftAdvanced);
            Assert.IsFalse(talk.Give(player, wanted));

            Object.DestroyImmediate(wanted);
            Object.DestroyImmediate(other);
            Object.DestroyImmediate(root);
        }
    }
}
