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

            player.aimingMode = true;
            player.aimHeld = 0f;
            if (player.isInteracting)
                player.ClearAimingMode();
        }
    }
}
