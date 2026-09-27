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
                character.cAnimator.ohLightAttack1,
                character.cAnimator.ohLightAttack2,
                character.cAnimator.thLightAttack1,
                character.cAnimator.thLightAttack2,
                AttackType.light_1,
                AttackType.light_2);
        }
    }
}
