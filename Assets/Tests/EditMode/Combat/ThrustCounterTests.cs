using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class ThrustCounterTests
    {
        [Test]
        public void AttackingThrust_ScalesByWeaponAndRing()
        {
            Assert.AreEqual(1.5f, ThrustCounter.Scale(true, PhysicalKind.Thrust, true, 1.5f, 1f), 0.001f);
            Assert.AreEqual(3f, ThrustCounter.Scale(true, PhysicalKind.Thrust, true, 1.5f, 2f), 0.001f);
        }

        [Test]
        public void IdleSlashOrUnfilled_StaysOne()
        {
            Assert.AreEqual(1f, ThrustCounter.Scale(false, PhysicalKind.Thrust, true, 1.5f, 2f), 0.001f);
            Assert.AreEqual(1f, ThrustCounter.Scale(true, PhysicalKind.Slash, true, 1.5f, 2f), 0.001f);
            Assert.AreEqual(1f, ThrustCounter.Scale(true, PhysicalKind.Thrust, false, 1.5f, 2f), 0.001f);
            Assert.AreEqual(1.5f, ThrustCounter.Scale(true, PhysicalKind.Thrust, true, 1.5f, 0f), 0.001f);
        }
    }
}
