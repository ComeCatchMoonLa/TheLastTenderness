using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class FogDoorTests
    {
        [Test]
        public void Begin_FillsHealth_AndCanLeaveOnlyAfterTheFight()
        {
            float current = 40f;
            FogDoor.Begin(ref current, 100f);
            Assert.AreEqual(100f, current);
            Assert.IsFalse(FogDoor.CanLeave(true));
            Assert.IsTrue(FogDoor.CanLeave(false));
        }
    }
}
