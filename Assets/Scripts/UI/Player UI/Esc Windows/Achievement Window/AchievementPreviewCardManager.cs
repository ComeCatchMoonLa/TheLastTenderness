using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatchMoon
{
    public class AchievementPreviewCardManager : MonoBehaviour
    {
        PlayerManager player;

        [SerializeField] AchievementCardManager achievementCard;

        [Header("成就信息")]
        public string achievementName;
        public Sprite achievementSprite;      // 成就图片
        public Sprite achievementSpriteBlack; // 成就图片(黑白)
        [Min(1)]public int currentAchievementStage = 1;
        [TextArea] public List<string> achievementDescription;

        private void Awake()
        {
            player = transform.root.GetComponent<PlayerManager>();
        }
        private void Start()
        {
            #region 检测空引用异常
            if (player == null)
                Debug.LogError("player == null");
            #endregion
        }

        public void ViewAchievementCard()
        {
            SetCardView();
            SetCardInfo();

            player.ui.escWin.GetAchievementWin().buttons.SetActive(false);
            player.ui.escWin.GetAchievementWin().achievementCardsWin.cardList.SetActive(false);
            player.ui.openedWin = EscWinOpenedWinType.achievementCard;

            achievementCard.gameObject.SetActive(true);
        }

        /// <summary>
        /// 设置成就卡片的显示内容
        /// (切换卡片阶段时不改变的内容)
        /// </summary>
        void SetCardView()
        {
            achievementCard.achievementImage.sprite = achievementSprite;
            achievementCard.achievementImageBlack.sprite = achievementSpriteBlack;
            achievementCard.achievementNameTMP.text = achievementName;
            achievementCard.achievementDescriptionTMP.text = achievementDescription[currentAchievementStage - 1];
        }

        /// <summary>
        /// 传递成就卡片的相关信息(切换卡片阶段时会改变的内容使用)
        /// </summary>
        void SetCardInfo()
        {
            achievementCard.toggles[0].GetComponent<Toggle>().isOn = false;
            for (int i = 1; i < achievementDescription.Count; i++)
            {
                GameObject toggle = Instantiate(achievementCard.toggles[0], achievementCard.toggles[0].transform.parent);
                achievementCard.toggles.Add(toggle);
            }
            achievementCard.toggles[currentAchievementStage - 1].GetComponent<Toggle>().isOn = true;

            achievementCard.currentViewAchievementStage = currentAchievementStage;
            achievementCard.achievementDescription = achievementDescription;
        }
    }
}