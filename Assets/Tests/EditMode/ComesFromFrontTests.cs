using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ComesFromFrontTests
    {
        [Test]
        public void FrontIsTrue_BehindAndOverlapAreFalse()
        {
            Vector3 defender = Vector3.zero;
            Vector3 forward = Vector3.forward;

            Assert.IsTrue(CharacterCombatManager.ComesFromFront(new Vector3(0f, 0f, 2f), defender, forward));
            Assert.IsFalse(CharacterCombatManager.ComesFromFront(new Vector3(0f, 0f, -2f), defender, forward));
            Assert.IsFalse(CharacterCombatManager.ComesFromFront(defender, defender, forward));
        }
    }
}
