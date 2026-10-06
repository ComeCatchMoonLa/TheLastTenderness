using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class HingedDoorTests
    {
        [Test]
        public void FrontOpens_BackDoesNot_RestKeepsTheFlag_SwingIsNinety()
        {
            Assert.IsTrue(HingedDoor.FromFront(Vector3.forward, Vector3.forward));
            Assert.IsFalse(HingedDoor.FromFront(Vector3.forward, Vector3.back));

            Assert.IsTrue(HingedDoor.TryOpen(false, true));
            Assert.IsFalse(HingedDoor.TryOpen(false, false));
            Assert.IsFalse(HingedDoor.TryOpen(true, false));

            Assert.IsTrue(HingedDoor.AfterRest(true));
            Assert.IsFalse(HingedDoor.AfterRest(false));

            Quaternion swung = HingedDoor.Swing(Quaternion.identity, true);
            Assert.AreEqual(90f, Quaternion.Angle(Quaternion.identity, swung), 0.01f);
            Assert.AreEqual(Quaternion.identity, HingedDoor.Swing(Quaternion.identity, false));
        }
    }
}
