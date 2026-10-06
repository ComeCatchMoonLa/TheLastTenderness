using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Light Attack Action")]
    public class LightAttackAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character)
        {
            if (character.cStats.currentStamina <= 0) return;

            PlayerManager player = character as PlayerManager;
            bool movingForward = player != null && player.input != null && player.input.vertical > 0f;
            character.kickArmed = ShieldKick.IsKick(character.isSprinting, movingForward);

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
                    OneHandFirst = character.cAnimator.ohLightAttack1,
                    OneHandSecond = character.cAnimator.ohLightAttack2,
                    TwoHandFirst = character.cAnimator.thLightAttack1,
                    TwoHandSecond = character.cAnimator.thLightAttack2
                },
                AttackType.light_1,
                AttackType.light_2);
        }
    }
}
