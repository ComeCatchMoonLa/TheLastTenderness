using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class FrostbiteTests
    {
        [Test]
        public void FullMeter_ChunksThenMultiplies_FireOrMossClears()
        {
            float meter = BleedMeter.Add("冰霜", 0f, 10f, 0f, 10f);
            Assert.IsTrue(Frostbite.TryProc("冰霜", meter, 10f, 2f, 30f, 2f, 0.5f, 100f, out float health, out float time, out float cleared));
            Assert.AreEqual(70f, health);
            Assert.AreEqual(0f, cleared);
            Assert.AreEqual(2f, time);

            Assert.AreEqual(20f, Frostbite.Taken("冰霜", 10f, true, 2f));
            Assert.AreEqual(50f, Frostbite.Regen("冰霜", 100f, true, 0.5f));
            Assert.AreEqual(10f, Frostbite.Taken("冰霜", 10f, false, 2f));
            Assert.AreEqual(100f, Frostbite.Regen("冰霜", 100f, false, 0.5f));

            Assert.IsTrue(Frostbite.ClearedByFire(1f, time));
            Assert.AreEqual(10f, Frostbite.Taken("冰霜", 10f, false, 2f));

            Assert.IsTrue(Frostbite.ClearWithBlueMoss(4f, 0f, out float mossed, out _));
            Assert.AreEqual(0f, mossed);

            time = Frostbite.Tick(2f, 2f);
            Assert.AreEqual(0f, time);
            Assert.IsFalse(Frostbite.ClearedByFire(1f, time));

            LogAssert.Expect(LogType.Error, "冰霜: frostDuration 未填");
            Assert.IsFalse(Frostbite.TryProc("冰霜", 10f, 10f, 0f, 30f, 2f, 0.5f, 100f, out float same, out _, out float held));
            Assert.AreEqual(100f, same);
            Assert.AreEqual(10f, held);
        }
    }
}
