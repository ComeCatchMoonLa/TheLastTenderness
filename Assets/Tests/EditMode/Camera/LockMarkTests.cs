using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class LockMarkTests
    {
        [Test]
        public void Shown_OnlyWhileLockedAndTheTargetIsAlive()
        {
            Assert.IsTrue(LockMark.Shown(true, false));
            Assert.IsFalse(LockMark.Shown(false, false));
            Assert.IsFalse(LockMark.Shown(true, true));
        }
    }
}
