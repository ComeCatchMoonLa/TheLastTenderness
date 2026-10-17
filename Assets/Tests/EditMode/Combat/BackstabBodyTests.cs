using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class BackstabBodyTests
    {
        [Test]
        public void BossOrLarge_IsALightAttack_UnmarkedStaysBackstab()
        {
            Assert.IsFalse(BackstabBody.Allows(true, false));
            Assert.IsFalse(BackstabBody.Allows(false, true));
            Assert.IsTrue(BackstabBody.Allows(false, false));

            Assert.AreEqual(
                CriticalAttackAction.CriticalStrikeChoice.LightAttack,
                CriticalAttackAction.Choose(true, -0.9f, false, false));
            Assert.AreEqual(
                CriticalAttackAction.CriticalStrikeChoice.Backstab,
                CriticalAttackAction.Choose(true, -0.9f, false, true));
        }
    }
}
