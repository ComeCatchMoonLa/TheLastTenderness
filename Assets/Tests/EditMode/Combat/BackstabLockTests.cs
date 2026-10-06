using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class BackstabLockTests
    {
        [Test]
        public void TryEnter_LocksOnceFromBehind_AndRefusesTheSideOrASecondPress()
        {
            Assert.IsTrue(BackstabLock.TryEnter(true, -0.9f, false, out bool locked));
            Assert.IsTrue(locked);

            Assert.IsFalse(BackstabLock.TryEnter(true, -0.9f, true, out locked));
            Assert.IsTrue(locked);

            Assert.IsFalse(BackstabLock.TryEnter(true, 0f, false, out locked));
            Assert.IsFalse(locked);
        }
    }
}
