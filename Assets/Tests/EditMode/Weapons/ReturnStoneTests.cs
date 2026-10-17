using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class ReturnStoneTests
    {
        [Test]
        public void MatchingStone_ClearsInfusion_AndKeepsLevel()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.infusion = "火";
            weapon.reinforceLevel = 4;
            Assert.IsTrue(ReturnStone.TryRevert("火石", "火", weapon));
            Assert.AreEqual(string.Empty, weapon.infusion);
            Assert.AreEqual(4, weapon.reinforceLevel);

            weapon.infusion = "火";
            Assert.IsFalse(ReturnStone.TryRevert("雷石", "雷", weapon));
            Assert.AreEqual("火", weapon.infusion);

            LogAssert.Expect(LogType.Error, "空石: infusion 未填");
            Assert.IsFalse(ReturnStone.TryRevert("空石", "", weapon));
            Assert.AreEqual("火", weapon.infusion);
        }

        [Test]
        public void Upgrade_SpendsFilledCount_UnfilledDoesNotRaise()
        {
            Assert.IsTrue(UpgradeSpend.Try("剑", true, 2, 5, 3, ReinforceCap.Normal, out int held, out int level));
            Assert.AreEqual(3, held);
            Assert.AreEqual(4, level);
            Assert.IsFalse(UpgradeSpend.Try("剑", true, 2, 1, 3, ReinforceCap.Normal, out held, out level));
            Assert.AreEqual(1, held);
            Assert.AreEqual(3, level);
            Assert.IsFalse(UpgradeSpend.Try("鳞", true, 1, 5, 5, ReinforceCap.Scale, out held, out level));
            Assert.AreEqual(5, level);

            LogAssert.Expect(LogType.Error, "剑: upgradeCount 未填");
            Assert.IsFalse(UpgradeSpend.Try("剑", false, 0, 5, 3, ReinforceCap.Normal, out held, out level));
            Assert.AreEqual(5, held);
            Assert.AreEqual(3, level);
        }

        [Test]
        public void BowShieldCatalystAndTorch_CannotInfuse()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.infusion = "无";
            weapon.weaponType = WeaponType.bow;
            Assert.IsFalse(InfusionSwap.Replace(weapon, "火"));
            Assert.AreEqual("无", weapon.infusion);

            weapon.weaponType = WeaponType.crossbow;
            Assert.IsFalse(InfusionSwap.Replace(weapon, "火"));
            weapon.weaponType = WeaponType.greatbow;
            Assert.IsFalse(InfusionSwap.Replace(weapon, "火"));
            weapon.weaponType = WeaponType.spellCaster;
            Assert.IsFalse(InfusionSwap.Replace(weapon, "火"));
            weapon.weaponType = WeaponType.faithCaster;
            Assert.IsFalse(InfusionSwap.Replace(weapon, "火"));
            weapon.weaponType = WeaponType.pyromancyCaster;
            Assert.IsFalse(InfusionSwap.Replace(weapon, "火"));
            weapon.weaponType = WeaponType.melee_OH_Shield;
            Assert.IsFalse(InfusionSwap.Replace(weapon, "火"));

            weapon.weaponType = WeaponType.melee_OH_RH;
            weapon.torch = true;
            Assert.IsFalse(InfusionSwap.Replace(weapon, "火"));
            Assert.AreEqual("无", weapon.infusion);

            weapon.torch = false;
            Assert.IsTrue(InfusionSwap.Replace(weapon, "火"));
            Assert.AreEqual("火", weapon.infusion);
        }
    }
}
