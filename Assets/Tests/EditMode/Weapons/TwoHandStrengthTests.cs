using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class TwoHandStrengthTests
    {
        [Test]
        public void TwoHand_RaisesStrength_AndCapsAt99()
        {
            Assert.AreEqual(10, TwoHandStrength.ForNeed(10, false));
            Assert.AreEqual(15, TwoHandStrength.ForNeed(10, true));
            Assert.AreEqual(99, TwoHandStrength.ForNeed(66, true));
            Assert.AreEqual(99, TwoHandStrength.ForNeed(67, true));
        }

        [Test]
        public void Requirement_UsesRaisedStrength_OtherStatsStillShort()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.strengthNeedFilled = true;
            weapon.strengthNeed = 14;

            Assert.IsTrue(RequirementPenalty.TryScale("刀", weapon, 10, 10, 10, 10, 10, 0.5f, 0.5f, 10, false, out float speed, out float scaled));
            Assert.AreEqual(0.5f, speed);
            Assert.AreEqual(5f, scaled);

            Assert.IsTrue(RequirementPenalty.TryScale("刀", weapon, 10, 10, 10, 10, 10, 0.5f, 0.5f, 10, true, out speed, out scaled));
            Assert.AreEqual(1f, speed);
            Assert.AreEqual(10f, scaled);

            weapon.strengthNeed = 99;
            Assert.IsTrue(RequirementPenalty.TryScale("刀", weapon, 66, 10, 10, 10, 10, 0.5f, 0.5f, 10, true, out speed, out scaled));
            Assert.AreEqual(1f, speed);
            Assert.AreEqual(10f, scaled);

            weapon.strengthNeed = 14;
            weapon.dexterityNeedFilled = true;
            weapon.dexterityNeed = 20;
            Assert.IsTrue(RequirementPenalty.TryScale("刀", weapon, 10, 10, 10, 10, 10, 0.5f, 0.5f, 10, true, out speed, out scaled));
            Assert.AreEqual(0.5f, speed);
            Assert.AreEqual(5f, scaled);

            Object.DestroyImmediate(weapon);
        }
    }
}
