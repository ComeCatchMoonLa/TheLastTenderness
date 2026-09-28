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
        public static string FrameRateLimitLabel(FrameRateLimit_Options option)
        {
            switch (option)
            {
                case FrameRateLimit_Options.max60: return "60";
                case FrameRateLimit_Options.max90: return "90";
                case FrameRateLimit_Options.max120: return "120";
                default: return "无限制";
            }
        }

        public static int FrameRateLimitValue(FrameRateLimit_Options option)
        {
            switch (option)
            {
                case FrameRateLimit_Options.max60: return 60;
                case FrameRateLimit_Options.max90: return 90;
                case FrameRateLimit_Options.max120: return 120;
                default: return -1;
            }
        }

        public static FrameRateLimit_Options NextFrameRateLimit(FrameRateLimit_Options option)
        {
            switch (option)
            {
                case FrameRateLimit_Options.max60: return FrameRateLimit_Options.max90;
                case FrameRateLimit_Options.max90: return FrameRateLimit_Options.max120;
                case FrameRateLimit_Options.max120: return FrameRateLimit_Options.unlimited;
                default: return FrameRateLimit_Options.max60;
            }
        }

        public static FrameRateLimit_Options PreviousFrameRateLimit(FrameRateLimit_Options option)
        {
            switch (option)
            {
                case FrameRateLimit_Options.max60: return FrameRateLimit_Options.unlimited;
                case FrameRateLimit_Options.max90: return FrameRateLimit_Options.max60;
                case FrameRateLimit_Options.max120: return FrameRateLimit_Options.max90;
                default: return FrameRateLimit_Options.max120;
            }
        }

        public void FrameRateLimit_SetNextOption()
        {
            gameSettingsData.frameRateLimit_Options = NextFrameRateLimit(gameSettingsData.frameRateLimit_Options);
            FrameRateLimit_ApplyCurrentOption();
        }

        public void FrameRateLimit_SetPreviousOption()
        {
            gameSettingsData.frameRateLimit_Options = PreviousFrameRateLimit(gameSettingsData.frameRateLimit_Options);
            FrameRateLimit_ApplyCurrentOption();
        }

        void FrameRateLimit_ApplyCurrentOption()
        {
            FrameRateLimit_Options option = gameSettingsData.frameRateLimit_Options;
            frameRateLimit_Option_text.text = FrameRateLimitLabel(option);
            Application.targetFrameRate = FrameRateLimitValue(option);
        }

        // 画质
        public static string DisplayQualityLabel(DisplayQuality_Options option)
        {
            switch (option)
            {
                case DisplayQuality_Options.veryLow: return "非常低";
                case DisplayQuality_Options.low: return "低";
                case DisplayQuality_Options.medium: return "中等";
                case DisplayQuality_Options.high: return "高";
                case DisplayQuality_Options.veryHigh: return "非常高";
                default: return "最高";
            }
        }

        public static int DisplayQualityLevel(DisplayQuality_Options option)
        {
            switch (option)
            {
                case DisplayQuality_Options.veryLow: return 0;
                case DisplayQuality_Options.low: return 1;
                case DisplayQuality_Options.medium: return 2;
                case DisplayQuality_Options.high: return 3;
                case DisplayQuality_Options.veryHigh: return 4;
                default: return 5;
            }
        }

        public static DisplayQuality_Options NextDisplayQuality(DisplayQuality_Options option)
        {
            switch (option)
            {
                case DisplayQuality_Options.veryLow: return DisplayQuality_Options.low;
                case DisplayQuality_Options.low: return DisplayQuality_Options.medium;
                case DisplayQuality_Options.medium: return DisplayQuality_Options.high;
                case DisplayQuality_Options.high: return DisplayQuality_Options.veryHigh;
                case DisplayQuality_Options.veryHigh: return DisplayQuality_Options.Ultra;
                default: return DisplayQuality_Options.veryLow;
            }
        }

        public static DisplayQuality_Options PreviousDisplayQuality(DisplayQuality_Options option)
        {
            switch (option)
            {
                case DisplayQuality_Options.veryLow: return DisplayQuality_Options.Ultra;
                case DisplayQuality_Options.low: return DisplayQuality_Options.veryLow;
                case DisplayQuality_Options.medium: return DisplayQuality_Options.low;
                case DisplayQuality_Options.high: return DisplayQuality_Options.medium;
                case DisplayQuality_Options.veryHigh: return DisplayQuality_Options.high;
                default: return DisplayQuality_Options.veryHigh;
            }
        }

        public void DisplayQuality_SetNextOption()
        {
            gameSettingsData.displayQuality_Options = NextDisplayQuality(gameSettingsData.displayQuality_Options);
            DisplayQuality_ApplyCurrentOption();
        }

        public void DisplayQuality_SetPreviousOption()
        {
            gameSettingsData.displayQuality_Options = PreviousDisplayQuality(gameSettingsData.displayQuality_Options);
            DisplayQuality_ApplyCurrentOption();
        }

        void DisplayQuality_ApplyCurrentOption()
        {
            DisplayQuality_Options option = gameSettingsData.displayQuality_Options;
            displayQuality_Option_text.text = DisplayQualityLabel(option);
            QualitySettings.SetQualityLevel(DisplayQualityLevel(option));
        }

        // 后处理品质
        public static string PostTreatmentQualityLabel(PostTreatmentQuality_Options option)
        {
            switch (option)
            {
                case PostTreatmentQuality_Options.low: return "低";
                default: return "高";
            }
        }

        public static PostTreatmentQuality_Options NextPostTreatmentQuality(PostTreatmentQuality_Options option)
        {
            switch (option)
            {
                case PostTreatmentQuality_Options.low: return PostTreatmentQuality_Options.high;
                default: return PostTreatmentQuality_Options.low;
            }
        }

        public void PostTreatmentQuality_SwitchOption()
        {
            gameSettingsData.postTreatmentQuality_Options = NextPostTreatmentQuality(gameSettingsData.postTreatmentQuality_Options);
            PostTreatmentQuality_ApplyCurrentOption();
        }

        void PostTreatmentQuality_ApplyCurrentOption()
        {
            postTreatmentQuality_text.text = PostTreatmentQualityLabel(gameSettingsData.postTreatmentQuality_Options);
        }

        // 特效质量
        public void SpecialEffectQuality_SwitchOption()
        {
            switch (gameSettingsData.specialEffectQuality_Options)
            {
                case SpecialEffectQuality_Options.low:
                    {
                        gameSettingsData.specialEffectQuality_Options = SpecialEffectQuality_Options.high;
                        specialEffectQuality_text.text = "高";
                    }
                    break;
                case SpecialEffectQuality_Options.high:
                    {
                        gameSettingsData.specialEffectQuality_Options = SpecialEffectQuality_Options.low;
                        specialEffectQuality_text.text = "低";
                    }
                    break;
            }
        }
        void SpecialEffectQuality_ApplyCurrentOption()
        {
            switch (gameSettingsData.specialEffectQuality_Options)
            {
                case SpecialEffectQuality_Options.low:
                    {
                        specialEffectQuality_text.text = "低";
                    }
                    break;
                case SpecialEffectQuality_Options.high:
                    {
                        specialEffectQuality_text.text = "高";
                    }
                    break;
            }
        }
        
        /// [未完成] 亮度调节(准确来说是伽马值)
        #endregion
    }
}