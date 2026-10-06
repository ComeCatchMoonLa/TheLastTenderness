using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class ScaleInfusionTests
    {
        [Test]
        public void List_DropsTheFifteenOnScaleAndTwinkling_AndKeepsThemOnNormal()
        {
            string[] names = { "甲", "乙" };
            string[] into = new string[2];

            Assert.AreEqual(0, ScaleInfusion.List(UpgradePath.scale, names, into));
            Assert.AreEqual(0, ScaleInfusion.List(UpgradePath.twinkling, names, into));

            int count = ScaleInfusion.List(UpgradePath.normal, names, into);
            Assert.AreEqual(2, count);
            Assert.AreEqual("甲", into[0]);
            Assert.AreEqual("乙", into[1]);
        }
    }
}
