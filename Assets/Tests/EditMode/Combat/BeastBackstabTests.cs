using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class BeastBackstabTests
    {
        [Test]
        public void Beast_IsALightAttack_UnmarkedStaysBackstab()
        {
            Assert.IsFalse(BackstabBody.Allows(false, false, true));
            Assert.IsTrue(BackstabBody.Allows(false, false, false));
            Assert.AreEqual(
                CriticalAttackAction.CriticalStrikeChoice.LightAttack,
                CriticalAttackAction.Choose(true, -0.9f, false, false));
            Assert.AreEqual(
                CriticalAttackAction.CriticalStrikeChoice.Backstab,
                CriticalAttackAction.Choose(true, -0.9f, false, true));
        }
    }
}
