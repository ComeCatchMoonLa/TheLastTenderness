using UnityEngine;

namespace CatchMoon
{
    public class OpenChest : Interactable
    {
        Animator animator;
        Transform lid;
        Vector3 closedLidPosition;
        Quaternion closedLidRotation;

        [SerializeField] Transform playerStandingPosition;
        public WeaponItem itemInChest;
        public bool opened;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            lid = transform.Find("Lid");
            if (lid != null)
            {
                closedLidPosition = lid.localPosition;
                closedLidRotation = lid.localRotation;
            }

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
            if (animator != null)
            {
                animator.enabled = true;
                animator.speed = 1f;
                animator.Play("Chest Open", 0, 0f);
            }

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

        public void CloseForNewCycle()
        {
            opened = ChestLoot.ForNewCycle(opened);
            gameObject.tag = "Interactable";
            if (lid != null)
            {
                lid.localPosition = closedLidPosition;
                lid.localRotation = closedLidRotation;
            }
            if (animator != null)
                animator.enabled = false;
        }
    }
}
