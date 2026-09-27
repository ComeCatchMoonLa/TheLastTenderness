using UnityEngine;

namespace CatchMoon
{
    public class PlayerLocomotionManager : MonoBehaviour
    {
        PlayerManager player;

        Vector3 normalVector = Vector3.up;  // ������(���ڵ��������µ��ٶ�, �����ٶȼ�С, �����ٶ�����)
        public Vector3 moveDir;             // �ƶ�����

        [Header("Ground & Air Detection Stats")]
        // ��Fall��Land, ��ֱ��Land, ��ʲô������
        // Player��ײ�����·��ĸ߶Ⱦ�����
        float rayStartPointHeight;                              // ��ؼ�����ߵ����߶�
        [SerializeField] float HeightToBeginLand = 0.8f;        // ��ʼ��½�ĸ߶�
        float minHeightNeedToLand;                              // ��ĳ��ƽ̨����Land��������Сƽ̨�߶�
        [SerializeField] float rayStartPointOffsetDist = 0.2f;  // ��ؼ����������ƫ��(Player�ƶ�ʱ)
        [SerializeField] UnityEngine.LayerMask GroundCheckLayer;            // ��ؼ��Ĳ�

        [Header("����ٶ�")]
        [SerializeField] float rotateSpeed = 10; // ��ת�ٶ�
        [SerializeField] float runSpeed = 5;     // �����ٶ�
        [SerializeField] float sprintSpeed = 7;  // ����ٶ�
        [SerializeField] float fallSpeed = 200;  // �����ٶ�

        [Header("��������")]
        [SerializeField] float rollCost = 10;     // �������ĵ�����ֵ
        [SerializeField] float backStepCost = 5;  // ���������ĵ�����ֵ
        public float sprintCost = 0.2f;           // �������
        public float sprintNeedMinStamina = 10f;  // �����Ҫ����������ֵ
        [SerializeField] float jumpCost = 10;     // ��Ծ���ĵ�����ֵ

        [Header("��ͬ״̬�µ�Collider")]
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
            rayStartPointHeight = player.cCollider.center.y - player.cCollider.height * 0.5f; //(��Player��ײ�����·�ͬ��)
            minHeightNeedToLand = rayStartPointHeight; // ��ֱ���ϵ�ƽ̨�߶Ⱦ���ֱ����

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
        }

        void HandleMove()
        {
            if (player.isInteracting || player.input.rollFlag)
                return;

            UpdateMoveDir();
            float speed = runSpeed;
            if (player.isSprinting) // ���
            {
                speed = sprintSpeed;
                player.pStats.DeductStamina(sprintCost);
            }

            // normalVector��ʾPlayer��ǰ��վ��λ�ô�����ƽ��Ĵ�ֱ����
            // ��Player���ٶ�ʸ��ͶӰ����������������ƽ����, ����������ΪPlayer�ĸ�����ٶ�(���������ʱ���ٶ�)
            Vector3 projectVelocity = Vector3.ProjectOnPlane(moveDir * speed, normalVector);
            // �ж�������ʱ����������Player������
            if (moveDir != Vector3.zero)
            {
                float theta = Vector3.Angle(moveDir, normalVector); // ������ƶ����������ڵ���ƽ��ļнǵĲ���, 0��ֵ����(0, 180)

                if (theta < 90) // ����
                    projectVelocity *= (2f / Mathf.Sin(theta * Mathf.Deg2Rad) - 1);
            }
            player.rigidBody.linearVelocity = projectVelocity;

            // ��������Ƿ�����������Ƿ��̾�������ƶ��ķ�ʽ
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

            /// ���ô������߼����������
            Vector3 origin = transform.position;
            origin.y += rayStartPointHeight;

            // ��ǰ�����ϰ���, �򲻽��м����������ƫ��
            if (!Physics.Raycast(origin, transform.forward, out hit, rayStartPointOffsetDist))
                origin += moveDir.normalized * rayStartPointOffsetDist;

            //  ����ڿ������Ǿ͸���ʩ��������(һ�����ƶ�����, һ������)
            if (player.isInAir)
                player.rigidBody.AddForce((Vector3.down + moveDir * 0.1f) * fallSpeed * player.rigidBody.mass);

            Vector3 targetPosition = transform.position; // ��������Player�ĸ߶�

            // �ж��Ƿ���½(����Land״̬���ڵ�����)
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
            
            // ��Player���õ�targetPosition(������ҵ�������������)
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