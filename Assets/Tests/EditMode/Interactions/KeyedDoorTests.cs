using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class KeyedDoorTests
    {
        [Test]
        public void Allowed_RequiresTheKeyOnlyWhenTheDoorAsksForOne()
        {
            Assert.IsFalse(KeyedDoor.Allowed(true, false));
            Assert.IsTrue(KeyedDoor.Allowed(true, true));
            Assert.IsTrue(KeyedDoor.Allowed(false, false));
        }
    }
}
