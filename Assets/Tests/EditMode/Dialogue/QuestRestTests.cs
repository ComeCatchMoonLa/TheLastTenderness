using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class QuestRestTests
    {
        [Test]
        public void AfterTheDelivery_RestKeepsTheFailedLineClosed()
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
            bool giverOpen = true;

            Assert.IsTrue(talk.Give(player, gift));
            talk.otherLineOpen = true;
            ExclusiveFlag.Rest(talk.otherLineFailed, ref talk.otherLineOpen);
            ExclusiveFlag.Rest(false, ref giverOpen);

            Assert.IsFalse(ExclusiveFlag.CanTalk(talk.otherLineOpen, talk.otherLineFailed));
            Assert.IsTrue(ExclusiveFlag.CanTalk(giverOpen, false));
            Object.DestroyImmediate(gift);
            Object.DestroyImmediate(root);
        }
    }
}
