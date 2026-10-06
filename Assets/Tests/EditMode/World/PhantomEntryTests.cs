using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class PhantomEntryTests
    {
        [Test]
        public void WhiteAndYellow_HalveCaps_ReturnKeepsDeparture_RedStays()
        {
            var root = new GameObject("inventory");
            var inventory = root.AddComponent<PlayerInventoryManager>();
            inventory.estusShare = 5;
            inventory.estusLeft = 4;
            inventory.ashShare = 3;
            inventory.ashLeft = 2;

            Assert.IsTrue(inventory.EnterPhantom(PhantomSign.White, 120f, 100f, out float maxNow));
            Assert.AreEqual(2, inventory.estusLeft);
            Assert.AreEqual(1, inventory.ashLeft);
            Assert.AreEqual(100f, maxNow, 0.001f);

            inventory.estusLeft = 0;
            inventory.ashLeft = 0;
            Assert.IsTrue(inventory.ReturnPhantom(out float restored));
            Assert.AreEqual(4, inventory.estusLeft);
            Assert.AreEqual(2, inventory.ashLeft);
            Assert.AreEqual(120f, restored, 0.001f);

            inventory.estusLeft = 4;
            inventory.ashLeft = 2;
            Assert.IsTrue(inventory.EnterPhantom(PhantomSign.Yellow, 120f, 100f, out maxNow));
            Assert.AreEqual(2, inventory.estusLeft);
            Assert.AreEqual(1, inventory.ashLeft);
            inventory.ReturnPhantom(out _);

            inventory.estusLeft = 4;
            inventory.ashLeft = 2;
            Assert.IsFalse(inventory.EnterPhantom(PhantomSign.Red, 120f, 100f, out maxNow));
            Assert.AreEqual(4, inventory.estusLeft);
            Assert.AreEqual(2, inventory.ashLeft);
            Assert.AreEqual(120f, maxNow, 0.001f);
        }
    }
}
