using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class AttackPoiseTests
    {
        [Test]
        public void Holds_UntilThePoolIsEmptied()
        {
            Assert.IsTrue(AttackPoise.Holds(30f, 10f, out float left));
            Assert.AreEqual(20f, left);
            Assert.IsFalse(AttackPoise.Holds(left, 20f, out float empty));
            Assert.AreEqual(0f, empty);
            Assert.IsFalse(AttackPoise.Holds(30f, 40f, out _));
        }
    }
}
