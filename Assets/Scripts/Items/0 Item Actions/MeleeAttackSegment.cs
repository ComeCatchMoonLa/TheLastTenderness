using UnityEngine;

namespace CatchMoon
{
    public static class MeleeAttackSegment
    {
        public static void Play(
            CharacterManager character,
            bool combo,
            string oneHandFirst,
            string oneHandSecond,
            string twoHandFirst,
            string twoHandSecond,
            AttackType firstType,
            AttackType secondType)
        {
            if (!character.isUsingRightHand && !character.isUsingLeftHand)
            {
                if (!combo)
                    character.cCombat.attackType = firstType;
                return;
            }

            string first = oneHandFirst;
            string second = oneHandSecond;
            bool mirror = character.isUsingLeftHand;
            if (character.isUsingRightHand && character.isTwoHandingWeapon)
            {
                first = twoHandFirst;
                second = twoHandSecond;
                mirror = false;
            }

            bool playSecond = combo && character.cAnimator.lastAttack == first;
            string anim = playSecond ? second : first;
            character.cAnimator.PlayTargetAnimation(anim, isInteracting: true, mirrorAnim: mirror);
            character.cAnimator.lastAttack = anim;
            character.cCombat.attackType = playSecond ? secondType : firstType;
        }
    }
}
