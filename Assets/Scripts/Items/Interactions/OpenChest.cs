using UnityEngine;

namespace CatchMoon
{
    public class OpenChest : Interactable
    {
        Animator animator;

        [SerializeField] Transform playerStandingPosition;
        public WeaponItem itemInChest;
        public bool opened;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            #region 检错
            if (animator == null)
            {
                Debug.LogError("");
                return;
            }
            #endregion
        }

        public override void Interact(PlayerManager playerManager)
        {
            if (itemInChest == null)
            {
                Debug.LogError($"{name}: itemInChest 未填");
                return;
            }
            if (opened) return;
            if (playerManager == null || playerManager.pInventory == null) return;

            playerManager.OpenChestInteracting(playerStandingPosition, transform.position);
            animator.Play("Chest Open");

            if (!ChestLoot.Give(false, itemInChest, playerManager.pInventory)) return;
            opened = true;
            gameObject.tag = "Untagged";

            if (playerManager.ui != null && playerManager.ui.popUps != null && playerManager.ui.popUps.interactUI != null)
            {
                playerManager.ui.popUps.interactUI.SetInteractionInfo(itemInChest);
                playerManager.ui.popUps.interactUI.PopUpInteractionInfoUI();
            }
        }

        public void AfterRest()
        {
            opened = ChestLoot.AfterRest(opened);
        }
    }
}
