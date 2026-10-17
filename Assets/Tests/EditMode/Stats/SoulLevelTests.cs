using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class SoulLevelTests
    {
        [Test]
        public void NineTens_IsOne_NineNinetynine_Is802()
        {
            Assert.AreEqual(1, SoulLevel.From(10, 10, 10, 10, 10, 10, 10, 10, 10));
            Assert.AreEqual(802, SoulLevel.From(99, 99, 99, 99, 99, 99, 99, 99, 99));
        }

        [Test]
        public void OnePoint_RaisesSoulLevelByOne()
        {
            int before = SoulLevel.From(10, 10, 10, 10, 10, 10, 10, 10, 10);
            int after = SoulLevel.From(11, 10, 10, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual(before + 1, after);
        }
    }
}
