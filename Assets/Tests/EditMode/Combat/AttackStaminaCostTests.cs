using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class AttackStaminaCostTests
    {
        [Test]
        public void LightAndHeavy_UseTheirMultipliers_CriticalIsZero()
        {
            var weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.baseStaminaCost = 2;
            weapon.laStaminaCostM = 3f;
            weapon.haStaminaCostM = 1.5f;

            Assert.AreEqual(6f, CharacterAnimatorManager.AttackStaminaCost(weapon, AttackType.light_1), 0.001f);
            Assert.AreEqual(6f, CharacterAnimatorManager.AttackStaminaCost(weapon, AttackType.light_2), 0.001f);
            Assert.AreEqual(3f, CharacterAnimatorManager.AttackStaminaCost(weapon, AttackType.heavy_1), 0.001f);
            Assert.AreEqual(3f, CharacterAnimatorManager.AttackStaminaCost(weapon, AttackType.heavy_2), 0.001f);
            Assert.AreEqual(0f, CharacterAnimatorManager.AttackStaminaCost(weapon, AttackType.critical), 0.001f);

            Object.DestroyImmediate(weapon);
        }
    }
}
