using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class ParryShieldTests
    {
        [Test]
        public void OneHandParryShield_UsesLeft()
        {
            Assert.IsTrue(ParryShield.UseLeft(false, WeaponType.melee_OH_Shield, false, true));
        }

        [Test]
        public void TwoHanding_StanceShield_OrMissingParry_DoesNotUseLeft()
        {
            Assert.IsFalse(ParryShield.UseLeft(true, WeaponType.melee_OH_Shield, false, true));
            Assert.IsFalse(ParryShield.UseLeft(false, WeaponType.melee_OH_Shield, true, true));
            Assert.IsFalse(ParryShield.UseLeft(false, WeaponType.melee_OH_RH, false, true));
            Assert.IsFalse(ParryShield.UseLeft(false, WeaponType.melee_OH_Shield, false, false));
        }
    }
}
