using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ExclusiveFlagTests
    {
        [Test]
        public void Delivery_RaisesTheFlag_AndClosesTheOtherLine()
        {
            GameObject root = new GameObject("交付");
            Talk talk = root.AddComponent<Talk>();
            PlayerInventoryManager inventory = root.AddComponent<PlayerInventoryManager>();
            PlayerManager player = root.AddComponent<PlayerManager>();
            player.pInventory = inventory;
            inventory.items = new List<Item>();
            inventory.consumables = new List<ConsumableItem>();
            Item gift = ScriptableObject.CreateInstance<Item>();
            gift.itemType = ItemType.consumable;
            inventory.items.Add(gift);
            talk.wantedGift = gift;
            talk.closesOtherLine = true;

            Item wrong = ScriptableObject.CreateInstance<Item>();
            Assert.IsFalse(talk.Give(player, wrong));
            Assert.IsFalse(talk.exclusiveRaised);
            Assert.IsTrue(ExclusiveFlag.CanTalk(talk.otherLineOpen, talk.otherLineFailed));

            Assert.IsTrue(talk.Give(player, gift));
            Assert.IsTrue(talk.exclusiveRaised);
            Assert.IsFalse(ExclusiveFlag.CanTalk(talk.otherLineOpen, talk.otherLineFailed));
            Assert.IsTrue(talk.giftAdvanced);
            Assert.IsTrue(ExclusiveFlag.CanTalk(true, false));

            Object.DestroyImmediate(gift);
            Object.DestroyImmediate(wrong);
            Object.DestroyImmediate(root);
        }
    }
}
