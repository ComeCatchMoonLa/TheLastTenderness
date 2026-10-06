using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class TerritoryTests
    {
        [Test]
        public void PulledOut_RefillsOnlyANormalEnemy_AndCanResumeInsideRange()
        {
            Assert.IsTrue(Territory.PulledOut(false, 30f, 10f));
            Assert.IsFalse(Territory.PulledOut(true, 30f, 10f));
            Assert.IsFalse(Territory.PulledOut(false, 30f, 0f));

            Assert.AreEqual(100f, Territory.Refill(40f, 100f));

            Assert.IsTrue(Territory.Resume(true, true, 3f, 5f));
            Assert.IsFalse(Territory.Resume(true, true, 8f, 5f));
        }
    }
}
