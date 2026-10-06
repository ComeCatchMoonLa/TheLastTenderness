using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class CycleGateTests
    {
        [Test]
        public void TryRecord_KeepsTheFirstEnding_AllowNeedsOne()
        {
            string recorded = null;
            Assert.IsTrue(CycleGate.TryRecord(ref recorded, "传火", "终局"));
            Assert.AreEqual("传火", recorded);
            Assert.IsFalse(CycleGate.TryRecord(ref recorded, "窃火", "终局"));
            Assert.AreEqual("传火", recorded);

            LogAssert.Expect(LogType.Error, "终局: ending 未填");
            string empty = null;
            Assert.IsFalse(CycleGate.TryRecord(ref empty, "", "终局"));
            Assert.IsTrue(string.IsNullOrEmpty(empty));

            Assert.IsFalse(CycleGate.Allow(null));
            Assert.IsTrue(CycleGate.Allow(recorded));
        }
    }
}
