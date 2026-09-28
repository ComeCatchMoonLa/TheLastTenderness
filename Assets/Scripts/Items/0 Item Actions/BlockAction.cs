using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Block Action")]
    public class BlockAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character)
        {
            if (character.isInteracting || character.cCombat.isBlocking || character.cStats.currentStamina <= 0) return;

            character.cCombat.isBlocking = true;
            character.cCombat.SetBlockingAbsorptionFromBlockingWeapon();
            character.cAnimator.PlayTargetAnimation("Block - Start", false, new AnimationOptions { CanRotate = true });
        }
    }
}