using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class OnlineMarksTests
    {
        [Test]
        public void Online_ReadsAndRatesOnce_OfflineHidesMarks_RemnantStays()
        {
            Assert.IsTrue(OnlineMarks.TryRead("留言", true, "你好", out string read));
            Assert.AreEqual("你好", read);
            Assert.IsTrue(OnlineMarks.TryRate(true, false));
            Assert.IsFalse(OnlineMarks.TryRate(true, true));
            Assert.IsFalse(OnlineMarks.TryRead("留言", false, "你好", out read));

            LogAssert.Expect(LogType.Error, "留言: body 未填");
            Assert.IsFalse(OnlineMarks.TryRead("留言", true, "", out read));

            var remnant = new GameObject("remnant");
            var rootObject = new GameObject("marks");
            var root = rootObject.AddComponent<OnlineMarksRoot>();
            var message = new GameObject("message");
            var blood = new GameObject("blood");
            message.transform.SetParent(rootObject.transform);
            blood.transform.SetParent(rootObject.transform);
            root.message = message;
            root.bloodstain = blood;

            root.online = false;
            root.Apply();
            Assert.IsFalse(message.activeSelf);
            Assert.IsFalse(blood.activeSelf);
            Assert.IsTrue(remnant.activeSelf);

            root.online = true;
            root.Apply();
            Assert.IsTrue(message.activeSelf);
            Assert.IsTrue(blood.activeSelf);
            Assert.IsTrue(remnant.activeSelf);
        }
    }
}
