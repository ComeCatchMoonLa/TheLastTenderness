using UnityEngine;

namespace CatchMoon
{
    public class EscWindowsManager : MonoBehaviour
    {
        PlayerManager player;

        [SerializeField] SettingsWindowManager settingsWin;
        [SerializeField] AchievementWinManager achievenmentWin;
        public GameObject selectButtons;

        private void Awake()
        {
            player = transform.root.GetComponent<PlayerManager>();
        }
        private void Start()
        {
            #region 检测空引用异常
            if (settingsWin == null)
                Debug.LogError("settingsWin == null");
            if (achievenmentWin == null)
                Debug.LogError("achievementWin == null");
            #endregion
        }

        public void Open()
        {
            gameObject.SetActive(true);

            // 禁用与Esc窗口操作无关的输入, 并启用Esc窗口操作相关的输入
            player.input.inputActions.Disable();
            player.input.inputActions.UIOperation.Enable();
            player.input.cameraRotateInput = Vector2.zero;

            // Unfinished: 暂停游戏
            Time.timeScale = 0;

            // 设置当前所处的ui层级
            player.ui.openedWin = EscWinOpenedWinType.escWin;
        }
        public void Close()
        {
            gameObject.SetActive(false);

            // 禁用与Esc窗口操作相关的输入, 并禁用Esc窗口操作相关的输入
            player.input.inputActions.Enable();
            player.input.inputActions.UIOperation.Disable();

            // Unfinished: 取消暂停
            Time.timeScale = 1;

            // 设置当前所处的ui层级
            player.ui.openedWin = EscWinOpenedWinType.notEscWinMode;
        }

        public void OpenSettingsWin()
        {
            selectButtons.SetActive(false);
            settingsWin.gameObject.SetActive(true);
            player.ui.openedWin = EscWinOpenedWinType.settingsWin;
        }
        public void OpenAchievementWin()
        {
            selectButtons.SetActive(false);
            achievenmentWin.gameObject.SetActive(true);
            player.ui.openedWin = EscWinOpenedWinType.achievementWin;
        }
        // 每个ui层级的返回按钮可能样式不一样
        public void ColseSettingsWin()
        {
            selectButtons.SetActive(true);
            settingsWin.gameObject.SetActive(false);
            player.ui.openedWin = EscWinOpenedWinType.escWin;
        }
        public void ColseAchievementWin()
        {
            selectButtons.SetActive(true);
            achievenmentWin.gameObject.SetActive(false);
            player.ui.openedWin = EscWinOpenedWinType.escWin;
        }
        public void BackToEscWin()
        {
            // 关闭该层的ui
            if (player.ui.openedWin == EscWinOpenedWinType.settingsWin)
                ColseSettingsWin();
            else
                ColseAchievementWin();
        }

        public SettingsWindowManager GetSettingWin()
        {
            #region 检测空引用异常
            if (settingsWin == null)
                Debug.LogError("settingsWin == null");
            #endregion

            return settingsWin;
        }

        public AchievementWinManager GetAchievementWin()
        {
            #region 检测空引用异常
            if (achievenmentWin == null)
                Debug.LogError("achievenmentWin == null");
            #endregion

            return achievenmentWin;
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        }
    }
}