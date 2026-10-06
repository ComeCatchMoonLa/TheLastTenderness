using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class DarkSignTests
    {
        [Test]
        public void TryUse_ZerosSouls_AndDoesNotWriteARemnant()
        {
            DarkSignState used = new DarkSignState
            {
                souls = 40,
                lastFire = "甲",
                place = "乙",
                hadRemnant = true,
                remnant = 10
            };
            Assert.IsTrue(DarkSign.TryUse(used));
            Assert.AreEqual("甲", used.place);
            Assert.AreEqual(0, used.souls);
            Assert.IsTrue(used.hadRemnant);
            Assert.AreEqual(10, used.remnant);

            DarkSignState refused = new DarkSignState
            {
                bossFight = true,
                souls = 40,
                lastFire = "甲",
                place = "乙",
                hadRemnant = true,
                remnant = 10
            };
            Assert.IsFalse(DarkSign.TryUse(refused));
            Assert.AreEqual("乙", refused.place);
            Assert.AreEqual(40, refused.souls);
            Assert.IsTrue(refused.hadRemnant);
            Assert.AreEqual(10, refused.remnant);
        }
    }
}
