using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class BossBarTests
    {
        [Test]
        public void TryShow_LengthMatchesCurrentHealth_EmptyNameStaysClosed()
        {
            Assert.IsTrue(BossBar.TryShow("甲", 50f, 100f, "头目", out string shown, out float length));
            Assert.AreEqual("甲", shown);
            Assert.AreEqual(0.5f, length);

            LogAssert.Expect(LogType.Error, "头目: cName 未填");
            Assert.IsFalse(BossBar.TryShow("", 50f, 100f, "头目", out _, out _));
        }
    }
}
