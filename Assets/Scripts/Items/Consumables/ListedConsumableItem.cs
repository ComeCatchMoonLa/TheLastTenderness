using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Consumable/Listed")]
    public class ListedConsumableItem : ConsumableItem
    {
        public ListedConsumable kind;

        public override void AttemptToConsumableItem(PlayerManager player)
        {
            if (player == null || player.pStats == null || player.pInventory == null || player.pAnimator == null) return;
            if (player.pCombat != null && player.pCombat.isUsingConsumable) return;
            player.pInventory.consumableBeingUsed = this;
            player.pAnimator.PlayTargetAnimation(consumeAnimation, isInteracting, new AnimationOptions { CanRotate = true });
        }

        public override void SuccessfullyUsedConsumable(PlayerManager player)
        {
            if (player == null || player.pStats == null || player.pInventory == null) return;
            if (!ConsumableEffect.Apply(kind, player.pStats, Time.time)) return;
            player.pInventory.TrySpendConsumable(this);
        }
    }
}
