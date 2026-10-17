using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class UsurpationTests
    {
        [Test]
        public void ClearedSigils_DropUsurpation()
        {
            Assert.AreEqual("窃火", EndingChoice.Choose(Usurpation.Holds(true, false), false, false));
            Assert.AreEqual("灭火", EndingChoice.Choose(Usurpation.Holds(true, true), true, true));
            Assert.AreEqual("传火", EndingChoice.Choose(Usurpation.Holds(true, true), false, false));
        }
    }
}
