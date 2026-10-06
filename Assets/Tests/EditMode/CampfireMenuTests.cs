using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class CampfireMenuTests
    {
        [Test]
        public void Names_AreTheFourEntries_WithoutLevelUp()
        {
            Assert.IsTrue(CampfireMenu.Contains("传送"));
            Assert.IsTrue(CampfireMenu.Contains("整理法术"));
            Assert.IsTrue(CampfireMenu.Contains("储物箱"));
            Assert.IsTrue(CampfireMenu.Contains("离开"));
            Assert.IsFalse(CampfireMenu.Contains("升级"));
            Assert.AreEqual(4, CampfireMenu.Names.Length);
        }
    }
}
