using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Heavy Attack Action")]
    public class HeavyAttackAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character)
        {
            if (character.cStats.currentStamina <= 0) return;

            if (character.canDoCombo)
            {
                character.canDoCombo = false;
                PlaySegment(character, combo: true);
            }
            else
            {
                if (character.isInteracting) return;

                PlaySegment(character, combo: false);
            }
            character.cEffects.PlayWeaponTrialFX();
        }

        void PlaySegment(CharacterManager character, bool combo)
        {
            MeleeAttackSegment.Play(
                character,
                combo,
                new MeleeAnimations
                {
                    OneHandFirst = character.cAnimator.ohHeavyAttack1,
                    OneHandSecond = character.cAnimator.ohHeavyAttack2,
                    TwoHandFirst = character.cAnimator.thHeavyAttack1,
                    TwoHandSecond = character.cAnimator.thHeavyAttack2
                },
                AttackType.heavy_1,
                AttackType.heavy_2);
        }
    }
}
