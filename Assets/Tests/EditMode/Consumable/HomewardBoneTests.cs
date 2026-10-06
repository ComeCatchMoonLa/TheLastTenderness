using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class HomewardBoneTests
    {
        [Test]
        public void TryUse_ReturnsToTheLastFire_AndLeavesSoulsAndEmber()
        {
            HomewardState used = new HomewardState
            {
                bones = 1,
                lastFire = "甲",
                place = "乙",
                souls = 40,
                emberLit = true,
                hp = 10f,
                maxHp = 100f,
                mp = 5f,
                maxMp = 80f,
                estus = 0,
                estusShare = 3,
                ash = 1,
                ashShare = 2
            };
            Assert.IsTrue(HomewardBone.TryUse(used));
            Assert.AreEqual("甲", used.place);
            Assert.AreEqual(0, used.bones);
            Assert.AreEqual(100f, used.hp);
            Assert.AreEqual(80f, used.mp);
            Assert.AreEqual(3, used.estus);
            Assert.AreEqual(2, used.ash);
            Assert.AreEqual(40, used.souls);
            Assert.IsTrue(used.emberLit);
            Assert.IsTrue(used.resetEnemies);

            HomewardState refused = new HomewardState
            {
                bossBarVisible = true,
                bones = 1,
                lastFire = "甲",
                place = "乙",
                souls = 40,
                emberLit = true,
                hp = 10f,
                maxHp = 100f,
                mp = 5f,
                maxMp = 80f,
                estus = 0,
                estusShare = 3,
                ash = 1,
                ashShare = 2
            };
            Assert.IsFalse(HomewardBone.TryUse(refused));
            Assert.AreEqual("乙", refused.place);
            Assert.AreEqual(1, refused.bones);
            Assert.AreEqual(10f, refused.hp);
            Assert.AreEqual(5f, refused.mp);
            Assert.AreEqual(0, refused.estus);
            Assert.AreEqual(1, refused.ash);
            Assert.AreEqual(40, refused.souls);
            Assert.IsTrue(refused.emberLit);
            Assert.IsFalse(refused.resetEnemies);
        }
    }
}
