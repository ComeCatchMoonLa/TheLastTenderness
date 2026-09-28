using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class CameraOffsetTargetTests
    {
        [Test]
        public void ThreeModes_MatchCurrentTargets()
        {
            Assert.AreEqual(new Vector3(-0.6f, 1.5f, 0f), PlayerCameraManager.AimingCameraOffsetTarget(0.6f, 1.5f));
            Assert.AreEqual(new Vector3(0f, 2.2f, 0f), PlayerCameraManager.LockedCameraOffsetTarget(2.2f));
            Assert.AreEqual(new Vector3(0f, 1.4f, 0f), PlayerCameraManager.DefaultCameraOffsetTarget(1.4f));
        }
    }
}
