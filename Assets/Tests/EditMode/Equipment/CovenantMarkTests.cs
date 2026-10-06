using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class CovenantMarkTests
    {
        [Test]
        public void Active_ReadsOnlyTheFirstEquippedMark()
        {
            Item inBag = ScriptableObject.CreateInstance<Item>();
            inBag.isCovenantMark = true;
            Item first = ScriptableObject.CreateInstance<Item>();
            first.isCovenantMark = true;
            Item second = ScriptableObject.CreateInstance<Item>();
            second.isCovenantMark = true;
            Item[] empty = new Item[EquipmentLayout.RingSlots];
            Assert.IsNull(CovenantMark.Active(empty));

            Item[] worn = new Item[EquipmentLayout.RingSlots];
            worn[0] = first;
            worn[1] = second;
            Assert.AreSame(first, CovenantMark.Active(worn));

            Item[] pastTheFourth = new Item[5];
            pastTheFourth[4] = inBag;
            Assert.IsNull(CovenantMark.Active(pastTheFourth));
        }
    }
}
