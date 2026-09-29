using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class HipsModelChangerRenameTests
    {
        [Test]
        public void TypeName_MatchesHipsSlot()
        {
            Assert.AreEqual("HipsModelChanger", typeof(HipsModelChanger).Name);
            Assert.AreEqual("armor_Hips_Slot", EquipmentSlotType.armor_Hips_Slot.ToString());
        }
    }
}
