using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class SpellBehaviorTests
    {
        [Test]
        public void SameLayerAndGroup_Replaces_OtherLayerStays_EmptyGroupStops()
        {
            var slots = new List<BodyBuff>();
            Assert.IsTrue(BodyBuffs.TryPut("铁身", slots, BodyBuffLayer.Spell, "护身", "铁身"));
            Assert.IsTrue(BodyBuffs.TryPut("深庇护", slots, BodyBuffLayer.Spell, "护身", "深庇护"));
            Assert.IsTrue(BodyBuffs.TryPut("绿花", slots, BodyBuffLayer.Consumable, "花", "绿花"));
            Assert.AreEqual(2, slots.Count);
            Assert.AreEqual("深庇护", slots[0].name);
            Assert.AreEqual("绿花", slots[1].name);

            LogAssert.Expect(LogType.Error, "空: group 未填");
            Assert.IsFalse(BodyBuffs.TryPut("空", slots, BodyBuffLayer.Spell, "", "空"));
            Assert.AreEqual(2, slots.Count);

            BodyBuffs.Clear(slots);
            Assert.AreEqual(0, slots.Count);
        }

        [Test]
        public void ThreeStrikes_CountAsThree_AreaUsesMaxHealth()
        {
            Assert.AreEqual(3, SpellBehavior.Strikes(3));
            Assert.AreEqual(1, SpellBehavior.Strikes(0));

            Assert.IsTrue(SpellBehavior.TryAreaDamage("雾", true, 0.1f, 100f, out float damage));
            Assert.AreEqual(10f, damage);

            LogAssert.Expect(LogType.Error, "雾: areaPercent 未填");
            Assert.IsFalse(SpellBehavior.TryAreaDamage("雾", false, 0.1f, 100f, out damage));
            Assert.AreEqual(0f, damage);
        }
    }
}
