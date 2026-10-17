using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class RollIFrameTests
    {
        [Test]
        public void LightAndMid_OpenFromFrame2Through14()
        {
            Assert.IsFalse(RollIFrame.Open(1, false));
            Assert.IsTrue(RollIFrame.Open(2, false));
            Assert.IsTrue(RollIFrame.Open(14, false));
            Assert.IsFalse(RollIFrame.Open(15, false));
            Assert.AreEqual(1, RollIFrame.Frame(0f));
            Assert.AreEqual(2, RollIFrame.Frame(1f / 30f));
        }

        [Test]
        public void HeavyEstimate_EndsAtFrame13()
        {
            Assert.IsTrue(RollIFrame.Open(13, true));
            Assert.IsFalse(RollIFrame.Open(14, true));
        }
    }
}
