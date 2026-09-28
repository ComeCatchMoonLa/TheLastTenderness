using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName ="Item/Consumable/Flask")]
    public class FlaskItem : ConsumableItem
    {
        [Header("Flask Type")]
        public FlaskType flaskType;

        [Header("Recovery Amount")]
        public int healthRecoverAmount;      // HP(血量)恢复量
        public int focusPointsRecoverAmount; // FP(精力值)恢复量

        [Header("Recovery FX")]
        public GameObject recoveryFX;

        public override void AttemptToConsumableItem(PlayerManager player)
        {
            if (player.pCombat.isUsingConsumable) return;

            if (player.pInventory.TrySpendConsumable(this))
            {
                player.pInventory.consumableBeingUsed = this;

                player.pAnimator.PlayTargetAnimation(consumeAnimation, isInteracting, true);

                GameObject flask = Instantiate(itemModel, player.pWeaponSlot.rightHandSlot.overrideParentWhileHolding);
                player.pEffects.currentParticleFX = recoveryFX;
                player.pEffects.instantiatedFXModel = flask;
                player.pWeaponSlot.rightHandSlot.UnloadWeapon();
            }
            else
            {
                player.pAnimator.PlayTargetAnimation("Shrug", true);
            }
        }
        public override void SuccessfullyUsedConsumable(PlayerManager player)
        {
            if (player.pInventory.TryGetFlaskSip(flaskType, out int amount))
            {
                if (flaskType == FlaskType.ashen)
                    player.pStats.AddMP(amount);
                else
                    player.pStats.AddHP(amount);
            }
            GameObject healParticles = Instantiate(player.pEffects.currentParticleFX, player.pStats.transform);
            Destroy(player.pEffects.instantiatedFXModel.gameObject);
            player.pEffects.instantiatedFXModel = null;
            player.pWeaponSlot.LoadWeaponsOnBothHands();
        }
    }
}