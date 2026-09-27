using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CatchMoon
{
    public class SettingsWindowManager : MonoBehaviour
    {
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
        }
        public void SaveCameraUpAndDownSpeedSetting()
        {
            gameSettingsData.cameraUpAndDownSpeed = float.Parse(cameraUpAndDownSpeed.text);
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
                    {
                        gameSettingsData.vsync_Options = VSYNC_Options.disable;
                        vsync_Option_Text.text = "禁用";
                    }
                    break;
                case VSYNC_Options.disable:
                    {
                        gameSettingsData.vsync_Options = VSYNC_Options.enable;
                        vsync_Option_Text.text = "启用";
                    }
                    break;
            }
        }
        void VSYNC_ApplyCurrentOption()
        {
            switch (gameSettingsData.vsync_Options)
            {
                case VSYNC_Options.enable:
                    {
                        vsync_Option_Text.text = "启用";
                    }
                    break;
                case VSYNC_Options.disable:
                    {
                        vsync_Option_Text.text = "禁用";
                    }
                    break;
            }
        }

        // 帧率限制
        public void FrameRateLimit_SetNextOption()
        {
            switch (gameSettingsData.frameRateLimit_Options)
            {
                case FrameRateLimit_Options.max60:
                    {
                        gameSettingsData.frameRateLimit_Options = FrameRateLimit_Options.max90;
                        frameRateLimit_Option_text.text = "90";
                        Application.targetFrameRate = 90;
                    }
                    break;
                case FrameRateLimit_Options.max90:
                    {
                        gameSettingsData.frameRateLimit_Options = FrameRateLimit_Options.max120;
                        frameRateLimit_Option_text.text = "120";
                        Application.targetFrameRate = 120;
                    }
                    break;
                case FrameRateLimit_Options.max120:
                    {
                        gameSettingsData.frameRateLimit_Options = FrameRateLimit_Options.unlimited;
                        frameRateLimit_Option_text.text = "无限制";
                        Application.targetFrameRate = -1;
                    }
                    break;
                case FrameRateLimit_Options.unlimited:
                    {
                        gameSettingsData.frameRateLimit_Options = FrameRateLimit_Options.max60;
                        frameRateLimit_Option_text.text = "60";
                        Application.targetFrameRate = 60;
                    }
                    break;
            }
        }
        public void FrameRateLimit_SetPreviousOption()
        {
            switch (gameSettingsData.frameRateLimit_Options)
            {
                case FrameRateLimit_Options.max60:
                    {
                        gameSettingsData.frameRateLimit_Options = FrameRateLimit_Options.unlimited;
                        frameRateLimit_Option_text.text = "无限制";
                        Application.targetFrameRate = -1;
                    }
                    break;
                case FrameRateLimit_Options.max90:
                    {
                        gameSettingsData.frameRateLimit_Options = FrameRateLimit_Options.max60;
                        frameRateLimit_Option_text.text = "60";
                        Application.targetFrameRate = 60;
                    }
                    break;
                case FrameRateLimit_Options.max120:
                    {
                        gameSettingsData.frameRateLimit_Options = FrameRateLimit_Options.max90;
                        frameRateLimit_Option_text.text = "90";
                        Application.targetFrameRate = 90;
                    }
                    break;
                case FrameRateLimit_Options.unlimited:
                    {
                        gameSettingsData.frameRateLimit_Options = FrameRateLimit_Options.max120;
                        frameRateLimit_Option_text.text = "120";
                        Application.targetFrameRate = 120;
                    }
                    break;
            }
        }
        void FrameRateLimit_ApplyCurrentOption()
        {
            switch (gameSettingsData.frameRateLimit_Options)
            {
                
                case FrameRateLimit_Options.max60:
                    {
                        frameRateLimit_Option_text.text = "60";
                        Application.targetFrameRate = 60;
                    }
                    break;
                case FrameRateLimit_Options.max90:
                    {
                        frameRateLimit_Option_text.text = "90";
                        Application.targetFrameRate = 90;
                    }
                    break;
                case FrameRateLimit_Options.max120:
                    {
                        frameRateLimit_Option_text.text = "120";
                        Application.targetFrameRate = 120;
                    }
                    break;
                case FrameRateLimit_Options.unlimited:
                    {
                        frameRateLimit_Option_text.text = "无限制";
                        Application.targetFrameRate = -1;
                    }
                    break;
            }
        }

        // 画质
        public void DisplayQuality_SetNextOption()
        {
            switch (gameSettingsData.displayQuality_Options)
            {
                case DisplayQuality_Options.veryLow:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.low;
                        displayQuality_Option_text.text = "低";
                        QualitySettings.SetQualityLevel(1);
                    }
                    break;
                case DisplayQuality_Options.low:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.medium;
                        displayQuality_Option_text.text = "中";
                        QualitySettings.SetQualityLevel(2);
                    }
                    break;
                case DisplayQuality_Options.medium:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.high;
                        displayQuality_Option_text.text = "高";
                        QualitySettings.SetQualityLevel(3);
                    }
                    break;
                case DisplayQuality_Options.high:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.veryHigh;
                        displayQuality_Option_text.text = "非常高";
                        QualitySettings.SetQualityLevel(4);
                    }
                    break;
                case DisplayQuality_Options.veryHigh:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.Ultra;
                        displayQuality_Option_text.text = "最高";
                        QualitySettings.SetQualityLevel(5);
                    }
                    break;
                case DisplayQuality_Options.Ultra:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.veryLow;
                        displayQuality_Option_text.text = "非常低";
                        QualitySettings.SetQualityLevel(0);
                    }
                    break;
            }
        }
        public void DisplayQuality_SetPreviousOption()
        {
            switch (gameSettingsData.displayQuality_Options)
            {
                case DisplayQuality_Options.veryLow:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.Ultra;
                        displayQuality_Option_text.text = "最高"; 
                        QualitySettings.SetQualityLevel(5);
                    }
                    break;
                case DisplayQuality_Options.low:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.veryLow;
                        displayQuality_Option_text.text = "非常低";
                        QualitySettings.SetQualityLevel(0);
                    }
                    break;
                case DisplayQuality_Options.medium:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.low;
                        displayQuality_Option_text.text = "低";
                        QualitySettings.SetQualityLevel(1);
                    }
                    break;
                case DisplayQuality_Options.high:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.medium;
                        displayQuality_Option_text.text = "中等";
                        QualitySettings.SetQualityLevel(2);
                    }
                    break;
                case DisplayQuality_Options.veryHigh:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.high;
                        displayQuality_Option_text.text = "高"; 
                        QualitySettings.SetQualityLevel(3);
                    }
                    break;
                case DisplayQuality_Options.Ultra:
                    {
                        gameSettingsData.displayQuality_Options = DisplayQuality_Options.veryHigh;
                        displayQuality_Option_text.text = "非常高"; 
                        QualitySettings.SetQualityLevel(4);
                    }
                    break;
            }
        }
        void DisplayQuality_ApplyCurrentOption()
        {
            switch (gameSettingsData.displayQuality_Options)
            {
                case DisplayQuality_Options.veryLow:
                    {
                        displayQuality_Option_text.text = "非常低";
                        QualitySettings.SetQualityLevel(0);
                    }
                    break;
                case DisplayQuality_Options.low:
                    {
                        displayQuality_Option_text.text = "低";
                        QualitySettings.SetQualityLevel(1);
                    }
                    break;
                case DisplayQuality_Options.medium:
                    {
                        displayQuality_Option_text.text = "中等";
                        QualitySettings.SetQualityLevel(2);
                    }
                    break;
                case DisplayQuality_Options.high:
                    {
                        displayQuality_Option_text.text = "高";
                        QualitySettings.SetQualityLevel(3);
                    }
                    break;
                case DisplayQuality_Options.veryHigh:
                    {
                        displayQuality_Option_text.text = "非常高";
                        QualitySettings.SetQualityLevel(4);
                    }
                    break;
                case DisplayQuality_Options.Ultra:
                    {
                        displayQuality_Option_text.text = "最高";
                        QualitySettings.SetQualityLevel(5);
                    }
                    break;
            }
        }

        // 后处理品质
        public void PostTreatmentQuality_SwitchOption()
        {
            switch (gameSettingsData.postTreatmentQuality_Options)
            {
                case PostTreatmentQuality_Options.low:
                    {
                        gameSettingsData.postTreatmentQuality_Options = PostTreatmentQuality_Options.high;
                        postTreatmentQuality_text.text = "高";
                    }
                    break;
                case PostTreatmentQuality_Options.high:
                    {
                        gameSettingsData.postTreatmentQuality_Options = PostTreatmentQuality_Options.low;
                        postTreatmentQuality_text.text = "低";
                    }
                    break;
            }
        }
        void PostTreatmentQuality_ApplyCurrentOption()
        {
            switch (gameSettingsData.postTreatmentQuality_Options)
            {
                case PostTreatmentQuality_Options.low:
                    {
                        postTreatmentQuality_text.text = "低";
                    }
                    break;
                case PostTreatmentQuality_Options.high:
                    {
                        postTreatmentQuality_text.text = "高";
                    }
                    break;
            }
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