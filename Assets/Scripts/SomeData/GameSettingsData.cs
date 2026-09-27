using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Data/GameSettings")]
    public class GameSettingsData : ScriptableObject
    {
        [Header("游戏设置")]
        public bool auto;
        public bool wordForWord;
        public bool skip;
        public bool enableFps;
        public bool autoChangeLockOnTarget;
        public float cameraUpAndDownSpeed;
        public float cameraLeftAndRightSpeed;

        [Header("显示设置")]
        public VSYNC_Options vsync_Options;
        public FrameRateLimit_Options frameRateLimit_Options;
        public DisplayQuality_Options displayQuality_Options;
        public PostTreatmentQuality_Options postTreatmentQuality_Options;
        public SpecialEffectQuality_Options specialEffectQuality_Options;


        public void Reset()
        {

        }

        #region 默认设置
        const bool default_Auto = false;
        const bool default_WordForWord = true;
        const bool default_Skip = false;
        const bool default_EnableFps = false;
        const float default_cameraUpAndDownSpeed = 60f;
        const float default_cameraLeftAndRightSpeed = 120f;
        const VSYNC_Options default_Vsync = VSYNC_Options.disable;
        const FrameRateLimit_Options default_FrameRateLimit = FrameRateLimit_Options.max90;
        const DisplayQuality_Options default_DisplayQuality = DisplayQuality_Options.Ultra;
        const PostTreatmentQuality_Options default_PostTreatmentQuality = PostTreatmentQuality_Options.low;
        const SpecialEffectQuality_Options default_specialEffectQuality = SpecialEffectQuality_Options.low;
        #endregion
    }
}