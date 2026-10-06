using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class EnemyRootMotionTests
    {
        [Test]
        public void WritesVelocityWhileInteractingOrInLocomotion()
        {
            int locomotion = Animator.StringToHash("Locomotion");
            int other = Animator.StringToHash("Other");
            Assert.IsTrue(EnemyAnimatorManager.WritesHorizontalRootVelocity(true, other));
            Assert.IsTrue(EnemyAnimatorManager.WritesHorizontalRootVelocity(false, locomotion));
            Assert.IsFalse(EnemyAnimatorManager.WritesHorizontalRootVelocity(false, other));
            Assert.AreEqual(locomotion, EnemyAnimatorManager.LocomotionState);
        }
    }
}
