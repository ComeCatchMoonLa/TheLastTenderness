using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class WalkSpeedTests
    {
        [Test]
        public void SmallInput_Walks_FullStick_Runs_Sprint_StaysFaster()
        {
            Assert.AreEqual(2f, WalkSpeed.Choose("玩家", 0.3f, false, 2f, 5f, 7f, 0.5f));
            Assert.AreEqual(5f, WalkSpeed.Choose("玩家", 1f, false, 2f, 5f, 7f, 0.5f));
            Assert.AreEqual(7f, WalkSpeed.Choose("玩家", 0.3f, true, 2f, 5f, 7f, 0.5f));
            Assert.AreEqual(5f, WalkSpeed.Choose("玩家", 0.3f, false, 2f, 5f, 7f, 0f));

            LogAssert.Expect(LogType.Error, "玩家: walkSpeed 未填");
            Assert.AreEqual(5f, WalkSpeed.Choose("玩家", 0.3f, false, 0f, 5f, 7f, 0.5f));
        }
    }
}
