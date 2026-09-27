using UnityEngine;

namespace CatchMoon
{
    public class PlayerLocomotionManager : MonoBehaviour
    {
        PlayerManager player;

        Vector3 normalVector = Vector3.up;  // 法向量(用于调整上下坡的速度, 上坡速度减小, 下坡速度增大)
        public Vector3 moveDir;             // 移动方向

        [Header("Ground & Air Detection Stats")]
        // 先Fall再Land, 或直接Land, 或什么都不做
        // Player碰撞器最下方的高度决定了
        float rayStartPointHeight;                              // 落地检测射线的起点高度
        [SerializeField] float HeightToBeginLand = 0.8f;        // 开始着陆的高度
        float minHeightNeedToLand;                              // 下某个平台播放Land动画的最小平台高度
        [SerializeField] float rayStartPointOffsetDist = 0.2f;  // 落地检测射线起点的偏移(Player移动时)
        [SerializeField] UnityEngine.LayerMask GroundCheckLayer;            // 落地检侧的层

        [Header("玩家速度")]
        [SerializeField] float rotateSpeed = 10; // 旋转速度
        [SerializeField] float runSpeed = 5;     // 奔跑速度
        [SerializeField] float sprintSpeed = 7;  // 冲刺速度
        [SerializeField] float fallSpeed = 200;  // 降落速度
        [SerializeField] float currentSpeed;

        [Header("体力消耗")]
        [SerializeField] float rollCost = 10;     // 翻滚消耗的体力值
        [SerializeField] float backStepCost = 5;  // 后跳步消耗的体力值
        public float sprintCost = 0.2f;           // 冲刺消耗
        public float sprintNeedMinStamina = 10f;  // 冲刺需要的最少体力值
        [SerializeField] float jumpCost = 10;     // 跳跃消耗的体力值

        [Header("不同状态下的Collider")]
        public Vector3 defaultColliderCenter = new Vector3(0f, 1.2f, 0f);
        public float defaultColliderRadius = 0.3f;
        public float defaultColliderHeight = 1.2f;
        public Vector3 jumpingColliderCenter = new Vector3(0f, 0.9f, 0f);
        public float jumpingColliderHeight = 1.8f;
        public Vector3 rollingColliderCenter = new Vector3(0f, 0.6f, 0f);
        public float rollingColliderHeight = 0.3f;

        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }
        private void Start()
        {
            rayStartPointHeight = player.cCollider.center.y - player.cCollider.height * 0.5f; //(与Player碰撞器最下方同高)
            minHeightNeedToLand = rayStartPointHeight; // 能直接上的平台高度就能直接下

            GroundCheckLayer = (LayerMask.environment | LayerMask.npc | LayerMask.player);
        }
        private void FixedUpdate()
        {
            if (player.pStats.isDead) return;

            HandleMove();
            HandleRotate();
            HandleFall();
            HandleJump();
            HandleRoll();
            HandleBackStep();
            currentSpeed = player.rigidBody.linearVelocity.magnitude;
        }

        void HandleMove()
        {
            if (player.isInteracting || player.input.rollFlag)
                return;

            UpdateMoveDir();
            float speed = runSpeed;
            if (player.isSprinting) // 冲刺
            {
                speed = sprintSpeed;
                player.pStats.DeductStamina(sprintCost);
            }

            // normalVector表示Player当前所站的位置处地形平面的垂直向量
            // 将Player的速度矢量投影到法向量所决定的平面上, 并将其设置为Player的刚体的速度(这个是上坡时的速度)
            Vector3 projectVelocity = Vector3.ProjectOnPlane(moveDir * speed, normalVector);
            // 判断是下坡时，我们增加Player的移速
            if (moveDir != Vector3.zero)
            {
                float theta = Vector3.Angle(moveDir, normalVector); // 即玩家移动方向与所在地形平面的夹角的补角, 0的值域在(0, 180)

                if (theta < 90) // 下坡
                    projectVelocity *= (2f / Mathf.Sin(theta * Mathf.Deg2Rad) - 1);
            }
            player.rigidBody.linearVelocity = projectVelocity;

            // 根据相机是否锁定与玩家是否冲刺决定玩家移动的方式
            if (player.pCamera.lockOnFlag && !player.isSprinting)
                player.pAnimator.UpdateAnimatorValues(player.input.vertical, player.input.horizontal, player.isSprinting, true);
            else
                player.pAnimator.UpdateAnimatorValues(player.input.moveAmount, 0, player.isSprinting, false);
        }

