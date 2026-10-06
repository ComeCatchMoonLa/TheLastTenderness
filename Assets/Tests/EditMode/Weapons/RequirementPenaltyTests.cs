using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class RequirementPenaltyTests
    {
        [Test]
        public void ShortNeed_ScalesDown_MetNeed_Stays_EmptyFactor_Stops()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.strengthNeedFilled = true;
            weapon.strengthNeed = 14;

            Assert.IsTrue(RequirementPenalty.TryScale("刀", weapon, 10, 10, 0.5f, 0.5f, 10, out float speed, out float scaled));
            Assert.AreEqual(0.5f, speed);
            Assert.AreEqual(5f, scaled);

            Assert.IsTrue(RequirementPenalty.TryScale("刀", weapon, 14, 10, 0.5f, 0.5f, 10, out speed, out scaled));
            Assert.AreEqual(1f, speed);
            Assert.AreEqual(10f, scaled);

            LogAssert.Expect(LogType.Error, "刀: speedFactor 未填");
            Assert.IsFalse(RequirementPenalty.TryScale("刀", weapon, 10, 10, 0f, 0.5f, 10, out speed, out scaled));
            Assert.AreEqual(1f, speed);
            Assert.AreEqual(10f, scaled);
        }
    }
}
