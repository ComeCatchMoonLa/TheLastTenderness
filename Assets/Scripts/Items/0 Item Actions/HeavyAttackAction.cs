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
                HandleHeavyAttackCombo(character);
                character.cEffects.PlayWeaponTrialFX();
            }
            else
            {
                if (character.isInteracting) return;

                HandleHeavyAttack(character);
                character.cEffects.PlayWeaponTrialFX();
            }
        }

        void HandleHeavyAttack(CharacterManager character)
        {
            if (character.isUsingRightHand)
            {
                if (character.isTwoHandingWeapon)
                {
                    character.cAnimator.PlayTargetAnimation(character.cAnimator.thHeavyAttack1, isInteracting: true, mirrorAnim: false);
                    character.cAnimator.lastAttack = character.cAnimator.thHeavyAttack1;
                }
                else
                {
                    character.cAnimator.PlayTargetAnimation(character.cAnimator.ohHeavyAttack1, isInteracting: true, mirrorAnim: false);
                    character.cAnimator.lastAttack = character.cAnimator.ohHeavyAttack1;
                }
            }
            else if (character.isUsingLeftHand)
            {
                character.cAnimator.PlayTargetAnimation(character.cAnimator.ohHeavyAttack1, isInteracting: true, mirrorAnim: true);
                character.cAnimator.lastAttack = character.cAnimator.ohHeavyAttack1;
            }

            character.cCombat.attackType = AttackType.heavy_1;
        }

        void HandleHeavyAttackCombo(CharacterManager character)
        {
            if (character.canDoCombo)
            {
                character.canDoCombo = false;

                if (character.isUsingRightHand)
                {
                    if (character.isTwoHandingWeapon)
                    {
                        if (character.cAnimator.lastAttack == character.cAnimator.thHeavyAttack1)
                        {
                            character.cAnimator.PlayTargetAnimation(character.cAnimator.thHeavyAttack2, isInteracting: true, mirrorAnim: false);
                            character.cAnimator.lastAttack = character.cAnimator.thHeavyAttack2;
                            character.cCombat.attackType = AttackType.heavy_2;
                        }
                        else
                        {
                            character.cAnimator.PlayTargetAnimation(character.cAnimator.thHeavyAttack1, isInteracting: true, mirrorAnim: false);
                            character.cAnimator.lastAttack = character.cAnimator.thHeavyAttack1;
                            character.cCombat.attackType = AttackType.heavy_1;
                        }
                    }
                    else
                    {
                        if (character.cAnimator.lastAttack == character.cAnimator.ohHeavyAttack1)
                        {
                            character.cAnimator.PlayTargetAnimation(character.cAnimator.ohHeavyAttack2, isInteracting: true, mirrorAnim: false);
                            character.cAnimator.lastAttack = character.cAnimator.ohHeavyAttack2;
                            character.cCombat.attackType = AttackType.heavy_2;
                        }
                        else
                        {
                            character.cAnimator.PlayTargetAnimation(character.cAnimator.ohHeavyAttack1, isInteracting: true, mirrorAnim: false);
                            character.cAnimator.lastAttack = character.cAnimator.ohHeavyAttack1;
                            character.cCombat.attackType = AttackType.heavy_1;
                        }
                    }
                }
                else if (character.isUsingLeftHand)
                {
                    if (character.cAnimator.lastAttack == character.cAnimator.ohHeavyAttack1)
                    {
                        character.cAnimator.PlayTargetAnimation(character.cAnimator.ohHeavyAttack2, isInteracting: true, mirrorAnim: true);
                        character.cAnimator.lastAttack = character.cAnimator.ohHeavyAttack2;
                        character.cCombat.attackType = AttackType.heavy_2;
                    }
                    else
                    {
                        character.cAnimator.PlayTargetAnimation(character.cAnimator.ohHeavyAttack1, isInteracting: true, mirrorAnim: true);
                        character.cAnimator.lastAttack = character.cAnimator.ohHeavyAttack1;
                        character.cCombat.attackType = AttackType.heavy_1;
                    }
                }
            }
        }
    }
}