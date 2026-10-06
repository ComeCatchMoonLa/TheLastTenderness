using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class EndingChoiceTests
    {
        [Test]
        public void Choose_ReturnsExactlyOneEnding()
        {
            Assert.AreEqual("窃火", EndingChoice.Choose(true, true, true));
            Assert.AreEqual("窃火", EndingChoice.Choose(true, false, false));
            Assert.AreEqual("灭火", EndingChoice.Choose(false, true, true));
            Assert.AreEqual("传火", EndingChoice.Choose(false, true, false));
            Assert.AreEqual("传火", EndingChoice.Choose(false, false, true));
            Assert.AreEqual("传火", EndingChoice.Choose(false, false, false));
        }
    }
}
