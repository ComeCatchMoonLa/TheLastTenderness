using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SpellBuffTests
    {
        [Test]
        public void ForbiddenOrCannotBuff_LeavesNoExtra()
        {
            WeaponItem fire = ScriptableObject.CreateInstance<WeaponItem>();
            fire.infusion = "火";
            Assert.IsFalse(SpellBuff.TryCoat(fire, 30f, Extra(), out WeaponBuffState denied));
            Assert.IsFalse(denied.active);

            WeaponItem blocked = ScriptableObject.CreateInstance<WeaponItem>();
            blocked.cannotBuff = true;
            Assert.IsFalse(SpellBuff.TryCoat(blocked, 30f, Extra(), out WeaponBuffState none));
            Assert.IsFalse(none.active);

            Object.DestroyImmediate(fire);
            Object.DestroyImmediate(blocked);
        }

        [Test]
        public void AllowedInfusion_Coats()
        {
            WeaponItem heavy = ScriptableObject.CreateInstance<WeaponItem>();
            heavy.infusion = "重";
            Assert.IsTrue(SpellBuff.TryCoat(heavy, 30f, Extra(), out WeaponBuffState coated));
            Assert.IsTrue(coated.active);
            Assert.AreEqual(5, coated.extra.Fire);
            Object.DestroyImmediate(heavy);
        }

        static DamageSegments Extra()
        {
            DamageSegments extra = default;
            extra.Fire = 5;
            return extra;
        }
    }
}
