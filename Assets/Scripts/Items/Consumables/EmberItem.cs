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
            player.pInventory.consumableBeingUsed = this;
            player.pAnimator.PlayTargetAnimation(consumeAnimation, isInteracting);
        }

        public override void SuccessfullyUsedConsumable(PlayerManager player)
        {
            if (player == null || player.pStats == null) return;
            player.pStats.ApplyEmber(healthRatio, name);
        }
    }
}
