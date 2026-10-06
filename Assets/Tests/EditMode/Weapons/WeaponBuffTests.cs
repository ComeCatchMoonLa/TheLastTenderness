using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class WeaponBuffTests
    {
        [Test]
        public void Allowed_AddsUntilTime_SwapStoreAndReapplyClear_FireFollows()
        {
            var heavy = ScriptableObject.CreateInstance<WeaponItem>();
            heavy.infusion = "重";
            var fire = ScriptableObject.CreateInstance<WeaponItem>();
            fire.infusion = "火";
            var plain = ScriptableObject.CreateInstance<WeaponItem>();
            plain.cannotBuff = true;
            var other = ScriptableObject.CreateInstance<WeaponItem>();

            Assert.IsTrue(WeaponBuff.CanApply("重", false));
            Assert.IsTrue(WeaponBuff.CanApply("", false));
            Assert.IsFalse(WeaponBuff.CanApply("火", false));
            Assert.IsFalse(WeaponBuff.CanApply("", true));

            DamageSegments extra = new DamageSegments { Fire = 5f };
            Assert.IsTrue(WeaponBuff.TryApply("附魔", heavy.infusion, heavy.cannotBuff, 2f, extra, heavy, out WeaponBuffState state));
            DamageSegments hit = WeaponBuff.Add(new DamageSegments { Physical = 10f }, state);
            Assert.AreEqual(5f, hit.Fire, 0.001f);

            state = WeaponBuff.Tick(state, 1f);
            hit = WeaponBuff.Add(new DamageSegments { Physical = 10f }, state);
            Assert.IsTrue(state.active);
            Assert.AreEqual(5f, hit.Fire, 0.001f);

            state = WeaponBuff.Tick(state, 1f);
            hit = WeaponBuff.Add(new DamageSegments { Physical = 10f }, state);
            Assert.IsFalse(state.active);
            Assert.AreEqual(0f, hit.Fire, 0.001f);

            Assert.IsTrue(WeaponBuff.TryApply("附魔", heavy.infusion, false, 2f, extra, heavy, out state));
            state = WeaponBuff.ClearIfUnequipped(state, other, null);
            Assert.IsFalse(state.active);

            Assert.IsTrue(WeaponBuff.TryApply("附魔", heavy.infusion, false, 2f, extra, heavy, out state));
            state = WeaponBuff.ClearIfStored(state, heavy, true);
            Assert.IsFalse(state.active);

            DamageSegments replaced = new DamageSegments { Fire = 8f };
            Assert.IsTrue(WeaponBuff.TryApply("附魔", heavy.infusion, false, 2f, extra, heavy, out state));
            Assert.IsTrue(WeaponBuff.TryApply("附魔", heavy.infusion, false, 2f, replaced, heavy, out state));
            hit = WeaponBuff.Add(new DamageSegments(), state);
            Assert.AreEqual(8f, hit.Fire, 0.001f);

            Assert.IsFalse(WeaponBuff.TryApply("附魔", fire.infusion, false, 2f, extra, fire, out WeaponBuffState denied));
            Assert.IsFalse(denied.active);
            Assert.IsFalse(WeaponBuff.TryApply("附魔", plain.infusion, plain.cannotBuff, 2f, extra, plain, out denied));

            LogAssert.Expect(LogType.Error, "附魔: duration 未填");
            Assert.IsFalse(WeaponBuff.TryApply("附魔", "重", false, 0f, extra, heavy, out denied));

            var root = new GameObject("sword");
            var fx = new GameObject("FX_Sword_Fire");
            fx.transform.SetParent(root.transform);
            fx.SetActive(false);
            GameObject found = WeaponBuff.FindFire(root.transform);
            WeaponBuff.ShowFire(found, true);
            Assert.IsTrue(found.activeSelf);
            WeaponBuff.ShowFire(found, false);
            Assert.IsFalse(found.activeSelf);
        }
    }
}
