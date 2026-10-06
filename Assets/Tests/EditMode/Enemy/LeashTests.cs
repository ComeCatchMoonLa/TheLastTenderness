using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class LeashTests
    {
        [Test]
        public void TooFar_SendsItHome_Arrived_NeedsAFilledDistance()
        {
            Assert.IsTrue(Leash.TooFar(21f, 20f));
            Assert.IsFalse(Leash.TooFar(19f, 20f));
            Assert.IsFalse(Leash.TooFar(21f, 0f));

            bool warned = false;
            Assert.IsTrue(Leash.Arrived("敌人", 1f, 2f, ref warned));
            LogAssert.Expect(LogType.Error, "敌人: returnArrive 未填");
            Assert.IsFalse(Leash.Arrived("敌人", 1f, 0f, ref warned));
            Assert.IsTrue(warned);
        }
    }
}
