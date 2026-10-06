using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class StatusBuildupTests
    {
        [Test]
        public void Add_SkipsWhileInvulnerable_AndAddsAfterItEnds()
        {
            float stayed = StatusBuildup.Add(true, "出血", 3f, 10f, 0f, 20f);
            Assert.AreEqual(3f, stayed);

            float next = StatusBuildup.Add(false, "出血", 3f, 10f, 0f, 20f);
            Assert.AreEqual(13f, next);
        }
    }
}
