using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class EquipmentLayoutTests
    {
        [Test]
        public void Ensure_PadsEmptySlots_AndKeepsWhatWasThere()
        {
            WeaponItem kept = ScriptableObject.CreateInstance<WeaponItem>();
            WeaponItem[] hand = EquipmentLayout.Ensure(new WeaponItem[] { kept }, EquipmentLayout.WeaponsPerHand);
            Assert.AreEqual(3, hand.Length);
            Assert.AreSame(kept, hand[0]);
            Assert.IsNull(hand[1]);
            Assert.IsNull(hand[2]);

            Item[] rings = EquipmentLayout.Ensure((Item[])null, EquipmentLayout.RingSlots);
            Assert.AreEqual(4, rings.Length);
            Assert.IsNull(rings[0]);
            Assert.AreEqual(4, EquipmentLayout.ArmorSlotNames.Length);
            Assert.AreEqual("头", EquipmentLayout.ArmorSlotNames[0]);
            Assert.AreEqual("胸", EquipmentLayout.ArmorSlotNames[1]);
            Assert.AreEqual("手", EquipmentLayout.ArmorSlotNames[2]);
            Assert.AreEqual("腿", EquipmentLayout.ArmorSlotNames[3]);
        }
    }
}
