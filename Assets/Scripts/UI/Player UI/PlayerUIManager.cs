using UnityEngine;

namespace CatchMoon
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] PlayerManager player;

        public static PlayerManager FindPlayer(Component from)
        {
            PlayerUIManager ui = from.GetComponentInParent<PlayerUIManager>();
            return ui != null ? ui.player : null;
        }

        [Header("HUD & PopUps")]
        public HUDWindowsManager hud;
        public EscWindowsManager escWin;
        public TapWindowsManager tapWin;
        public PopUpWindowsManager popUps;

        [Header("打开的窗口类型")]
        [Tooltip("用于决定ESC窗口模式下，Back按钮将执行什么操作")]
        public EscWinOpenedWinType openedWin;

        private void Start()
        {
            #region 检测空引用异常
            if (player == null)
                Debug.LogError($"{name}: player 未填");

            if (hud == null)
                Debug.LogError("hud == null");
            if (escWin == null)
                Debug.LogError("escWin == null");
            if (tapWin == null)
                Debug.LogError("tapWin == null");
            if (popUps == null)
                Debug.LogError("popUps == null");
            #endregion
            
            // 应用游戏设置
            escWin.GetSettingWin().ApplyGameSettings(player);
        }
        private void Update()
        {
            if (player.pStats.isDead)
            {
                if (openedWin != EscWinOpenedWinType.notEscWinMode)
                    HandleOpenedEscWindow();
                return;
            }

            HandleTabInput();
            hud.UpdateCrosshair();
            hud.UpdateFPS();

            // 处理在ESC窗口模式相关输入操作
            if (openedWin == EscWinOpenedWinType.notEscWinMode)
            {
                HandleEscInput();
            }
            else
            {
                HandleOpenedEscWindow();
            }
        }

        void HandleOpenedEscWindow()
        {
            HandleEscWinBackInput();
            HandleEscWinMoveSelectorToLeftInput();
            HandleEscWinMoveSelectorToRightInput();
            HandleEscWinMoveSelectorToUpInput();
            HandleEscWinMoveSelectorToDownInput();
        }

        void HandleTabInput()
        {
            if (player.input.ui_tabWinSwitch_Input)
            {
                player.input.ui_tabWinSwitch_Input = false;

                tapWin.Switch();
            }
        }
        void HandleEscInput()
        {
            if (player.input.ui_openEscWin_Input)
            {
                player.input.ui_openEscWin_Input = false;

                escWin.Open();
            }
        }
        void HandleEscWinBackInput()
        {
            if (player.input.ui_Back_Input)
            {
                player.input.ui_Back_Input = false;
                if (openedWin == EscWinOpenedWinType.escWin)
                {
                    escWin.Close();
                }
                else if (openedWin == EscWinOpenedWinType.settingsWin ||
                    openedWin == EscWinOpenedWinType.achievementWin)
                {
                    escWin.BackToEscWin();
                }
                else if (openedWin == EscWinOpenedWinType.achievementCard)
                {
                    escWin.GetAchievementWin().achievementCardsWin.achievementCard.Colse();
                }
            }
        }
        void HandleEscWinMoveSelectorToLeftInput()
        {
            if (player.input.ui_MoveSelectorToLeft_Input)
            {
                player.input.ui_MoveSelectorToLeft_Input = false;

                switch (openedWin)
                {
                    case EscWinOpenedWinType.settingsWin:
                        escWin.GetSettingWin().SelectLeftWin();
                        break;
                    case EscWinOpenedWinType.achievementWin:
                        escWin.GetAchievementWin().SelectLeftWin();
                        break;
                    case EscWinOpenedWinType.achievementCard:
                        escWin.GetAchievementWin().achievementCardsWin.achievementCard.ViewPreCardStage();
                        break;
                }
            }
        }
        void HandleEscWinMoveSelectorToRightInput()
        {
            if (player.input.ui_MoveSelectorToRight_Input)
            {
                player.input.ui_MoveSelectorToRight_Input = false;
                switch (openedWin)
                {
                    case EscWinOpenedWinType.settingsWin:
                        escWin.GetSettingWin().SelectRightWin();
                        break;
                    case EscWinOpenedWinType.achievementWin:
                        escWin.GetAchievementWin().SelectRightWin();
                        break;
                    case EscWinOpenedWinType.achievementCard:
                        escWin.GetAchievementWin().achievementCardsWin.achievementCard.ViewNextCardStage();
                        break;
                }
            }
        }
        void HandleEscWinMoveSelectorToUpInput()
        {

        }
        void HandleEscWinMoveSelectorToDownInput()
        {

        }
    }
}