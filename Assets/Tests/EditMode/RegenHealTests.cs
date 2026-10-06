using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class RegenHealTests
    {
        [Test]
        public void Apply_AddsThePerSecondAmount_AndStopsAtMax()
        {
            Assert.AreEqual(15f, RegenHeal.Apply(10f, 100f, 5f, 1f));
            Assert.AreEqual(17f, RegenHeal.Apply(10f, 100f, 7f, 1f));
            Assert.AreEqual(20f, RegenHeal.Apply(10f, 100f, 10f, 1f));
            Assert.AreEqual(100f, RegenHeal.Apply(98f, 100f, 10f, 1f));
            Assert.AreEqual(10f, RegenHeal.Apply(10f, 100f, 5f, 0f));
        }
    }
}
