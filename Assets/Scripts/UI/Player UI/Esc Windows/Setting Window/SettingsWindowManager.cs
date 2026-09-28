using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CatchMoon
{
    public class SettingsWindowManager : MonoBehaviour
    {
        PlayerManager player;

        [Header("游戏设置数据")]
        public GameSettingsData gameSettingsData;

        [Header("子窗口")]
        [SerializeField] GameObject gameSettingsWin;
        [SerializeField] GameObject displayWin;
        [SerializeField] GameObject soundWin;
        [SerializeField] GameObject controlWin;

        [Header("被选择的窗口")]
        [SerializeField] SettingsWinType selectedWin;

        [Header("游戏设置")]
        [SerializeField] Toggle auto_Toggle;
        [SerializeField] Toggle wordForWord_Toggle;
        [SerializeField] Toggle skip_Toggle;
        [SerializeField] Toggle fps_Toggle;
        [SerializeField] Toggle autoChangeLockOnTarget_Toggle;
        [SerializeField] TMP_InputField cameraUpAndDownSpeed;
        [SerializeField] TMP_InputField cameraLeftAndRightSpeed;

        [Header("显示设置")]
        [SerializeField] TextMeshProUGUI vsync_Option_Text;
        [SerializeField] TextMeshProUGUI frameRateLimit_Option_text;
        [SerializeField] TextMeshProUGUI displayQuality_Option_text;
        [SerializeField] TextMeshProUGUI postTreatmentQuality_text;
        [SerializeField] TextMeshProUGUI specialEffectQuality_text;

        [Header("选项的文本(该选项被选中后加粗)")]
        [SerializeField] TextMeshProUGUI selectButton_GameSetting_Text;
        [SerializeField] TextMeshProUGUI selectButton_Display_Text;
        [SerializeField] TextMeshProUGUI selectButton_Sound_Text;
        [SerializeField] TextMeshProUGUI selectButton_Control_Text;

        private void Awake()
        {
            player = transform.root.GetComponent<PlayerManager>();
        }

        private void Start()
        {
            #region 检测空引用异常
            if (gameSettingsData == null)
                Debug.LogError("gameSettingsData == null");

            if (vsync_Option_Text == null)
                Debug.LogError("vsync_Option_Text == null");
            if (frameRateLimit_Option_text == null)
                Debug.LogError("frameRateLimit_Option_text == null");
            if (displayQuality_Option_text == null)
                Debug.LogError("displayQuality_Option_text == null");
            if (postTreatmentQuality_text == null)
                Debug.LogError("postTreatmentQuality_text == null");
            if (specialEffectQuality_text == null)
                Debug.LogError("specialEffectQuality_text == null");
            #endregion
        }

        // 控制子窗口之间的切换
        public void SelectGameSettingsWin()
        {
            UnselectCurrentWin();
            selectedWin = SettingsWinType.gameSettings;
            selectButton_GameSetting_Text.fontStyle = FontStyles.Bold;
            gameSettingsWin.SetActive(true);
        }
        public void SelectDisplayWin()
        {
            UnselectCurrentWin();
            selectedWin = SettingsWinType.display;
            selectButton_Display_Text.fontStyle = FontStyles.Bold;
            displayWin.SetActive(true);
        }
        public void SelectSoundWin()
        {
            UnselectCurrentWin();
            selectedWin = SettingsWinType.sound;
            selectButton_Sound_Text.fontStyle = FontStyles.Bold;
            soundWin.SetActive(true);
        }
        public void SelectControlWin()
        {
            UnselectCurrentWin();
            selectedWin = SettingsWinType.control;
            selectButton_Control_Text.fontStyle = FontStyles.Bold;
            controlWin.SetActive(true);
        }
        void UnselectCurrentWin()
        {
            if (selectedWin == SettingsWinType.gameSettings)
            {
                selectButton_GameSetting_Text.fontStyle = FontStyles.Normal;
                gameSettingsWin.SetActive(false);
            }
            else if (selectedWin == SettingsWinType.display)
            {
                selectButton_Display_Text.fontStyle = FontStyles.Normal;
                displayWin.SetActive(false);
            }
            else if (selectedWin == SettingsWinType.sound)
            {
                selectButton_Sound_Text.fontStyle = FontStyles.Normal;
                soundWin.SetActive(false);
            }
            else if (selectedWin == SettingsWinType.control)
            {
                selectButton_Control_Text.fontStyle = FontStyles.Normal;
                controlWin.SetActive(false);
            }
        }
        public void SelectLeftWin()
        {
            switch (selectedWin)
            {
                case SettingsWinType.gameSettings:
                    SelectControlWin();
                    break;
                case SettingsWinType.display:
                    SelectGameSettingsWin();
                    break;
                case SettingsWinType.sound:
                    SelectDisplayWin();
                    break;
                case SettingsWinType.control:
                    SelectSoundWin();
                    break;
            }
        }
        public void SelectRightWin()
        {
            switch (selectedWin)
            {
                case SettingsWinType.gameSettings:
                    SelectDisplayWin();
                    break;
                case SettingsWinType.display:
                    SelectSoundWin();
                    break;
                case SettingsWinType.sound:
                    SelectControlWin();
                    break;
                case SettingsWinType.control:
                    SelectGameSettingsWin();
                    break;
            }
        }

        // 应用游戏设置
        public void ApplyGameSettings(PlayerManager player)
        {
            #region 检测空引用异常
            if (gameSettingsData == null)
                Debug.LogError("gameSettingsData == null");

            if (vsync_Option_Text == null)
                Debug.LogError("vsync_Option_Text == null");
            if (frameRateLimit_Option_text == null)
                Debug.LogError("frameRateLimit_Option_text == null");
            if (displayQuality_Option_text == null)
                Debug.LogError("displayQuality_Option_text == null");
            if (postTreatmentQuality_text == null)
                Debug.LogError("postTreatmentQuality_text == null");
            if (specialEffectQuality_text == null)
                Debug.LogError("specialEffectQuality_text == null");
            #endregion

            // 【游戏】子窗口设置
            ApplyDialogueBoxSetting();
            ApplyFpsSetting();
            ApplyAutoChangeLockOnTargetSetting();
            ApplyCameraSpeedSetting(player);
            // 【画面】子窗口设置
            VSYNC_ApplyCurrentOption();
            FrameRateLimit_ApplyCurrentOption();
            DisplayQuality_ApplyCurrentOption(); 
            PostTreatmentQuality_ApplyCurrentOption(); 
            SpecialEffectQuality_ApplyCurrentOption();
        }

        #region 【游戏】子窗口设置
        // 底部文字框设置
        public void SaveAutoSetting()
        {
            gameSettingsData.auto = auto_Toggle.isOn;
        }
        public void SaveWFWSetting()
        {
            gameSettingsData.wordForWord = wordForWord_Toggle.isOn;
        }
        public void SaveSkipSetting()
        {
            gameSettingsData.skip = skip_Toggle.isOn;
        }
        void ApplyDialogueBoxSetting()
        {
            auto_Toggle.isOn = gameSettingsData.auto;
            wordForWord_Toggle.isOn = gameSettingsData.wordForWord;
            skip_Toggle.isOn = gameSettingsData.skip;
        }
        // FPS设置
        public void SaveFpsSetting()
        {
            gameSettingsData.enableFps = fps_Toggle.isOn;
        }
        void ApplyFpsSetting()
        {
            fps_Toggle.isOn = gameSettingsData.enableFps;
        }
        // 相机速度设置
        public void SaveCameraLeftAndRightSpeedSetting()
        {
            gameSettingsData.cameraLeftAndRightSpeed = float.Parse(cameraLeftAndRightSpeed.text);
            ApplyCameraSpeedSetting(player);
        }
        public void SaveCameraUpAndDownSpeedSetting()
        {
            gameSettingsData.cameraUpAndDownSpeed = float.Parse(cameraUpAndDownSpeed.text);
            ApplyCameraSpeedSetting(player);
        }
        void ApplyCameraSpeedSetting(PlayerManager player)
        {
            player.pCamera.leftAndRightSpeed = gameSettingsData.cameraLeftAndRightSpeed;
            player.pCamera.upAndDownSpeed = gameSettingsData.cameraUpAndDownSpeed;
            cameraLeftAndRightSpeed.placeholder.GetComponent<TextMeshProUGUI>().text = gameSettingsData.cameraLeftAndRightSpeed.ToString();
            cameraUpAndDownSpeed.placeholder.GetComponent<TextMeshProUGUI>().text = gameSettingsData.cameraUpAndDownSpeed.ToString();
        }
        // 相机自动切换锁定目标设置
        public void SaveAutoChangeLockOnTargetSetting()
        {
            gameSettingsData.autoChangeLockOnTarget = autoChangeLockOnTarget_Toggle.isOn;
        }
        void ApplyAutoChangeLockOnTargetSetting()
        {
            autoChangeLockOnTarget_Toggle.isOn = gameSettingsData.autoChangeLockOnTarget;
        }
        #endregion

        #region 【画面】子窗口设置
        // 垂直同步
        public void VSYNC_SwitchOption()
        {
            switch (gameSettingsData.vsync_Options)
            {
                case VSYNC_Options.enable:
                    gameSettingsData.vsync_Options = VSYNC_Options.disable;
                    break;
                case VSYNC_Options.disable:
                    gameSettingsData.vsync_Options = VSYNC_Options.enable;
                    break;
            }
            VSYNC_ApplyCurrentOption();
        }
        void VSYNC_ApplyCurrentOption()
        {
            switch (gameSettingsData.vsync_Options)
            {
                case VSYNC_Options.enable:
                    vsync_Option_Text.text = "启用";
                    QualitySettings.vSyncCount = 1;
                    break;
                case VSYNC_Options.disable:
                    vsync_Option_Text.text = "禁用";
                    QualitySettings.vSyncCount = 0;
                    break;
            }
        }

        // 帧率限制
        public void FrameRateLimit_SetNextOption()
        {
            gameSettingsData.frameRateLimit_Options = FrameRateLimitTable.Next(gameSettingsData.frameRateLimit_Options);
            FrameRateLimit_ApplyCurrentOption();
        }

        public void FrameRateLimit_SetPreviousOption()
        {
            gameSettingsData.frameRateLimit_Options = FrameRateLimitTable.Previous(gameSettingsData.frameRateLimit_Options);
            FrameRateLimit_ApplyCurrentOption();
        }

        void FrameRateLimit_ApplyCurrentOption()
        {
            FrameRateLimit_Options option = gameSettingsData.frameRateLimit_Options;
            frameRateLimit_Option_text.text = FrameRateLimitTable.Label(option);
            Application.targetFrameRate = FrameRateLimitTable.Value(option);
        }

        public void DisplayQuality_SetNextOption()
        {
            gameSettingsData.displayQuality_Options = DisplayQualityTable.Next(gameSettingsData.displayQuality_Options);
            DisplayQuality_ApplyCurrentOption();
        }

        public void DisplayQuality_SetPreviousOption()
        {
            gameSettingsData.displayQuality_Options = DisplayQualityTable.Previous(gameSettingsData.displayQuality_Options);
            DisplayQuality_ApplyCurrentOption();
        }

        void DisplayQuality_ApplyCurrentOption()
        {
            DisplayQuality_Options option = gameSettingsData.displayQuality_Options;
            displayQuality_Option_text.text = DisplayQualityTable.Label(option);
            QualitySettings.SetQualityLevel(DisplayQualityTable.Level(option));
        }

        public void PostTreatmentQuality_SwitchOption()
        {
            gameSettingsData.postTreatmentQuality_Options = PostTreatmentQualityTable.Next(gameSettingsData.postTreatmentQuality_Options);
            PostTreatmentQuality_ApplyCurrentOption();
        }

        void PostTreatmentQuality_ApplyCurrentOption()
        {
            postTreatmentQuality_text.text = PostTreatmentQualityTable.Label(gameSettingsData.postTreatmentQuality_Options);
        }

        public void SpecialEffectQuality_SwitchOption()
        {
            gameSettingsData.specialEffectQuality_Options = SpecialEffectQualityTable.Next(gameSettingsData.specialEffectQuality_Options);
            SpecialEffectQuality_ApplyCurrentOption();
        }

        void SpecialEffectQuality_ApplyCurrentOption()
        {
            specialEffectQuality_text.text = SpecialEffectQualityTable.Label(gameSettingsData.specialEffectQuality_Options);
        }
        
        /// [未完成] 亮度调节(准确来说是伽马值)
        #endregion
    }
}