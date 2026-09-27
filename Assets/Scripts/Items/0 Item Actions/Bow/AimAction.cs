using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Aim Action")]
    public class AimAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character)
        {
            if (character.characterType == CharacterType.player)
                PerformActionForPlayer(character as PlayerManager);
        }

        void PerformActionForPlayer(PlayerManager player)
        {
            if (!player.isTwoHandingWeapon) return;

            // 没有箭时不能瞄准
            if (player.pInventory.currentAmmo == null)
            {
                player.aimingMode = false;
                if (!player.isInteracting)
                    player.pAnimator.PlayTargetAnimation("Shrug", true);
                return;
            }

            player.aimingMode = true;
            // 交互打断瞄准
            if (player.isInteracting)
                player.aimingMode = false;
        }
    }
}