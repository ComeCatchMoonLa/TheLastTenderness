using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class PlayerCameraManager : MonoBehaviour
    {
        #region 变量
        PlayerManager player;
        
        Vector3 cameraCurrentVelocity = Vector3.zero; // 相机跟随速度(用于平滑过度)
        Vector3 pivotOffsetVelocity = Vector3.zero;
        Collider[] lockOnOverlapResults = new Collider[32];

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
        public ChangeLockOnTargetMode changeLockOnTargetMode = ChangeLockOnTargetMode.nearset; // 切换锁定目标的设置(切换至最近目标/切换至血量最低目标)
        [SerializeField] float maxLookOnAngle_Half = 50f;       // 最大锁定范围角的一半
        [SerializeField] float maxLookOnAngle_Half_AutoChangeLockOnTargetMode = 30f; // 勾选自动锁定目标选项时, 减小可锁定锁定角度
        [SerializeField] float autoChangeLockOnTargetTime = 2f;  // 自动切换锁定目标的时间间隔
        [SerializeField] float autoChangeLockOnTargetTimer = 1f; // 自动切换锁定目标的计时器
        [SerializeField] float maxLockOnDist = 30.0f;           // 可锁敌的最大距离

        [Header("锁定目标信息")]
        [SerializeField] List<CharacterManager> lockableTargets = new List<CharacterManager>(); // 可锁定目标列表
        public CharacterManager curLockOnTarget;                // 当前锁定目标
        public CharacterManager nearestLockableTarget;          // 最近可锁定目标
        public CharacterManager leftLockableTarget;             // 当前锁定目标的左边(Player的左右)最近的可锁定目标
        public CharacterManager rightLockableTarget;            // 当前锁定目标的右边(Player的左右)最近的可锁定目标
        #endregion

        private void Awake()
        {
            player = FindAnyObjectByType<PlayerManager>();
            cameraObject= GetComponentInChildren<Camera>();

            #region 检测空引用异常
            if (player == null)
                Debug.LogError("playerManager is null.");
            if (cameraObject == null)
                Debug.LogError("cameraObject == null");
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

        /// <summary>
        /// 更新[相机锁定目标]
        /// </summary>
        ///   1. curLockOnTarget
        ///   2. nearsetLookableTarget
        ///   3. leftLookableTarget
        ///   4. rightLockableTarget
        public void UpdateLockOnTargets()
        {
            if (!lockOnMode) return;

            lockableTargets.Clear();
            nearestLockableTarget = null;
            leftLockableTarget = null;
            rightLockableTarget = null;

            float minDist = Mathf.Infinity;
            float minDistOfLeftTarget = -Mathf.Infinity;
            float minDistOfRightTarget = Mathf.Infinity;

            // 检测以Player为球心、最大可锁定范围为半径的球体内所包含的所有collider。不传层，和原来的 OverlapSphere 一样。
            int lockOnCount = OverlapQuery.CollectOverlaps(player.transform.position, maxLockOnDist, ~0, ref lockOnOverlapResults);
            for (int i = 0; i < lockOnCount; ++i)
            {
                // 包含collider的物体是否为角色(是否包含CharacterManager)
                CharacterManager lockableTarget = lockOnOverlapResults[i].GetComponent<CharacterManager>();
                if (lockableTarget == null || lockableTarget == player) continue;
                // 计算要用到的相关信息
                Vector3 dir = lockableTarget.transform.position - player.transform.position;
                float fov = Vector3.SignedAngle(dir, cameraTransform.forward, Vector3.up); // player朝向与lockOnTarget方向的夹角
                float dist = Vector3.Distance(lockableTarget.transform.position, player.transform.position);
                //  判断是否在可锁定范围内(扇形区域)
                if (player.ui.escWin.GetSettingWin().gameSettingsData.autoChangeLockOnTarget)
                {
                    // 当开启自动切换锁定目标选项时, 自动切换使用更小的扇形圆心角
                    if (fov < -maxLookOnAngle_Half_AutoChangeLockOnTargetMode
                        || fov > maxLookOnAngle_Half_AutoChangeLockOnTargetMode || dist > maxLockOnDist) continue;
                }
                else
                {
                    if (fov < -maxLookOnAngle_Half || fov > maxLookOnAngle_Half || dist > maxLockOnDist) continue;
                }
                
                // 检测Player到target之间的路径是否被阻挡
                RaycastHit hit;
                if (Physics.Linecast(player.lockOnTransform.position, lockableTarget.lockOnTransform.position, out hit, cameraCanDetectLayers)
                    && hit.transform.gameObject.layer == Layer.environment)
                    continue;
                // 判断可锁定目标是否已死亡(若角色死后禁用collider, 则这里无需判断)
                if (lockableTarget.cStats.isDead) continue;
                // 添加到可锁定列表
                lockableTargets.Add(lockableTarget);
                // 更新nearestLockOnTarget
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestLockableTarget = lockableTarget;
                }
            }
            // 更新leftLockTarget, rightLockTarget
            foreach (CharacterManager lockableTarget in lockableTargets)
            {
                if (curLockOnTarget != null)
                {
                    if (lockableTarget == curLockOnTarget) continue;
                }
                else
                {
                    if (lockableTarget == nearestLockableTarget) continue;
                }
                // 计算敌人相对于玩家的位置Pos, 用Pos.x判断在Player的左边还是右边(小于0在左边, 大于0在右边)
                float relativePlayerPosX = player.transform.InverseTransformPoint(lockableTarget.transform.position).x;
                if (relativePlayerPosX <= 0f)
                {
                    if (relativePlayerPosX > minDistOfLeftTarget)
                    {
                        minDistOfLeftTarget = relativePlayerPosX;
                        leftLockableTarget = lockableTarget;
                    }
                }
                else
                {
                    if (relativePlayerPosX < minDistOfRightTarget)
                    {
                        minDistOfRightTarget = relativePlayerPosX;
                        rightLockableTarget = lockableTarget;
                    }
                }
            }
            HandleChangeLockOnTarget();
        }

        /// <summary>
        /// [处理] 改变当前锁定目标
        /// </summary>
        ///    (拉脱切换、自动切换)
        public void HandleChangeLockOnTarget()
        {
            if (player.ui.escWin.GetSettingWin().gameSettingsData.autoChangeLockOnTarget)
            {
                if (curLockOnTarget == null) // 当前无锁定目标，则直接锁定最近目标
                {
                    if (nearestLockableTarget)
                    {
                        curLockOnTarget = nearestLockableTarget;
                        lockOnFlag = true;
                    }
                }
                else // 若当前已有锁定目标，则先判断自动切换目标的CD，再切换至最近目标
                {
                    autoChangeLockOnTargetTimer += Time.deltaTime;

                    if (autoChangeLockOnTargetTimer > autoChangeLockOnTargetTime)
                    {
                        autoChangeLockOnTargetTimer = 0f;

                        if (nearestLockableTarget)
                        {
                            curLockOnTarget = nearestLockableTarget;
                            lockOnFlag = true;
                        }
                        else
                        {
                            Unlock();
                        }
                    }
                }
            }
            else
            {
                bool needChangeLockTarget = false;
                if (curLockOnTarget != null)
                {
                    // 判断当前锁定目标是否拉脱
                    float curTargetDist = Vector3.Distance(curLockOnTarget.transform.position, player.transform.position);
                    if (curTargetDist > maxLockOnDist)
                        needChangeLockTarget = true;
                    // 判断当前锁定目标是否被挡住
                    RaycastHit hit;
                    if (Physics.Linecast(player.lockOnTransform.position, curLockOnTarget.lockOnTransform.position, out hit, cameraCanDetectLayers)
                        && hit.transform.gameObject.layer == Layer.environment)
                        needChangeLockTarget = true;
                }
                else
                {
                    needChangeLockTarget = true;
                }

                if (needChangeLockTarget)
                {
                    if (nearestLockableTarget != null)
                    {
                        if (changeLockOnTargetMode == ChangeLockOnTargetMode.nearset)
                            curLockOnTarget = nearestLockableTarget;
                        lockOnFlag = true;
                    }
                    else
                    {
                        Unlock();
                    }
                }
            }
        }

        /// <summary>
        /// 取消相机锁定
        /// </summary>
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