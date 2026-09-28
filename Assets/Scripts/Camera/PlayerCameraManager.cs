using UnityEngine;

namespace CatchMoon
{
    public class PlayerCameraManager : MonoBehaviour
    {
        #region 变量
        PlayerManager player;
        LockOnQuery lockOnQuery;
        
        Vector3 cameraCurrentVelocity = Vector3.zero; // 相机跟随速度(用于平滑过度)
        Vector3 pivotOffsetVelocity = Vector3.zero;

        [Header("相机支架")]
        public Transform cameraPivotTransform; // 相机支架Transform

        [Header("相机")]
        public Transform cameraTransform; // 相机Transform
        public Camera cameraObject;       // 相机Camera

        [Header("相机可检测到的层")]
        public int cameraCanDetectLayers; // 相机可以检测到的层

        [Header("相机速度")]
        [SerializeField] float followFadeTime = 0.2f;       // 相机跟随过渡时间
        [SerializeField] float changeModeFadeTime = 0.02f;  // 相机模式过渡时间
        public float leftAndRightSpeed = 120f;        // 相机左右旋转速度
        public float upAndDownSpeed = 60f;            // 相机上下旋转速度
        [SerializeField] float leftAndRightAimingSpeed = 60f; // 相机左右旋转速度(瞄准时)
        [SerializeField] float upAndDownAimingSpeed = 30f;    // 相机左右旋转速度(瞄准时)

        [Header("相机视角")]
        [SerializeField] float leftAndRightAngle;
        [SerializeField][Range(-35, 35)] float upAndDownAngle;
        [SerializeField] float minLookUpAngle = -35; // 最小仰角(就是最大俯角)
        [SerializeField] float maxLookUpAngle = 35;  // 最大仰角

        public bool isDefaultMode = true;

        [Header("相机Collider设置")]
        // (虚构的collider, 用于处理相机的动态偏移)
        [SerializeField] float cameraSphereRadius = 0.2f;       // 相机射线检测球的半径为0.2
        [SerializeField] float cameraCollisionOffSet = 0.2f;    // 相机碰撞时的向角色的位移0.2
        [SerializeField] float minCollisionOffSet = 0.2f;       // 相机最小的移动距离为0.2

        [Header("相机位置偏移")]
        [SerializeField] float defaultPivotHeight = 1.4f;       // 相机未锁定时轴的高度为1.4
        [SerializeField] float lockedPivotHeight = 2.2f;        // 相机锁定时的轴的高度为2.0
        [SerializeField] float aimingPivotHeight = 1.5f;        // 相机瞄准时的轴的高度为1.5
        [SerializeField][Tooltip("默认相机在玩家后面的距离(以前后为轴)")] float defaultCameraForwardDist = 4f;
        [SerializeField][Tooltip("瞄准时相机在玩家后面的距离(以前后为轴)")] float aimingCameraForwardDist = 1.2f;
        [SerializeField][Tooltip("瞄准时相机在玩家左边的距离(以左右为轴)")] float aimingCameraRightDist = 0.6f;

        [Header("锁定设置")]
        // Player可锁定区域为: 以Player朝向为对称轴、maxLockOnDist为半径、2 * maxlockAngle_half为圆心角的扇形区域
        public bool lockOnFlag = false;                         // 是否为锁定视角模式(已经进入了锁定模式, curTarget不为空)
        public bool lockOnMode = false;                         // 是否为锁定模式(player按F进行切换)
        public ChangeLockOnTargetMode changeLockOnTargetMode = ChangeLockOnTargetMode.nearest; // 切换锁定目标的设置(切换至最近目标/切换至血量最低目标)
        public CharacterManager curLockOnTarget;                // 当前锁定目标
        public CharacterManager nearestLockableTarget;          // 最近可锁定目标
        public CharacterManager leftLockableTarget;             // 当前锁定目标的左边(Player的左右)最近的可锁定目标
        public CharacterManager rightLockableTarget;            // 当前锁定目标的右边(Player的左右)最近的可锁定目标
        #endregion

