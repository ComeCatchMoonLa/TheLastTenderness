using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Consumable/Ember")]
    public class EmberItem : ConsumableItem
    {
        public float healthRatio;

        public override void AttemptToConsumableItem(PlayerManager player)
        {
            if (healthRatio <= 0f)
            {
                Debug.LogError($"{name}: healthRatio 未填");
                return;
            }
            if (player == null || player.pCombat == null || player.pCombat.isUsingConsumable) return;
            if (player.pStats == null || player.pAnimator == null || player.pInventory == null) return;
            if (player.pStats.emberLit) return;
            player.pInventory.consumableBeingUsed = this;
            player.pAnimator.PlayTargetAnimation(consumeAnimation, isInteracting, new AnimationOptions { CanRotate = true });
        }

        public override void SuccessfullyUsedConsumable(PlayerManager player)
        {
            if (player == null || player.pStats == null || player.pInventory == null) return;
            if (!player.pStats.ApplyEmber(healthRatio, name)) return;
            player.pInventory.TrySpendConsumable(this);
        }
    }
}
