using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace CatchMoon
{
    public class AchievementCardManager : MonoBehaviour
    {
        PlayerManager player;

        [Header("显示成就信息的控件")]
        public TextMeshProUGUI achievementNameTMP;
        public TextMeshProUGUI achievementDescriptionTMP;
        public Image achievementImage;
        public Image achievementImageBlack;
        public List<GameObject> toggles;

        [Header("当前预览的成就为第几阶段")]
        [Min(1)]public int currentViewAchievementStage = 1;

        [Header("成就的信息")]
        [TextArea] public List<string> achievementDescription;

        private void Awake()
        {
            player = PlayerUIManager.FindPlayer(this);
        }
        private void Start()
        {
            #region 检测空引用异常
            if (player == null)
                Debug.LogError("player == null");
            #endregion
        }

        public void Colse()
        {
            gameObject.SetActive(false);
            int len = toggles.Count;
            for (int i = 1; i < len; i++)
            {
                Destroy(toggles[1]);
                toggles.RemoveAt(1);
            }

            player.ui.escWin.GetAchievementWin().buttons.SetActive(true);
            player.ui.escWin.GetAchievementWin().achievementCardsWin.cardList.SetActive(true);
            player.ui.openedWin = EscWinOpenedWinType.achievementWin;
        }

        // 控制卡片在不同阶段之间的切换
        /// <summary>
        /// 预览卡片的下一个阶段
        /// </summary>
        public void ViewNextCardStage()
        {
            toggles[currentViewAchievementStage - 1].GetComponent<Toggle>().isOn = false;
            currentViewAchievementStage = (currentViewAchievementStage) % toggles.Count + 1;
            ApplyCurrentCardStage();
        }
        /// <summary>
        /// 预览卡片的上一个阶段
        /// </summary>
        public void ViewPreCardStage()
        {
            toggles[currentViewAchievementStage - 1].GetComponent<Toggle>().isOn = false;
            currentViewAchievementStage = (currentViewAchievementStage + toggles.Count - 2) % toggles.Count + 1;
            ApplyCurrentCardStage();
        }
        void ApplyCurrentCardStage()
        {
            achievementDescriptionTMP.text = achievementDescription[currentViewAchievementStage - 1];
            toggles[currentViewAchievementStage - 1].GetComponent<Toggle>().isOn = true;
        }
    }
}