using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class HoldAimOnceTests
    {
        [Test]
        public void HoldEntersOnce_ReleaseAllowsAnotherEntry()
        {
            bool entered = false;
            Assert.IsTrue(PlayerCombatManager.ShouldEnterHoldAction(true, ref entered));
            Assert.IsTrue(entered);
            Assert.IsFalse(PlayerCombatManager.ShouldEnterHoldAction(true, ref entered));
            Assert.IsFalse(PlayerCombatManager.ShouldEnterHoldAction(false, ref entered));
            Assert.IsFalse(entered);
            Assert.IsTrue(PlayerCombatManager.ShouldEnterHoldAction(true, ref entered));
        }
    }
}
