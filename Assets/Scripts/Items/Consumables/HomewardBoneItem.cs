using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Consumable/Homeward Bone")]
    public class HomewardBoneItem : ConsumableItem
    {
        public override void AttemptToConsumableItem(PlayerManager player)
        {
            if (player == null || player.pCombat == null || player.pCombat.isUsingConsumable) return;
            if (player.pInventory == null || player.pAnimator == null) return;
            if (!HomewardBone.Allowed(BossBarVisible(), player.pInventory.ConsumableRemaining(this), LastBonfire.recorded))
                return;
            player.pInventory.consumableBeingUsed = this;
            player.pAnimator.PlayTargetAnimation(consumeAnimation, isInteracting);
        }

        public override void SuccessfullyUsedConsumable(PlayerManager player)
        {
            if (player == null || player.pStats == null || player.pInventory == null) return;
            int before = player.pInventory.ConsumableRemaining(this);
            HomewardState state = new HomewardState
            {
                bossBarVisible = BossBarVisible(),
                bones = before,
                lastFire = LastBonfire.recorded,
                souls = player.pStats.soulCount,
                emberLit = player.pStats.emberLit,
                hp = player.pStats.currentHP,
                maxHp = player.pStats.maxHP,
                mp = player.pStats.currentMP,
                maxMp = player.pStats.maxMP,
                estus = player.pInventory.estusLeft,
                estusShare = player.pInventory.estusShare,
                ash = player.pInventory.ashLeft,
                ashShare = player.pInventory.ashShare
            };
            if (!HomewardBone.TryUse(state)) return;

            player.pStats.currentHP = state.hp;
            player.pStats.currentMP = state.mp;
            player.pInventory.estusLeft = state.estus;
            player.pInventory.ashLeft = state.ash;
            WriteBars(player);

            int spent = before - state.bones;
            for (int i = 0; i < spent; i++)
                player.pInventory.TrySpendConsumable(this);

            if (state.resetEnemies)
                RestoreEnemies();
            MoveToFire(player, state.place);
        }

        static bool BossBarVisible()
        {
            return BossFightGate.Active();
        }

        static void WriteBars(PlayerManager player)
        {
            if (player.ui == null || player.ui.hud == null) return;
            if (player.ui.hud.healthBar != null)
                player.ui.hud.healthBar.SetCurrentHP(player.pStats.currentHP);
            if (player.ui.hud.focusPointsBar != null)
                player.ui.hud.focusPointsBar.SetCurrentMP(player.pStats.currentMP);
            if (player.ui.hud.quickSlotsUI != null)
                player.ui.hud.quickSlotsUI.SetFlaskCounts(player.pInventory.estusLeft, player.pInventory.ashLeft);
        }

        static void RestoreEnemies()
        {
            EnemyStatsManager[] resting = Object.FindObjectsByType<EnemyStatsManager>(FindObjectsInactive.Include);
            for (int i = 0; i < resting.Length; i++)
            {
                if (resting[i] != null)
                    resting[i].RestoreAfterRest();
            }
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
