using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class FlaskSplitTests
    {
        [Test]
        public void Allocate_ChangesShares_AndRestWritesTheNewCaps()
        {
            GameObject root = new GameObject("split");
            PlayerInventoryManager inventory = root.AddComponent<PlayerInventoryManager>();
            inventory.flaskTotal = 4;
            inventory.estusShare = 3;
            inventory.ashShare = 1;
            inventory.estusLeft = 1;
            inventory.ashLeft = 0;

            Assert.IsTrue(inventory.TryAllocateFlasks(2, 2));
            Assert.AreEqual(2, inventory.estusShare);
            Assert.AreEqual(2, inventory.ashShare);
            Assert.AreEqual(1, inventory.estusLeft);
            Assert.AreEqual(0, inventory.ashLeft);

            inventory.RefillConsumablesToMax();
            Assert.AreEqual(2, inventory.estusLeft);
            Assert.AreEqual(2, inventory.ashLeft);
            Object.DestroyImmediate(root);
        }

        [Test]
        public void Allocate_RejectsASplitThatChangesTheTotal()
        {
            GameObject root = new GameObject("split-reject");
            PlayerInventoryManager inventory = root.AddComponent<PlayerInventoryManager>();
            inventory.flaskTotal = 4;

            Assert.IsFalse(inventory.TryAllocateFlasks(1, 2));
            Assert.AreEqual(3, inventory.estusShare);
            Assert.AreEqual(0, inventory.ashShare);
            Assert.AreEqual(3, inventory.estusLeft);
            Object.DestroyImmediate(root);
        }

        [Test]
        public void Talk_ForwardsOnlyWhenItOffersTheSplit()
        {
            GameObject root = new GameObject("talk-split");
            Talk talk = root.AddComponent<Talk>();
            PlayerInventoryManager inventory = root.AddComponent<PlayerInventoryManager>();
            PlayerManager player = root.AddComponent<PlayerManager>();
            player.pInventory = inventory;
            inventory.flaskTotal = 4;

            Assert.IsFalse(talk.AllocateFlasks(player, 4, 0));
            talk.offersFlaskSplit = true;
            Assert.IsTrue(talk.AllocateFlasks(player, 4, 0));
            Assert.AreEqual(4, inventory.estusShare);
            Assert.AreEqual(0, inventory.ashShare);
            Assert.AreEqual(3, inventory.estusLeft);
            Object.DestroyImmediate(root);
        }
    }
}
