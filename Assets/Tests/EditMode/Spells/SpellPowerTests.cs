using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SpellPowerTests
    {
        [Test]
        public void TryRead_ReturnsTheFilledNumber_EmptyIsNotZero()
        {
            WeaponItem catalyst = ScriptableObject.CreateInstance<WeaponItem>();
            catalyst.spellPowerFilled = true;
            catalyst.spellPower = 12f;
            Assert.IsTrue(SpellPower.TryRead(catalyst, out float power));
            Assert.AreEqual(12f, power);

            catalyst.spellPowerFilled = false;
            catalyst.spellPower = 0f;
            Assert.IsFalse(SpellPower.TryRead(catalyst, out _));

            catalyst.spellPowerFilled = true;
            catalyst.spellPower = 0f;
            Assert.IsTrue(SpellPower.TryRead(catalyst, out power));
            Assert.AreEqual(0f, power);
        }
    }
}
