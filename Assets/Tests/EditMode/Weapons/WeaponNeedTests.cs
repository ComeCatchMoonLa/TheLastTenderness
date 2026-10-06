using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class WeaponNeedTests
    {
        [Test]
        public void FilledNeed_ReadsTheNumber_EmptyIsNotZero()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.strengthNeedFilled = true;
            weapon.strengthNeed = 14;
            Assert.IsTrue(WeaponNeed.TryStrength(weapon, out int strength));
            Assert.AreEqual(14, strength);

            weapon.dexterityNeedFilled = false;
            weapon.dexterityNeed = 0;
            Assert.IsFalse(WeaponNeed.TryDexterity(weapon, out int dexterity));

            weapon.strengthNeedFilled = true;
            weapon.strengthNeed = 0;
            Assert.IsTrue(WeaponNeed.TryStrength(weapon, out strength));
            Assert.AreEqual(0, strength);
        }
    }
}
