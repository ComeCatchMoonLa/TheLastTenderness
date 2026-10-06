using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class BossThicknessTests
    {
        [Test]
        public void TryRaise_MultipliesOnlyWhenAnAllyIsPresentAndTheFactorIsFilled()
        {
            Assert.IsFalse(BossThickness.TryRaise(100f, false, 1.5f, "头目", out float same));
            Assert.AreEqual(100f, same);

            Assert.IsTrue(BossThickness.TryRaise(100f, true, 1.5f, "头目", out float raised));
            Assert.AreEqual(150f, raised);

            LogAssert.Expect(LogType.Error, "头目: allyHealthMultiplier 未填");
            Assert.IsFalse(BossThickness.TryRaise(100f, true, 0f, "头目", out float unchanged));
            Assert.AreEqual(100f, unchanged);
        }
    }
}
