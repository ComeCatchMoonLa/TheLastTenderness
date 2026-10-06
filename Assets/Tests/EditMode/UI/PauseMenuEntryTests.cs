using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class PauseMenuEntryTests
    {
        [Test]
        public void Entries_AreBackpackStatusAndStance_WithoutLevelUp()
        {
            Assert.IsTrue(PauseMenuEntries.Contains("背包"));
            Assert.IsTrue(PauseMenuEntries.Contains("状态"));
            Assert.IsTrue(PauseMenuEntries.Contains("姿态"));
            Assert.IsFalse(PauseMenuEntries.Contains("加点"));
            Assert.AreEqual(3, PauseMenuEntries.Names.Length);
        }
    }
}
