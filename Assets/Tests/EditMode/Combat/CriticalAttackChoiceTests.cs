using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class CriticalAttackChoiceTests
    {
        [Test]
        public void Choose_BackstabInsideTheRearWindow()
        {
            Assert.AreEqual(CriticalAttackAction.CriticalStrikeChoice.Backstab, CriticalAttackAction.Choose(true, -1f, false));
            Assert.AreEqual(CriticalAttackAction.CriticalStrikeChoice.Backstab, CriticalAttackAction.Choose(true, -0.8f, false));
            Assert.AreEqual(CriticalAttackAction.CriticalStrikeChoice.Backstab, CriticalAttackAction.Choose(true, -0.9f, true));
        }

        [Test]
        public void Choose_RiposteOnlyInFrontWhenOpen()
        {
            Assert.AreEqual(CriticalAttackAction.CriticalStrikeChoice.Riposte, CriticalAttackAction.Choose(true, 0.8f, true));
            Assert.AreEqual(CriticalAttackAction.CriticalStrikeChoice.Riposte, CriticalAttackAction.Choose(true, 1f, true));
            Assert.AreEqual(CriticalAttackAction.CriticalStrikeChoice.LightAttack, CriticalAttackAction.Choose(true, 0.9f, false));
        }

        [Test]
        public void Choose_LightAttackWhenTheAngleOrTheRayMisses()
        {
            Assert.AreEqual(CriticalAttackAction.CriticalStrikeChoice.LightAttack, CriticalAttackAction.Choose(true, -0.79f, false));
            Assert.AreEqual(CriticalAttackAction.CriticalStrikeChoice.LightAttack, CriticalAttackAction.Choose(true, 0f, true));
            Assert.AreEqual(CriticalAttackAction.CriticalStrikeChoice.LightAttack, CriticalAttackAction.Choose(false, -1f, true));
        }
    }
}
