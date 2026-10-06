using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ReinforceCapTests
    {
        [Test]
        public void NormalStopsAtTen_ScaleAndTwinklingStopAtFive()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.reinforceLevel = 9;
            Assert.IsTrue(ReinforceCap.TryRaise(weapon, ReinforceCap.Normal));
            Assert.AreEqual(10, weapon.reinforceLevel);
            Assert.IsFalse(ReinforceCap.TryRaise(weapon, ReinforceCap.Normal));
            Assert.AreEqual(10, weapon.reinforceLevel);

            weapon.reinforceLevel = 4;
            Assert.IsTrue(ReinforceCap.TryRaise(weapon, ReinforceCap.Scale));
            Assert.AreEqual(5, weapon.reinforceLevel);
            Assert.IsFalse(ReinforceCap.TryRaise(weapon, ReinforceCap.Scale));

            weapon.reinforceLevel = 5;
            Assert.IsFalse(ReinforceCap.TryRaise(weapon, ReinforceCap.Twinkling));
            Assert.AreEqual(5, weapon.reinforceLevel);

            Object.DestroyImmediate(weapon);
        }
    }
}