        private void Awake()
        {
            player = FindAnyObjectByType<PlayerManager>();
            cameraObject= GetComponentInChildren<Camera>();
            lockOnQuery = GetComponent<LockOnQuery>();

            #region 检测空引用异常
            if (player == null)
                Debug.LogError("playerManager is null.");
            if (cameraObject == null)
                Debug.LogError("cameraObject == null");
            if (lockOnQuery == null)
                Debug.LogError("lockOnQuery == null");
            #endregion
        }
        private void Start()
        {
            #region 检测空引用异常
            if (player == null)
                Debug.LogError($"{transform.name}: player == null");
            if (cameraObject == null)
                Debug.LogError($"{transform.name}: cameraObject == null");
            #endregion

            cameraCanDetectLayers = (LayerMask.defaultLayerMask | LayerMask.environment | LayerMask.npc);
        }
        private void Update()
        {
            if (player.pStats.isDead) return;

            UpdateLockOnTargets();
            HandleLockOnInput();
        }
        private void LateUpdate()
        {
            if (player.pStats.isDead) return;

            HandleCameraRotation();
            SetCameraPosOffset(); // 设置相机位置偏移 【mark, 准备重构一下】

            FollowPlayer();
        }

        void HandleLockOnInput()
        {
            if (player.input.f_Input)
            {
                player.input.f_Input = false;

                if (player.pInventory.leftWeapon.weaponType == WeaponType.bow) return; // 拿弓箭时无法锁定视角

                if (!lockOnFlag) // 锁定目标(视角)
                    lockOnMode = true;
                else
                    player.pCamera.Unlock();
            }

            if (lockOnFlag)
            {
                if (player.ui.escWin.GetSettingWin().gameSettingsData.autoChangeLockOnTarget) return;

                if (player.input.one_Input) // 锁定当前目标的左边一个目标
                {
                    player.input.one_Input = false;
                    if (leftLockableTarget != null)
                    {
                        curLockOnTarget = leftLockableTarget;
                        UpdateLockOnTargets();
                    }
                }
                if (player.input.two_Input) // 锁定当前目标的右边一个目标
                {
                    player.input.two_Input = false;
                    if (rightLockableTarget != null)
                    {
                        curLockOnTarget = rightLockableTarget;
                        UpdateLockOnTargets();
                    }
                }
            }
        }

        enum CameraPose
        {
            Aim,
            Lock,
            Default
        }

        CameraPose CurrentCameraPose()
        {
            if (player.aimingMode)
                return CameraPose.Aim;
            if (lockOnFlag)
                return CameraPose.Lock;
            return CameraPose.Default;
        }

