using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class MeleeAttackSegmentTests
    {
        static readonly MeleeAnimations Light = new MeleeAnimations
        {
            OneHandFirst = "oh1",
            OneHandSecond = "oh2",
            TwoHandFirst = "th1",
            TwoHandSecond = "th2"
        };

        [Test]
        public void RightHand_PlaysOneHandFirst()
        {
            Select(usingRightHand: true, usingLeftHand: false, twoHanding: false, combo: false, lastAttack: null, out string anim, out bool mirror, out AttackType attackType);
            Assert.AreEqual("oh1", anim);
            Assert.IsFalse(mirror);
            Assert.AreEqual(AttackType.light_1, attackType);
        }

        [Test]
        public void LeftHand_MirrorsOneHandFirst()
        {
            Select(usingRightHand: false, usingLeftHand: true, twoHanding: false, combo: false, lastAttack: null, out string anim, out bool mirror, out AttackType attackType);
            Assert.AreEqual("oh1", anim);
            Assert.IsTrue(mirror);
            Assert.AreEqual(AttackType.light_1, attackType);
        }

        [Test]
        public void TwoHandRight_PlaysTwoHandFirstWithoutMirror()
        {
            Select(usingRightHand: true, usingLeftHand: false, twoHanding: true, combo: false, lastAttack: null, out string anim, out bool mirror, out AttackType attackType);
            Assert.AreEqual("th1", anim);
            Assert.IsFalse(mirror);
            Assert.AreEqual(AttackType.light_1, attackType);
        }

        [Test]
        public void ComboAfterFirst_PlaysSecond()
        {
            Select(usingRightHand: true, usingLeftHand: false, twoHanding: false, combo: true, lastAttack: "oh1", out string anim, out bool mirror, out AttackType attackType);
            Assert.AreEqual("oh2", anim);
            Assert.IsFalse(mirror);
            Assert.AreEqual(AttackType.light_2, attackType);
        }

        [Test]
        public void ComboAfterOther_StaysOnFirst()
        {
            Select(usingRightHand: true, usingLeftHand: false, twoHanding: false, combo: true, lastAttack: "other", out string anim, out bool mirror, out AttackType attackType);
            Assert.AreEqual("oh1", anim);
            Assert.IsFalse(mirror);
            Assert.AreEqual(AttackType.light_1, attackType);
        }

        static void Select(bool usingRightHand, bool usingLeftHand, bool twoHanding, bool combo, string lastAttack, out string anim, out bool mirror, out AttackType attackType)
        {
            MeleeAttackSegment.Select(usingRightHand, usingLeftHand, twoHanding, combo, lastAttack, Light, AttackType.light_1, AttackType.light_2, out anim, out mirror, out attackType);
        }
    }
}
