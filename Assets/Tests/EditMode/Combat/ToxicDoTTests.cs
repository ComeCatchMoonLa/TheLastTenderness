using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class ToxicDoTTests
    {
        [Test]
        public void PurpleMoss_LeavesToxic_BloomingMoss_StopsOnlyToxic()
        {
            Assert.IsTrue(ToxicDoT.TryStart("剧毒", 10f, 10f, 2f, 8f, 0.5f, out float toxicTime, out float cleared));
            Assert.AreEqual(0f, cleared);
            float health = ToxicDoT.Tick("剧毒", 100f, toxicTime, 8f, 1f, out toxicTime);
            Assert.AreEqual(92f, health);
            Assert.AreEqual(1f, toxicTime);
            Assert.AreEqual(50f, ToxicDoT.Regen("剧毒", 100f, true, 0.5f));

            float poisonTime = 2f;
            Assert.IsTrue(MossChoice.Apply(false, ref poisonTime, ref toxicTime));
            Assert.AreEqual(0f, poisonTime);
            Assert.AreEqual(1f, toxicTime);
            Assert.AreEqual(50f, ToxicDoT.Regen("剧毒", 100f, toxicTime > 0f, 0.5f));

            poisonTime = 2f;
            Assert.IsTrue(MossChoice.Apply(true, ref poisonTime, ref toxicTime));
            Assert.AreEqual(2f, poisonTime);
            Assert.AreEqual(0f, toxicTime);
            Assert.AreEqual(92f, ToxicDoT.Tick("剧毒", health, toxicTime, 8f, 1f, out _));
            Assert.AreEqual(100f, ToxicDoT.Regen("剧毒", 100f, false, 0.5f));

            LogAssert.Expect(LogType.Error, "剧毒: toxicDuration 未填");
            Assert.IsFalse(ToxicDoT.TryStart("剧毒", 10f, 10f, 0f, 8f, 0.5f, out _, out float held));
            Assert.AreEqual(10f, held);
        }
    }
}