        /// <summary>
        /// 相机跟随目标
        /// </summary>
        public void FollowPlayer()
        {
            switch (CurrentCameraPose())
            {
                case CameraPose.Aim:
                    SetCameraPos_AimingMode();
                    break;
                case CameraPose.Lock:
                    SetCameraPos_LockedMode();
                    break;
                default:
                    SetCameraPos_DefaultMode();
                    break;
            }
            HandleCameraCollisions();
        }
        public void SetCameraPos_LockedMode()
        {
            transform.position = player.transform.position;
        }
        public void SetCameraPos_DefaultMode()
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraCurrentVelocity, followFadeTime);
        }
        public void SetCameraPos_AimingMode()
        {
            transform.position = player.cameraTransformWhileAiming.position;
        }

        /// <summary>
        /// 处理[相机碰撞] (相机与场景中其他物体发生碰撞时将靠近目标)
        /// </summary>
        /// 相机碰撞处理2：将遮挡物设为透明。(可能不会整了)
        void HandleCameraCollisions()
        {
            float targetCameraForwardDist;
            if (CurrentCameraPose() == CameraPose.Aim)
            {
                targetCameraForwardDist = aimingCameraForwardDist;
            }
            else
            {
                targetCameraForwardDist = defaultCameraForwardDist;
                RaycastHit hit;
                Vector3 dir = cameraTransform.position - cameraPivotTransform.position;
                dir.Normalize();

                if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, dir, out hit, Mathf.Abs(targetCameraForwardDist), LayerMask.environment))
                {
                    float dist = Vector3.Distance(cameraPivotTransform.position, hit.point);
                    targetCameraForwardDist = dist - cameraCollisionOffSet;
                }
                if (Mathf.Abs(targetCameraForwardDist) < minCollisionOffSet)
                {
                    targetCameraForwardDist = minCollisionOffSet;
                }
            }

            
            cameraTransform.localPosition = -Vector3.forward * targetCameraForwardDist;
        }

        /// <summary>
        /// 处理[相机旋转]
        /// </summary>
        /// <param name="mouseXInput">鼠标X轴输入</param>
        /// <param name="mouseYInput">鼠标Y轴输入</param>
        void HandleCameraRotation()
        {
            switch (CurrentCameraPose())
            {
                case CameraPose.Aim:
                    HandleAimingCameraRotation();
                    break;
                case CameraPose.Lock:
                    HandleLockedCameraRotation();
                    break;
                default:
                    HandleDefaultCameraRotation();
                    break;
            }
        }

        void HandleDefaultCameraRotation()
        {
            if (!isDefaultMode)
            {
                isDefaultMode = true;
                leftAndRightAngle = transform.localEulerAngles.y;
                upAndDownAngle = cameraPivotTransform.localEulerAngles.x;
                if (upAndDownAngle > 180)
                    upAndDownAngle -= 360;
            }
            else
            {
                leftAndRightAngle += player.input.mouseX * leftAndRightSpeed * Time.deltaTime;
                upAndDownAngle -= player.input.mouseY * upAndDownSpeed * Time.deltaTime;
                upAndDownAngle = Mathf.Clamp(upAndDownAngle, minLookUpAngle, maxLookUpAngle);
            }

            transform.localRotation = Quaternion.Euler(0f, leftAndRightAngle, 0f);
            cameraPivotTransform.localRotation = Quaternion.Euler(upAndDownAngle, 0f, 0f);
        }
        void HandleLockedCameraRotation()
        {
            isDefaultMode = false;
            Vector3 cameraHolderDirection = curLockOnTarget.transform.position - transform.position;
            cameraHolderDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(cameraHolderDirection.normalized);

            Vector3 cameraPivotDirection = curLockOnTarget.transform.position - cameraPivotTransform.position;
            cameraPivotDirection.y = 0;
            cameraPivotTransform.rotation = Quaternion.LookRotation(cameraPivotDirection.normalized);
        }
        void HandleAimingCameraRotation()
        {
            isDefaultMode = false;
            cameraTransform.localRotation = cameraPivotTransform.localRotation = Quaternion.identity;

            leftAndRightAngle += player.input.mouseX * leftAndRightAimingSpeed * Time.deltaTime;
            upAndDownAngle -= player.input.mouseY * upAndDownAimingSpeed * Time.deltaTime;
            upAndDownAngle = Mathf.Clamp(upAndDownAngle, minLookUpAngle, maxLookUpAngle);

            transform.localRotation = player.transform.localRotation = Quaternion.Euler(0f, leftAndRightAngle, 0f);
            cameraPivotTransform.localRotation = Quaternion.Euler(upAndDownAngle, 0f, 0f);
        }

        public PlayerManager Player => player;

        public void UpdateLockOnTargets()
        {
            lockOnQuery.UpdateLockOnTargets();
        }

        public void Unlock()
        {
            curLockOnTarget = null;
            lockOnMode = false;
            lockOnFlag = false;
        }

        /// <summary>
        /// 设置相位置偏移
        /// </summary>
        public static Vector3 AimingCameraOffsetTarget(float aimingCameraRightDist, float aimingPivotHeight)
        {
            return new Vector3(-aimingCameraRightDist, aimingPivotHeight, 0f);
        }

        public static Vector3 LockedCameraOffsetTarget(float lockedPivotHeight)
        {
            return Vector3.up * lockedPivotHeight;
        }

        public static Vector3 DefaultCameraOffsetTarget(float defaultPivotHeight)
        {
            return Vector3.up * defaultPivotHeight;
        }

        void SetCameraPosOffset()
        {
            switch (CurrentCameraPose())
            {
                case CameraPose.Aim:
                    SetCameraPosOffset_AimingMode();
                    break;
                case CameraPose.Lock:
                    SetCameraPosOffset_LockedMode();
                    break;
                default:
                    SetCameraPosOffset_DefaultMode();
                    break;
            }
        }

        public void SetCameraPosOffset_AimingMode()
        {
            // 过肩视角, 人物在屏幕右下角
            ApplyPivotOffset(AimingCameraOffsetTarget(aimingCameraRightDist, aimingPivotHeight));
        }

        public void SetCameraPosOffset_LockedMode()
        {
            ApplyPivotOffset(LockedCameraOffsetTarget(lockedPivotHeight));
        }

        public void SetCameraPosOffset_DefaultMode()
        {
            ApplyPivotOffset(DefaultCameraOffsetTarget(defaultPivotHeight));
        }

        void ApplyPivotOffset(Vector3 target)
        {
            cameraPivotTransform.localPosition = Vector3.SmoothDamp(
                cameraPivotTransform.localPosition, target, ref pivotOffsetVelocity, changeModeFadeTime);
        }
    }
}