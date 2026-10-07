using UnityEngine;

namespace CatchMoon
{
    public struct MeleeAnimations
    {
        public string OneHandFirst;
        public string OneHandSecond;
        public string TwoHandFirst;
        public string TwoHandSecond;
    }

    public static class MeleeAttackSegment
    {
        public static void Play(
            CharacterManager character,
            bool combo,
            MeleeAnimations animations,
            AttackType firstType,
            AttackType secondType)
        {
            if (!character.isUsingRightHand && !character.isUsingLeftHand)
            {
                if (!combo)
                    character.cCombat.attackType = firstType;
                return;
            }

            bool twoHand = character.isTwoHandingWeapon;
            if (character.cInventory != null)
            {
                WeaponItem weapon = character.isUsingRightHand ? character.cInventory.rightWeapon : character.cInventory.leftWeapon;
                if (weapon != null)
                    twoHand = PairedWeapons.AsTwoHand(weapon.weaponType, twoHand);
            }

            bool pairedTwoHand = twoHand && !character.isTwoHandingWeapon;
            Select(
                character.isUsingRightHand || pairedTwoHand,
                character.isUsingLeftHand && !pairedTwoHand,
                twoHand,
                combo,
                character.cAnimator.lastAttack,
                animations,
                firstType,
                secondType,
                out string anim,
                out bool mirror,
                out AttackType attackType);
            character.cAnimator.PlayTargetAnimation(anim, true, new AnimationOptions { Mirror = mirror });
            character.cAnimator.lastAttack = anim;
            character.cCombat.attackType = attackType;
        }

        public static void Select(
            bool usingRightHand,
            bool usingLeftHand,
            bool twoHanding,
            bool combo,
            string lastAttack,
            MeleeAnimations animations,
            AttackType firstType,
            AttackType secondType,
            out string anim,
            out bool mirror,
            out AttackType attackType)
        {
            string first = animations.OneHandFirst;
            string second = animations.OneHandSecond;
            mirror = usingLeftHand;
            if (usingRightHand && twoHanding)
            {
                first = animations.TwoHandFirst;
                second = animations.TwoHandSecond;
                mirror = false;
            }

            bool playSecond = combo && lastAttack == first;
            anim = playSecond ? second : first;
            attackType = playSecond ? secondType : firstType;
        }
    }
}
