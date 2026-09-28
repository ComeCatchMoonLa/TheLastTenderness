using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class PhaseDamageMultiplierTests
    {
        [Test]
        public void BothHands_ShareFourPhases_IdleAndCriticalStayOne()
        {
            var weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.laFirstPhaseDM = 1.1f;
            weapon.laSecondPhaseDM = 1.2f;
            weapon.haFirstPhaseDM = 1.6f;
            weapon.haSecondPhasDM = 1.8f;

            AssertPhases(true, false, weapon);
            AssertPhases(false, true, weapon);
            Assert.AreEqual(1f, DamageCollider.PhaseDamageMultiplier(false, false, weapon, weapon, AttackType.light_1));
            Assert.AreEqual(1f, DamageCollider.PhaseDamageMultiplier(true, false, weapon, weapon, AttackType.critical));

            Object.DestroyImmediate(weapon);
        }

        static void AssertPhases(bool usingRightHand, bool usingLeftHand, WeaponItem weapon)
        {
            Assert.AreEqual(weapon.laFirstPhaseDM, DamageCollider.PhaseDamageMultiplier(usingRightHand, usingLeftHand, weapon, weapon, AttackType.light_1));
            Assert.AreEqual(weapon.laSecondPhaseDM, DamageCollider.PhaseDamageMultiplier(usingRightHand, usingLeftHand, weapon, weapon, AttackType.light_2));
            Assert.AreEqual(weapon.haFirstPhaseDM, DamageCollider.PhaseDamageMultiplier(usingRightHand, usingLeftHand, weapon, weapon, AttackType.heavy_1));
            Assert.AreEqual(weapon.haSecondPhasDM, DamageCollider.PhaseDamageMultiplier(usingRightHand, usingLeftHand, weapon, weapon, AttackType.heavy_2));
        }
    }
}
