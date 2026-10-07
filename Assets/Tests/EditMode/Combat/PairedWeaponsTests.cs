using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class PairedWeaponsTests
    {
        [Test]
        public void Paired_UsesTwoHandSegment_ShieldDoesNotRise_NormalsStayOneHand()
        {
            Assert.IsTrue(PairedWeapons.AsTwoHand(WeaponType.melee_OH_DualWield, false));
            Assert.IsFalse(PairedWeapons.AsTwoHand(WeaponType.melee_OH_RH, false));
            Assert.IsTrue(PairedWeapons.AsTwoHand(WeaponType.melee_OH_RH, true));

            MeleeAnimations animations = new MeleeAnimations
            {
                OneHandFirst = "单手",
                TwoHandFirst = "双手"
            };
            MeleeAttackSegment.Select(true, false, true, false, null, animations, AttackType.light_1, AttackType.light_2, out string paired, out bool mirror, out _);
            Assert.AreEqual("双手", paired);
            Assert.IsFalse(mirror);

            MeleeAttackSegment.Select(true, false, false, false, null, animations, AttackType.light_1, AttackType.light_2, out string normal, out _, out _);
            Assert.AreEqual("单手", normal);

            Assert.IsTrue(PairedWeapons.ShieldBlocked(WeaponType.melee_OH_DualWield, WeaponType.melee_OH_Shield));
            Assert.IsFalse(PairedWeapons.ShieldBlocked(WeaponType.melee_OH_RH, WeaponType.melee_OH_Shield));
            Assert.IsFalse(PairedWeapons.ShieldBlocked(WeaponType.melee_OH_RH, WeaponType.melee_OH_LH));
        }
    }
}
