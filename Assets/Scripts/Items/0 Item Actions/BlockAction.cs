using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Block Action")]
    public class BlockAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character)
        {
            if (character.isInteracting || character.cCombat.isBlocking || character.cStats.currentStamina <= 0) return;

            if (character.cInventory != null)
            {
                WeaponItem right = character.cInventory.rightWeapon;
                WeaponItem left = character.cInventory.leftWeapon;
                WeaponType rightType = right != null ? right.weaponType : WeaponType.unarmed;
                WeaponType leftType = left != null ? left.weaponType : WeaponType.unarmed;
                if (PairedWeapons.ShieldBlocked(rightType, leftType)) return;
            }

            character.cCombat.isBlocking = true;
            character.cCombat.SetBlockingAbsorptionFromBlockingWeapon();
            character.cAnimator.PlayTargetAnimation("Block - Start", false, new AnimationOptions { CanRotate = true });
        }
    }
}