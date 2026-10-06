using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class CameraRigMotionTests
    {
        [Test]
        public void FollowPositions_MatchTheCurrentThreePoses()
        {
            Vector3 anchor = new Vector3(1f, 2f, 3f);
            Assert.AreEqual(anchor, CameraRigMotion.AimPosition(anchor));
            Assert.AreEqual(anchor, CameraRigMotion.LockPosition(anchor));

            Vector3 velocity = Vector3.zero;
            Vector3 current = Vector3.zero;
            Vector3 player = new Vector3(4f, 0f, 0f);
            Vector3 expected = Vector3.SmoothDamp(current, player, ref velocity, 0.2f);
            Vector3 again = Vector3.zero;
            Vector3 actual = CameraRigMotion.DefaultPosition(current, player, ref again, 0.2f);
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(velocity, again);
        }

        [Test]
        public void ForwardDistance_KeepsAimAndPullsInWhenBlocked()
        {
            Assert.AreEqual(1.2f, CameraRigMotion.ForwardDistance(true, 1.2f, 4f, true, 0.5f, 0.2f, 0.2f));
            Assert.AreEqual(4f, CameraRigMotion.ForwardDistance(false, 1.2f, 4f, false, 0f, 0.2f, 0.2f));
            Assert.AreEqual(1.8f, CameraRigMotion.ForwardDistance(false, 1.2f, 4f, true, 2f, 0.2f, 0.2f));
            Assert.AreEqual(0.2f, CameraRigMotion.ForwardDistance(false, 1.2f, 4f, true, 0.3f, 0.2f, 0.2f));
            Assert.AreEqual(0.2f, CameraRigMotion.ForwardDistance(false, 1.2f, 0.1f, false, 0f, 0.2f, 0.2f));
        }

        [Test]
        public void LocalBack_SitsBehindByTheDistance()
        {
            Assert.AreEqual(new Vector3(0f, 0f, -4f), CameraRigMotion.LocalBack(4f));
        }
    }
}
