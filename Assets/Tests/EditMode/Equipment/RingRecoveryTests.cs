using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class RingRecoveryTests
    {
        [Test]
        public void Worn_RaisesRecovery_Removed_RestoresIt_ChargesStay()
        {
            Item[] slots = new Item[EquipmentLayout.RingSlots];
            Item ring = ScriptableObject.CreateInstance<Item>();
            ring.isRing = true;
            ring.ringFamily = "回复";
            ring.staminaRegenBonus = 5f;
            ring.estusBonus = 20;
            ring.ashenBonus = 8;
            int charges = 3;

            Assert.IsTrue(RingWear.TryPut(slots, ring));
            Assert.AreEqual(5f, RingRecovery.Stamina(slots));
            Assert.AreEqual(120, 100 + RingRecovery.Estus(slots));
            Assert.AreEqual(28, 20 + RingRecovery.Ashen(slots));
            Assert.AreEqual(3, charges);

            Assert.IsTrue(RingWear.TryRemove(slots, ring));
            Assert.AreEqual(0f, RingRecovery.Stamina(slots));
            Assert.AreEqual(100, 100 + RingRecovery.Estus(slots));
            Assert.AreEqual(3, charges);
            Object.DestroyImmediate(ring);
        }
    }
}
