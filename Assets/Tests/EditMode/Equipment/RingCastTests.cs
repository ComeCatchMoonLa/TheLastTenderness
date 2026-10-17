using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class RingCastTests
    {
        [Test]
        public void Worn_RaisesSpellAndCritical_Removed_RestoresThem()
        {
            Item[] slots = new Item[EquipmentLayout.RingSlots];
            Item ring = ScriptableObject.CreateInstance<Item>();
            ring.isRing = true;
            ring.ringFamily = "法术";
            ring.spellBonus = 4;
            ring.criticalBonus = 0.5f;

            Assert.IsTrue(RingWear.TryPut(slots, ring));
            Assert.AreEqual(14, 10 + RingCast.Spell(slots));
            Assert.AreEqual(2.9f, 2.4f + RingCast.Critical(slots));

            Assert.IsTrue(RingWear.TryRemove(slots, ring));
            Assert.AreEqual(10, 10 + RingCast.Spell(slots));
            Assert.AreEqual(2.4f, 2.4f + RingCast.Critical(slots));
            Object.DestroyImmediate(ring);
        }
    }
}
