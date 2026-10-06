using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class LevelPreviewTests
    {
        [Test]
        public void Preview_RaisesOnlyTheChosenCap_ConfirmMatches_EmptyCostStops()
        {
            LevelPoints points = new LevelPoints { health = 10, focus = 10, stamina = 10 };
            LevelCaps preview = LevelPreview.Preview(points, LevelStat.Health);
            Assert.AreEqual(110f, preview.health);
            Assert.AreEqual(100f, preview.focus);
            Assert.AreEqual(100f, preview.stamina);

            Assert.IsTrue(LevelPreview.TryRaise("LevelTable", 100, 100, 10, points, LevelStat.Health, out int souls, out int level, out LevelPoints raised, out LevelCaps caps));
            Assert.AreEqual(0, souls);
            Assert.AreEqual(11, level);
            Assert.AreEqual(11, raised.health);
            Assert.AreEqual(10, raised.focus);
            Assert.AreEqual(10, raised.stamina);
            Assert.AreEqual(preview.health, caps.health);
            Assert.AreEqual(preview.focus, caps.focus);
            Assert.AreEqual(preview.stamina, caps.stamina);

            LogAssert.Expect(LogType.Error, "LevelTable: cost 未填");
            Assert.IsFalse(LevelPreview.TryRaise("LevelTable", 100, 0, 10, points, LevelStat.Health, out souls, out level, out raised, out caps));
            Assert.AreEqual(100, souls);
            Assert.AreEqual(10, level);
            Assert.AreEqual(10, raised.health);
            Assert.AreEqual(100f, caps.health);
        }
    }
}
