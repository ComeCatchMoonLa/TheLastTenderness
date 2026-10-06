using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class EmberSummonTests
    {
        [Test]
        public void Appears_OnlyWhenTheEmberIsLitAndAHelperIsPlaced()
        {
            Assert.IsFalse(EmberSummon.Appears(false, true));
            Assert.IsFalse(EmberSummon.Appears(true, false));
            Assert.IsTrue(EmberSummon.Appears(true, true));
        }
    }
}
