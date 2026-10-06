using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class ArenaDeviceTests
    {
        [Test]
        public void IsOn_ReadsTheFightSwitch_ThenTheAfterSwitch()
        {
            Assert.IsTrue(ArenaDevice.IsOn(true, true, false));
            Assert.IsFalse(ArenaDevice.IsOn(false, true, false));
            Assert.IsFalse(ArenaDevice.IsOn(true, false, true));
            Assert.IsTrue(ArenaDevice.IsOn(false, false, true));
        }
    }
}
