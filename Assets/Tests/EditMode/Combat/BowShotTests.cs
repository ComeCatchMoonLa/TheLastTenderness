using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class BowShotTests
    {
        [Test]
        public void ShortHold_UsesQuick_LongHold_UsesFull_EmptyTime_StaysQuick()
        {
            Assert.IsFalse(BowShot.FullDraw("箭", true, 0.5f, 0.2f));
            Assert.AreEqual(10f, BowShot.Damage("箭", false, 10, true, 25));

            Assert.IsTrue(BowShot.FullDraw("箭", true, 0.5f, 0.5f));
            Assert.AreEqual(25f, BowShot.Damage("箭", true, 10, true, 25));

            LogAssert.Expect(LogType.Error, "箭: fullDrawTime 未填");
            Assert.IsFalse(BowShot.FullDraw("箭", false, 0f, 1f));
            Assert.AreEqual(10f, BowShot.Damage("箭", false, 10, true, 25));
        }
    }
}
