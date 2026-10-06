using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class LevelPurchaseTests
    {
        [Test]
        public void TryConfirm_SpendsWhenEnough_AndStopsWhenEmptyOrShort()
        {
            LogAssert.Expect(LogType.Error, "LevelTable: cost 未填");
            Assert.IsFalse(LevelPurchase.TryConfirm("LevelTable", 100, 0, 10, out int souls, out int level));
            Assert.AreEqual(100, souls);
            Assert.AreEqual(10, level);

            Assert.IsFalse(LevelPurchase.TryConfirm("LevelTable", 99, 100, 10, out souls, out level));
            Assert.AreEqual(99, souls);
            Assert.AreEqual(10, level);

            Assert.IsTrue(LevelPurchase.TryConfirm("LevelTable", 100, 100, 10, out souls, out level));
            Assert.AreEqual(0, souls);
            Assert.AreEqual(11, level);
            Assert.IsFalse(CampfireMenu.Contains("升级"));
        }
    }
}
