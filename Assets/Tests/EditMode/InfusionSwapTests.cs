using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class InfusionSwapTests
    {
        [Test]
        public void Replace_ChangesTheKind_AndKeepsTheLevel()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.infusion = "甲";
            weapon.reinforceLevel = 3;
            InfusionSwap.Replace(weapon, "乙");
            Assert.AreEqual("乙", weapon.infusion);
            Assert.AreEqual(3, weapon.reinforceLevel);
        }
    }
}
