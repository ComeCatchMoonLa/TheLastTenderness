using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class EnumSpellingTests
    {
        [Test]
        public void RenamedMembers_KeepTheirIntegerValues()
        {
            Assert.AreEqual(0, (int)WeaponType.unarmed);
            Assert.AreEqual(2, (int)SpellType.sorcery);
            Assert.AreEqual(5, (int)EquipmentSlotType.armor_Torso_Slot);
            Assert.AreEqual(0, (int)ChangeLockOnTargetMode.nearest);

            Assert.IsFalse(System.Enum.IsDefined(typeof(WeaponType), "unaremd"));
            Assert.IsFalse(System.Enum.IsDefined(typeof(SpellType), "sorvery"));
            Assert.IsFalse(System.Enum.IsDefined(typeof(EquipmentSlotType), "armor_Toros_Slot"));
            Assert.IsFalse(System.Enum.IsDefined(typeof(ChangeLockOnTargetMode), "nearset"));
        }
    }
}
