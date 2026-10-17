using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class RollChainTests
    {
        [Test]
        public void Filled_SecondCostsMore_PauseReturnsToBase()
        {
            float first = RollChain.Cost("人", true, 5f, true, 0.4f, float.PositiveInfinity, 0, 10f, out int streak);
            Assert.AreEqual(10f, first);
            Assert.AreEqual(1, streak);

            float second = RollChain.Cost("人", true, 5f, true, 0.4f, 0.2f, streak, 10f, out streak);
            Assert.AreEqual(15f, second);
            Assert.AreEqual(2, streak);

            float afterPause = RollChain.Cost("人", true, 5f, true, 0.4f, 0.5f, streak, 10f, out streak);
            Assert.AreEqual(10f, afterPause);
            Assert.AreEqual(1, streak);
        }

        [Test]
        public void Unfilled_LogsAndStaysAtBase()
        {
            LogAssert.Expect(LogType.Error, "人: rollChainStep 未填");
            float cost = RollChain.Cost("人", false, 0f, true, 0.4f, 0.1f, 2, 10f, out int streak);
            Assert.AreEqual(10f, cost);
            Assert.AreEqual(0, streak);

            LogAssert.Expect(LogType.Error, "人: rollChainPause 未填");
            cost = RollChain.Cost("人", true, 5f, false, 0f, 0.1f, 2, 10f, out streak);
            Assert.AreEqual(10f, cost);
            Assert.AreEqual(0, streak);
        }
    }
}
