using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class ElevatorTests
    {
        [Test]
        public void Pull_GoesAwayThenHome_SameFloorsStay()
        {
            Vector3 home = new Vector3(0f, 0f, 0f);
            Vector3 away = new Vector3(0f, 5f, 0f);

            Assert.IsTrue(Elevator.Next("电梯", home, away, true, out Vector3 position, out bool goingAway));
            Assert.AreEqual(away, position);
            Assert.IsFalse(goingAway);

            Assert.IsTrue(Elevator.Next("电梯", home, away, goingAway, out position, out goingAway));
            Assert.AreEqual(home, position);
            Assert.IsTrue(goingAway);

            LogAssert.Expect(LogType.Error, "电梯: away 未填");
            Assert.IsFalse(Elevator.Next("电梯", home, home, true, out position, out goingAway));
            Assert.AreEqual(home, position);
            Assert.IsTrue(goingAway);
        }
    }
}
