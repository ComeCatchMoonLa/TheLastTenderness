using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class ParryWindowTests
    {
        [Test]
        public void Open_OnlyInsideTheFilledWindow()
        {
            Assert.IsTrue(ParryWindow.Open(0, 5, "弹反"));
            Assert.IsTrue(ParryWindow.Open(4, 5, "弹反"));
            Assert.IsFalse(ParryWindow.Open(5, 5, "弹反"));

            LogAssert.Expect(LogType.Error, "弹反: parryWindowFrames 未填");
            Assert.IsFalse(ParryWindow.Open(0, 0, "弹反"));
        }
    }
}
