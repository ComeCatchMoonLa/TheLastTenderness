using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Light Attack Action")]
    public class LightAttackAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character)
        {
            if (character.cStats.currentStamina <= 0) return;

            if (character.canDoCombo)
            {
                HandleLightAttackCombo(character);
            }
            else
            {
                if (character.isInteracting) return;

                HandleLightAttack(character);
            }
            character.cEffects.PlayWeaponTrialFX();
        }

        void HandleLightAttack(CharacterManager character)
        {
            if (character.isUsingRightHand)
            {
                if (character.isTwoHandingWeapon)
                {
                    character.cAnimator.PlayTargetAnimation(character.cAnimator.thLightAttack1, isInteracting: true, mirrorAnim: false);
                    character.cAnimator.lastAttack = character.cAnimator.thLightAttack1;
                }
                else
                {
                    character.cAnimator.PlayTargetAnimation(character.cAnimator.ohLightAttack1, isInteracting: true, mirrorAnim: false);
                    character.cAnimator.lastAttack = character.cAnimator.ohLightAttack1;
                }
            }
            else if (character.isUsingLeftHand)
            {
                character.cAnimator.PlayTargetAnimation(character.cAnimator.ohLightAttack1, isInteracting: true, mirrorAnim: true);
                character.cAnimator.lastAttack = character.cAnimator.ohLightAttack1;
            }

            character.cCombat.attackType = AttackType.light_1;
        }

        void HandleLightAttackCombo(CharacterManager character)
        {
            character.canDoCombo = false;

            if (character.isUsingRightHand)
            {
                if (character.isTwoHandingWeapon)
                {
                    if (character.cAnimator.lastAttack == character.cAnimator.thLightAttack1)
                    {
                        character.cAnimator.PlayTargetAnimation(character.cAnimator.thLightAttack2, isInteracting: true, mirrorAnim: false);
                        character.cAnimator.lastAttack = character.cAnimator.thLightAttack2;
                        character.cCombat.attackType = AttackType.light_2;
                    }
                    else
                    {
                        character.cAnimator.PlayTargetAnimation(character.cAnimator.thLightAttack1, isInteracting: true, mirrorAnim: false);
                        character.cAnimator.lastAttack = character.cAnimator.thLightAttack1;
                        character.cCombat.attackType = AttackType.light_1;
                    }
                }
                else
                {
                    if (character.cAnimator.lastAttack == character.cAnimator.ohLightAttack1)
                    {
                        character.cAnimator.PlayTargetAnimation(character.cAnimator.ohLightAttack2, isInteracting: true, mirrorAnim: false);
                        character.cAnimator.lastAttack = character.cAnimator.ohLightAttack2;
                        character.cCombat.attackType = AttackType.light_2;
                    }
                    else
                    {
                        character.cAnimator.PlayTargetAnimation(character.cAnimator.ohLightAttack1, isInteracting: true, mirrorAnim: false);
                        character.cAnimator.lastAttack = character.cAnimator.ohLightAttack1;
                        character.cCombat.attackType = AttackType.light_1;
                    }
                }
            }
            else if (character.isUsingLeftHand)
            {
                if (character.cAnimator.lastAttack == character.cAnimator.ohLightAttack1)
                {
                    character.cAnimator.PlayTargetAnimation(character.cAnimator.ohLightAttack2, isInteracting: true, mirrorAnim: true);
                    character.cAnimator.lastAttack = character.cAnimator.ohLightAttack2;
                    character.cCombat.attackType = AttackType.light_2;
                }
                else
                {
                    character.cAnimator.PlayTargetAnimation(character.cAnimator.ohLightAttack1, isInteracting: true, mirrorAnim: true);
                    character.cAnimator.lastAttack = character.cAnimator.ohLightAttack1;
                    character.cCombat.attackType = AttackType.light_1;
                }
            }
        }
    }
}