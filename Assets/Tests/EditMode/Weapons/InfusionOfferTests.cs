using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class InfusionOfferTests
    {
        [Test]
        public void LockedGroup_Stays_UntilThatCoalIsHandedIn()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.infusion = "甲";
            weapon.reinforceLevel = 3;

            Assert.IsTrue(InfusionOffer.CanSelect("", null));
            Assert.IsFalse(InfusionOffer.CanSelect("甲炭", new string[0]));
            Assert.AreEqual("甲", weapon.infusion);
            Assert.AreEqual(3, weapon.reinforceLevel);

            string[] handed = { "甲炭" };
            Assert.IsTrue(InfusionOffer.CanSelect("甲炭", handed));
            InfusionSwap.Replace(weapon, "乙");
            Assert.AreEqual("乙", weapon.infusion);
            Assert.AreEqual(3, weapon.reinforceLevel);
        }
    }
}
