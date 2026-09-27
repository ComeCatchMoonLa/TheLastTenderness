using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CatchMoon
{
    /// <summary>
    /// 可交互UI
    /// </summary>
    public class InteractUI : MonoBehaviour
    {
        [Header("交互提示UI")]
        [SerializeField] GameObject interactInfoUI;       // 交互信息UI: 玩家靠近可交互物品时，提示玩家如何交互的UI（告诉玩家拾取物品、递交物品等），或展示文字信息的UI（路标提示或是NPC的对话等）
        [SerializeField] TextMeshProUGUI interactTipText; // 玩家与物品交互时的提示文本

        [Header("交互品信息UI")]
        [SerializeField] GameObject interactionInfoUI;        // 交互物品信息UI: 当玩家完成与物品的交互后，展示物品信息的UI
        [SerializeField] TextMeshProUGUI interactionInfoText; // 交互物品的信息文本
        [SerializeField] Image interactionIcon;               // 交互物品的图标

        private void Start()
        {
            #region 检测空引用异常
            if (interactInfoUI == null)
                Debug.LogError("interactInfoUI == null");
            if (interactTipText == null)
                Debug.LogError("interactTipText == null");

            if (interactionInfoUI == null)
                Debug.LogError("interactionInfoUI == null");
            if (interactionInfoText == null)
                Debug.LogError("interactionInfoText == null");
            if (interactionIcon == null)
                Debug.LogError("interactionIcon == null");
            #endregion

            interactionIcon.preserveAspect = true;
        }

        public void SetInteractTip(string tip)
        {
            interactTipText.text = tip;
        }

        public void SetInteractionInfo(Item item)
        {
            interactionInfoText.text = item.itemName;
            interactionIcon.sprite = item.itemIcon;
        }

        public void PopUpInteractInfoUI()
        {
            interactInfoUI.SetActive(true);
        }

        public void CloseInteractInfoUI()
        {
            interactInfoUI.SetActive(false);
        }

        public void PopUpInteractionInfoUI()
        {
            interactionInfoUI.SetActive(true);
        }

        public void CloseInteractionInfoUI()
        {
            interactionInfoUI.SetActive(false);
        }
    }
}