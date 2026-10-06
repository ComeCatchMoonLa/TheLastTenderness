using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class BossPhaseTests
    {
        [Test]
        public void ShouldShift_OnlyAtTheFilledThreshold_AndOnlyOnce()
        {
            Assert.IsTrue(BossPhase.ShouldShift(50f, 100f, 0.5f, false));
            Assert.IsFalse(BossPhase.ShouldShift(60f, 100f, 0.5f, false));
            Assert.IsFalse(BossPhase.ShouldShift(50f, 100f, 0.5f, true));
            Assert.IsFalse(BossPhase.ShouldShift(50f, 100f, 0f, false));
        }
    }
}
