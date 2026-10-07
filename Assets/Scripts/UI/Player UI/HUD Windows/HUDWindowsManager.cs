using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CatchMoon
{
    public class HUDWindowsManager : MonoBehaviour
    {
        PlayerManager player;

        [Header("状态条")]
        public HealthBar healthBar;
        public StaminaBar staminaBar;
        public FocusPointsBar focusPointsBar;

        [Header("快捷槽 UI")]
        public QuickSlotsUI quickSlotsUI;

        [Header("灵魂数 UI")]
        public SoulCountUI soulCountUI;

        [Header("准心")]
        public GameObject crosshair;
        public Image lockMark;
        bool lockMarkMissingLogged;

        [Header("FPS(调试用)")]
        public TextMeshProUGUI fps_TMP;
        float totalTime = 0f;
        int needCnt = 300; // 300帧刷新一次fps
        int cnt= 0;

        private void Awake()
        {
            player = PlayerUIManager.FindPlayer(this);

            healthBar = GetComponentInChildren<HealthBar>();
            staminaBar = GetComponentInChildren<StaminaBar>();
            focusPointsBar = GetComponentInChildren<FocusPointsBar>();
            quickSlotsUI = GetComponentInChildren<QuickSlotsUI>();
            soulCountUI = GetComponentInChildren<SoulCountUI>();
        }
        private void Start()
        {
            #region 检测空引用异常
            if (player == null)
                Debug.LogError("player == null");

            if (healthBar == null)
                Debug.LogError("healthBar == null");
            if (staminaBar == null)
                Debug.LogError("staminaBar == null");
            if (focusPointsBar == null)
                Debug.LogError("focusPointsBar == null");
            if (quickSlotsUI == null)
                Debug.LogError("quickSlotsUI == null");
            if (soulCountUI == null)
                Debug.LogError("soulCountUI == null");
            #endregion
        }

        void Update()
        {
            UpdateLockMark();
        }

        public void UpdateLockMark()
        {
            if (player == null || player.pCamera == null) return;
            CharacterManager target = player.pCamera.curLockOnTarget;
            bool dead = target != null && target.cStats != null && target.cStats.isDead;
            bool locked = player.pCamera.lockOnFlag && target != null;
            if (!LockMark.Shown(locked, dead))
            {
                lockMarkMissingLogged = false;
                if (lockMark != null)
                    lockMark.enabled = false;
                return;
            }
            if (lockMark == null)
            {
                if (!lockMarkMissingLogged)
                {
                    Debug.LogError($"{name}: lockMark 未填");
                    lockMarkMissingLogged = true;
                }
                return;
            }
            if (target.lockOnTransform == null || player.pCamera.cameraObject == null)
                return;
            Vector3 screen = player.pCamera.cameraObject.WorldToScreenPoint(target.lockOnTransform.position);
            if (screen.z < 0f)
            {
                lockMark.enabled = false;
                return;
            }
            lockMark.enabled = true;
            lockMark.rectTransform.position = screen;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdateCrosshair()
        {
            if (player.aimingMode)
            {
                if (!crosshair.activeSelf)
                    crosshair.SetActive(true);
            }
            else
            {
                if (crosshair.activeSelf)
                    crosshair.SetActive(false);
            }
        }

        public void UpdateFPS()
        {
            if (player.ui.escWin.GetSettingWin().gameSettingsData.enableFps)
            {
                if (!fps_TMP.gameObject.activeSelf)
                    fps_TMP.gameObject.SetActive(true);
                ++cnt;
                totalTime += Time.deltaTime;
                if (cnt == needCnt)
                {
                    fps_TMP.text = $"FPS:{needCnt / totalTime:N0}";
                    totalTime = cnt = 0;
                }
            }
            else
            {
                if (fps_TMP.gameObject.activeSelf)
                    fps_TMP.gameObject.SetActive(false);
            }
        }
    }
}