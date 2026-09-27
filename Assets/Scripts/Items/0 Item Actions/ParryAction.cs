using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Parry Action")]
    public class ParryAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character)
        {
            if (character.isInteracting) return;

            if (character.isTwoHandingWeapon)
            {
                // 播放双手拿武器的动画
            }
            else
            {
                character.cAnimator.PlayTargetAnimation(character.cAnimator.weapon_art, true);
            }
        }
    }
}