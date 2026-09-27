using UnityEngine;

namespace CatchMoon
{
    public class AchievementWinManager : MonoBehaviour
    {
        [Header("子窗口")]
        public GameObject statisticalWin;
        public AchievementCardsWinManager achievementCardsWin;

        [Header("一些按钮")]
        public GameObject buttons;

        [Header("被选择的窗口")]
        [SerializeField] AchievementWinType selectedWin;

        private void Start()
        {
            #region 检测空引用异常
            if (statisticalWin == null)
                Debug.LogError("statisticalWin == null");
            if (achievementCardsWin == null)
                Debug.LogError("achievementCardsWin == null");
            #endregion
        }

        // 控制子窗口之间的切换
        public void SelectStatisticalWin()
        {
            UnselectCurrentWin();
            selectedWin = AchievementWinType.statistical;
            statisticalWin.SetActive(true);
        }
        public void SelectAchievementCardWin()
        {
            UnselectCurrentWin();
            selectedWin = AchievementWinType.achievementCard;
            achievementCardsWin.gameObject.SetActive(true);
        }
        void UnselectCurrentWin()
        {
            switch (selectedWin)
            {
                case AchievementWinType.statistical:
                    statisticalWin.SetActive(false);
                    break;
                case AchievementWinType.achievementCard:
                    achievementCardsWin.gameObject.SetActive(false);
                    break;
            }
        }
        public void SelectLeftWin()
        {
            switch (selectedWin)
            {
                case AchievementWinType.statistical:
                    SelectAchievementCardWin();
                    break;
                case AchievementWinType.achievementCard:
                    SelectStatisticalWin();
                    break;
            }
        }
        public void SelectRightWin()
        {
            switch (selectedWin)
            {
                case AchievementWinType.statistical:
                    SelectAchievementCardWin();
                    break;
                case AchievementWinType.achievementCard:
                    SelectStatisticalWin();
                    break;
            }
        }
    }
}