        void HandleRotate()
        {
            if (!player.aimingMode)
            {
                if (player.canRotate)
                {
                    Vector3 targetDir;
                    if (player.pCamera.lockOnFlag && !player.isSprinting && !player.isRolling)
                    {
                        targetDir = player.pCamera.curLockOnTarget.transform.position - transform.position;
                        targetDir.y = 0;
                        targetDir.Normalize();
                    }
                    else
                    {
                        UpdateMoveDir();
                        targetDir = moveDir;
                    }

                    if (targetDir == Vector3.zero) return;

                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), rotateSpeed * Time.deltaTime);
                }
            }
        }

        void HandleFall()
        {
            if (player.isJumping) return;

            RaycastHit hit;

            /// 设置触地射线检测的射线起点
            Vector3 origin = transform.position;
            origin.y += rayStartPointHeight;

            // 若前方有障碍物, 则不进行检测射线起点的偏移
            if (!Physics.Raycast(origin, transform.forward, out hit, rayStartPointOffsetDist))
                origin += moveDir.normalized * rayStartPointOffsetDist;

            //  如果在空中我们就给他施加两个力(一个朝移动方向, 一个朝下)
            if (player.isInAir)
                player.rigidBody.AddForce((Vector3.down + moveDir * 0.1f) * fallSpeed * player.rigidBody.mass);

            Vector3 targetPosition = transform.position; // 用于设置Player的高度

            // 判断是否着陆(进入Land状态或在地面上)
            Debug.DrawRay(origin, Vector3.down * (rayStartPointHeight +  HeightToBeginLand), Color.red, 0.1f, false);
            if (Physics.Raycast(origin, Vector3.down, out hit, (rayStartPointHeight + HeightToBeginLand), GroundCheckLayer))
            {
                player.isInAir = false;

                normalVector = hit.normal;
                targetPosition.y = hit.point.y;

                //Debug.Log($"{Mathf.Abs(origin.y-targetPosition.y - rayStartPointHeight):N2}");
                if (Mathf.Abs(targetPosition.y - origin.y) >= (rayStartPointHeight + minHeightNeedToLand))
                    player.pAnimator.PlayTargetAnimation("Land", true);
            }
            else
            {
                player.isInAir = true;
                if (!player.isInteracting)
                {
                    player.pAnimator.PlayTargetAnimation("Falling", true);
                }
            }
            
            // 将Player设置到targetPosition(避免玩家的陷入地面或悬空)
            if (!player.isInAir)
                transform.position = targetPosition;
        }

        void HandleRoll()
        {
            if (player.input.rollFlag)
            {
                player.input.rollFlag = false;

                if (player.pStats.DeductStamina(rollCost))
                {
                    UpdateMoveDir();
                    player.transform.localRotation = Quaternion.LookRotation(moveDir);
                    player.pAnimator.PlayTargetAnimation("Rolling", true);
                }
            }
        }

        void HandleBackStep()
        {
            if (player.input.backStepFlag)
            {
                player.input.backStepFlag = false;

                if (player.pStats.DeductStamina(backStepCost))
                {
                    player.pAnimator.PlayTargetAnimation("Backstep", true);
                }
            }
        }

        void HandleJump()
        {
            if (player.isInteracting) return;

            if (player.input.space_Input)
            {
                player.input.space_Input = false;
                if (player.aimingMode) return;

                if (player.input.moveAmount > 0)
                {
                    if (player.pStats.DeductStamina(jumpCost))
                    {
                        UpdateMoveDir();
                        player.transform.localRotation = Quaternion.LookRotation(moveDir);
                        player.pAnimator.PlayTargetAnimation("Jump", true);
                    }
                }
            }
        }

        void UpdateMoveDir()
        {
            moveDir = player.pCamera.cameraObject.transform.forward * player.input.vertical 
                + player.pCamera.cameraObject.transform.right * player.input.horizontal;
            moveDir.y = 0;
            moveDir.Normalize();
        }
    }
}