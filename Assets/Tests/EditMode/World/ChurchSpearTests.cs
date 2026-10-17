using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class ChurchSpearTests
    {
        [Test]
        public void Offline_UsesTheLocalFighter_OnlineDoesNot_EmptyLogs()
        {
            GameObject body = new GameObject("枪兵");
            EnemyManager local = body.AddComponent<EnemyManager>();

            Assert.AreSame(local, ChurchSpear.Opponent(true, local, "教堂枪兵"));
            Assert.IsNull(ChurchSpear.Opponent(false, local, "教堂枪兵"));

            LogAssert.Expect(LogType.Error, "教堂枪兵: local 未填");
            Assert.IsNull(ChurchSpear.Opponent(true, null, "教堂枪兵"));

            Object.DestroyImmediate(body);
        }
    }
}
