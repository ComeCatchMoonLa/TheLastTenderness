using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class SpellHealTests
    {
        [Test]
        public void BelowMax_StopsAtMax()
        {
            Assert.AreEqual(100f, SpellHeal.Apply(80f, 100f, 50f), 0.001f);
        }

        [Test]
        public void AlreadyFull_StaysFull()
        {
            Assert.AreEqual(100f, SpellHeal.Apply(100f, 100f, 50f), 0.001f);
        }
    }
}
