using UnityEngine;

namespace CatchMoon
{
    public class PickUpItem : Interactable
    {
        public Item item; // 武器
        public bool taken;

        private void Start()
        {
            #region 检错
            if (item == null)
            {
                Debug.LogError("weapon is null.");
                return;
            }
            #endregion
        }

        /// <summary>
        /// 玩家交互[重写]
        /// </summary>
        public override void Interact(PlayerManager player)
        {
            // 拾起物品操作打断玩家移动
            player.rigidBody.linearVelocity = Vector3.zero;
            player.pAnimator.PlayTargetAnimation("Pick Up Item", true);
            player.pInventory.AddItem(item);
            player.ui.popUps.interactUI.SetInteractionInfo(item);

            player.ui.popUps.interactUI.PopUpInteractionInfoUI(); // 将提示物品信息的UI激活
            HideTaken();
        }

        public void HideTaken()
        {
            taken = true;
            gameObject.SetActive(false);
        }

        public void ShowForNewCycle()
        {
            if (!taken) return;
            taken = false;
            gameObject.SetActive(true);
        }
    }
}