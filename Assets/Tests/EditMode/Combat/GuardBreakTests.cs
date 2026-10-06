using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class GuardBreakTests
    {
        [Test]
        public void OpensRiposte_OnlyWhenStaminaIsEmpty_ThenChooseRiposte()
        {
            Assert.IsFalse(GuardBreak.OpensRiposte(1f));
            Assert.IsTrue(GuardBreak.OpensRiposte(0f));
            Assert.AreEqual(
                CriticalAttackAction.CriticalStrikeChoice.LightAttack,
                CriticalAttackAction.Choose(true, 0.9f, false));
            Assert.AreEqual(
                CriticalAttackAction.CriticalStrikeChoice.Riposte,
                CriticalAttackAction.Choose(true, 0.9f, GuardBreak.OpensRiposte(0f)));
        }
    }
}
