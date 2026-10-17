using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class RingDiscoveryTests
    {
        [Test]
        public void Worn_RaisesDiscovery_Removed_IsLuckPlus100()
        {
            Item[] slots = new Item[EquipmentLayout.RingSlots];
            Item ring = ScriptableObject.CreateInstance<Item>();
            ring.isRing = true;
            ring.ringFamily = "发现";
            ring.discoveryBonus = 50;

            Assert.IsTrue(RingWear.TryPut(slots, ring));
            Assert.AreEqual(160, RingDiscovery.Total(10, slots));

            Assert.IsTrue(RingWear.TryRemove(slots, ring));
            Assert.AreEqual(110, RingDiscovery.Total(10, slots));
            Object.DestroyImmediate(ring);
        }
    }
}
