using UnityEngine;

namespace CatchMoon
{
    public class PickUpItem : Interactable
    {
        public Item item; // ����

        private void Start()
        {
            #region ���
            if (item == null)
            {
                Debug.LogError("weapon is null.");
                return;
            }
            #endregion
        }

        /// <summary>
        /// ��ҽ���[��д]
        /// </summary>
        public override void Interact(PlayerManager player)
        {
            // ʰ����Ʒ�����������ƶ�
            player.rigidBody.linearVelocity = Vector3.zero;
            player.pAnimator.PlayTargetAnimation("Pick Up Item", true);
            player.pInventory.AddItem(item);
            player.ui.popUps.interactUI.SetInteractionInfo(item);

            player.ui.popUps.interactUI.PopUpInteractionInfoUI(); // ����ʾ��Ʒ��Ϣ��UI����
            Destroy(gameObject);
        }
    }
}