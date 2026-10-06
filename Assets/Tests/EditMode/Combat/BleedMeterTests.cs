using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class BleedMeterTests
    {
        [Test]
        public void FullMeter_ChunksAndClears_ResistSlows_MossDoesNotChunk()
        {
            float full = BleedMeter.Add("出血", 0f, 10f, 0f, 10f);
            Assert.IsTrue(BleedMeter.TryProc(full, 10f, out float cleared));
            Assert.AreEqual(0f, cleared);
            Assert.AreEqual(70f, BleedMeter.ApplyChunk(100f, 30f));

            float slowed = BleedMeter.Add("出血", 0f, 10f, 0.5f, 10f);
            Assert.AreEqual(5f, slowed);
            Assert.IsFalse(BleedMeter.TryProc(slowed, 10f, out _));

            float moss = BleedMeter.Clear();
            Assert.AreEqual(0f, moss);
            Assert.IsFalse(BleedMeter.TryProc(moss, 10f, out _));

            LogAssert.Expect(LogType.Error, "出血: capacity 未填");
            Assert.AreEqual(3f, BleedMeter.Add("出血", 3f, 10f, 0f, 0f));
        }
    }
}
