using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class CurseMeterTests
    {
        [Test]
        public void FullMeter_KillsAndClears_DecayAndBlessLeaveTheLiving()
        {
            float full = BleedMeter.Add("咒死", 0f, 10f, 0f, 10f);
            Assert.IsTrue(CurseMeter.TryKill(full, 10f, out float cleared));
            Assert.AreEqual(0f, cleared);

            float left = CurseMeter.Decay("咒死", 5f, 2f, 1f);
            Assert.AreEqual(3f, left);
            Assert.IsFalse(CurseMeter.TryKill(left, 10f, out _));

            LogAssert.Expect(LogType.Error, "咒死: curseDecay 未填");
            Assert.AreEqual(5f, CurseMeter.Decay("咒死", 5f, 0f, 1f));

            Assert.IsTrue(CurseMeter.TryBless(5f, 10f, out float blessed));
            Assert.AreEqual(0f, blessed);

            Assert.IsFalse(CurseMeter.TryBless(10f, 10f, out float stillFull));
            Assert.AreEqual(10f, stillFull);

            Assert.AreEqual(0f, CurseMeter.Decay("咒死", 0f, 0f, 1f));
        }
    }
}
