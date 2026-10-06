using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class LastBonfireTests
    {
        [Test]
        public void Remember_WritesOnSit_AndLeavesItWhenRefusedOrNotCalled()
        {
            LastBonfire.Remember("甲", true);
            Assert.AreEqual("甲", LastBonfire.recorded);

            LastBonfire.Remember("乙", false);
            Assert.AreEqual("甲", LastBonfire.recorded);

            Assert.AreEqual("甲", LastBonfire.recorded);
        }
    }
}
