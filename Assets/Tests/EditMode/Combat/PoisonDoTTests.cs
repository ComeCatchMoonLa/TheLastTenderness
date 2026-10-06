using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class PoisonDoTTests
    {
        [Test]
        public void FullMeter_DrainsForTheDuration_MossStopsIt()
        {
            float meter = BleedMeter.Add("中毒", 0f, 10f, 0f, 10f);
            Assert.IsTrue(PoisonDoT.TryStart("中毒", meter, 10f, 2f, out float time, out float cleared));
            Assert.AreEqual(0f, cleared);
            Assert.AreEqual(2f, time);

            float health = PoisonDoT.Tick("中毒", 100f, time, 5f, 1f, out time);
            Assert.AreEqual(95f, health);
            Assert.AreEqual(1f, time);

            health = PoisonDoT.Tick("中毒", health, time, 5f, 1f, out time);
            Assert.AreEqual(90f, health);
            Assert.AreEqual(0f, time);
            Assert.AreEqual(90f, PoisonDoT.Tick("中毒", health, time, 5f, 1f, out _));

            Assert.IsTrue(PoisonDoT.TryStart("中毒", 10f, 10f, 2f, out time, out _));
            health = PoisonDoT.Tick("中毒", 100f, time, 5f, 1f, out time);
            time = PoisonDoT.Stop();
            Assert.AreEqual(95f, PoisonDoT.Tick("中毒", health, time, 5f, 1f, out _));

            LogAssert.Expect(LogType.Error, "中毒: poisonDuration 未填");
            Assert.IsFalse(PoisonDoT.TryStart("中毒", 10f, 10f, 0f, out _, out float held));
            Assert.AreEqual(10f, held);
            Assert.IsFalse(PoisonDoT.TryStart("中毒", 4f, 10f, 2f, out _, out _));
        }
    }
}
