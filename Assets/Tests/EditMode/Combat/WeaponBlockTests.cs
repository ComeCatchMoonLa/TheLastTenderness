using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class WeaponBlockTests
    {
        [Test]
        public void TwoHandStraightSword_UsesWeapon()
        {
            Assert.IsTrue(WeaponBlock.Use(true, WeaponType.melee_OH_RH));
        }

        [Test]
        public void ShieldUnarmedOrOneHand_DoesNot()
        {
            Assert.IsFalse(WeaponBlock.Use(true, WeaponType.melee_OH_Shield));
            Assert.IsFalse(WeaponBlock.Use(true, WeaponType.unarmed));
            Assert.IsFalse(WeaponBlock.Use(true, WeaponType.bow));
            Assert.IsFalse(WeaponBlock.Use(false, WeaponType.melee_OH_RH));
        }
    }
}
