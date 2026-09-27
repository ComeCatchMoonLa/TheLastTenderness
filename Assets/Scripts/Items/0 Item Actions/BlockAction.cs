using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Block Action")]
    public class BlockAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character)
        {
            if (character.isInteracting || character.cCombat.isBlocking || character.cStats.currentStamina <= 0) return;

            // ´ò¶Ï³å´Ì
            character.isSprinting = false;

            character.cCombat.isBlocking = true;
            character.cCombat.SetBlockingAbsorptionFromBlockingWeapon();
            character.cAnimator.PlayTargetAnimation("Block - Start", false, true);
        }
    }
}