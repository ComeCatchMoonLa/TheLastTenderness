using System.Reflection;
using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class PendingCriticalDamageRemovalTests
    {
        [Test]
        public void SecondCriticalEntry_IsGone()
        {
            Assert.IsNull(typeof(CharacterAnimatorManager).GetMethod("ApplyPendingDamage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
            Assert.IsNull(typeof(CharacterCombatManager).GetField("pendingCriticalDamage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
        }
    }
}
