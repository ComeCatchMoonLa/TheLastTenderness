using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class ShieldKickTests
    {
        [Test]
        public void ForwardWithoutSprint_IsKickAndSpendsLightCost()
        {
            Assert.IsTrue(ShieldKick.IsKick(false, true));
            Assert.AreEqual(12f, ShieldKick.Stamina(true, 12f), 0.001f);
        }

        [Test]
        public void SprintOrNotForward_IsNotKick()
        {
            Assert.IsFalse(ShieldKick.IsKick(true, true));
            Assert.IsFalse(ShieldKick.IsKick(false, false));
            Assert.AreEqual(0f, ShieldKick.Stamina(false, 12f), 0.001f);
        }
    }
}
