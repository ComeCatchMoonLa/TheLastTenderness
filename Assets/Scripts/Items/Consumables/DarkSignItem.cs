using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Consumable/Dark Sign")]
    public class DarkSignItem : ConsumableItem
    {
        public override void AttemptToConsumableItem(PlayerManager player)
        {
            if (player == null || player.pCombat == null || player.pCombat.isUsingConsumable) return;
            if (player.pStats == null || player.pInventory == null || player.pAnimator == null) return;
            if (BossFightGate.Active() || player.pStats.soulCount <= 0 || string.IsNullOrEmpty(LastBonfire.recorded))
                return;
            player.pInventory.consumableBeingUsed = this;
            player.pAnimator.PlayTargetAnimation(consumeAnimation, isInteracting);
        }

        public override void SuccessfullyUsedConsumable(PlayerManager player)
        {
            if (player == null || player.pStats == null) return;
            DarkSignState state = new DarkSignState
            {
                bossFight = BossFightGate.Active(),
                souls = player.pStats.soulCount,
                lastFire = LastBonfire.recorded,
                hadRemnant = player.pStats.hasSoulRemnant,
                remnant = player.pStats.soulRemnant
            };
            if (!DarkSign.TryUse(state)) return;
            player.pStats.SetSouls(state.souls);
            MoveToFire(player, state.place);
        }

        static void MoveToFire(PlayerManager player, string place)
        {
            CampFireInteractable[] fires = Object.FindObjectsByType<CampFireInteractable>(FindObjectsInactive.Include);
            for (int i = 0; i < fires.Length; i++)
            {
                if (fires[i] != null && fires[i].name == place)
                {
                    player.transform.position = fires[i].transform.position;
                    return;
                }
            }
        }
    }
}
