using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class RingBonusTests
    {
        [Test]
        public void Worn_ChangesTheThreeNumbers_Removed_RestoresThem()
        {
            Item[] slots = new Item[EquipmentLayout.RingSlots];
            Item ring = Ring("生命", 4f, 3f, 5);
            Assert.IsTrue(RingWear.TryPut(slots, ring));
            Assert.AreEqual(4f, RingBonus.Attack(slots));
            Assert.AreEqual(3f, RingBonus.Absorption(slots));
            Assert.AreEqual(5, RingBonus.Attribute(slots));
            Assert.AreEqual(14f, RingBonus.Physical(10f, RingBonus.Attack(slots), 0f));
            Assert.AreEqual(0f, RingBonus.Physical(2f, 0f, RingBonus.Absorption(slots)));

            Assert.IsTrue(RingWear.TryRemove(slots, ring));
            Assert.AreEqual(0f, RingBonus.Attack(slots));
            Assert.AreEqual(0f, RingBonus.Absorption(slots));
            Assert.AreEqual(0, RingBonus.Attribute(slots));
            Object.DestroyImmediate(ring);
        }

        [Test]
        public void SameFamilyAndFifth_DoNotWear()
        {
            Item[] slots = new Item[EquipmentLayout.RingSlots];
            Item first = Ring("力量", 1f, 0f, 0);
            Item plus = Ring("力量", 2f, 0f, 0);
            Assert.IsTrue(RingWear.TryPut(slots, first));
            Assert.IsFalse(RingWear.TryPut(slots, plus));
            Assert.AreEqual(1f, RingBonus.Attack(slots));

            Assert.IsTrue(RingWear.TryPut(slots, Ring("甲", 0f, 0f, 0)));
            Assert.IsTrue(RingWear.TryPut(slots, Ring("乙", 0f, 0f, 0)));
            Assert.IsTrue(RingWear.TryPut(slots, Ring("丙", 0f, 0f, 0)));
            Item fifth = Ring("丁", 9f, 0f, 0);
            Assert.IsFalse(RingWear.TryPut(slots, fifth));
            Assert.AreEqual(1f, RingBonus.Attack(slots));

            LogAssert.Expect(LogType.Error, "空: ringFamily 未填");
            Item blank = Ring("", 8f, 0f, 0);
            blank.name = "空";
            Assert.IsFalse(RingWear.TryPut(slots, blank));
        }

        static Item Ring(string family, float attack, float absorption, int attribute)
        {
            Item ring = ScriptableObject.CreateInstance<Item>();
            ring.isRing = true;
            ring.ringFamily = family;
            ring.attackBonus = attack;
            ring.absorptionBonus = absorption;
            ring.attributeBonus = attribute;
            return ring;
        }
    }
}
