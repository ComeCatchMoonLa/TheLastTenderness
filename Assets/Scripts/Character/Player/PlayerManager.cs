using UnityEngine;

namespace CatchMoon
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public InputManager input;
        [HideInInspector] public PlayerCameraManager pCamera;
        [HideInInspector] public PlayerUIManager ui;

        [HideInInspector] public PlayerAnimatorManager pAnimator;
        [HideInInspector] public PlayerCombatManager pCombat;
        [HideInInspector] public PlayerEffectsManager pEffects;
        [HideInInspector] public PlayerArmorManager pArmor;
        [HideInInspector] public PlayerInventoryManager pInventory;
        [HideInInspector] public PlayerLocomotionManager pLocomotion;
        [HideInInspector] public PlayerStatsManager pStats;
        [HideInInspector] public PlayerWeaponSlotManager pWeaponSlot;

        [Header("Player Flags")]
        public bool aimingMode = false;
        public float aimHeld;

        public void ClearAimingMode()
        {
            aimingMode = false;
            aimHeld = 0f;
        }
        [System.NonSerialized] public float colliderRadiusBeforeAim;
        [System.NonSerialized] public bool aimingRadiusApplied;
        public bool noArmor = false; // 无装备系统

         
        [Header("Camera Transform While Aiming")]
        public Transform cameraTransformWhileAiming;

        [Header("交互")]
        public float interactRadius = 2f;
        readonly Collider[] interactColliders = new Collider[1];

        [Header("对话对象(剧情npc)")]
        public EnemyManager storyNpc = null;

        protected override void Awake()
        {
            base.Awake();

            input = GetComponent<InputManager>();
            pCamera = FindAnyObjectByType<PlayerCameraManager>();
            ui = GetComponentInChildren<PlayerUIManager>();

            pAnimator = GetComponent<PlayerAnimatorManager>();
            pArmor = GetComponent<PlayerArmorManager>();
            pCombat = GetComponent<PlayerCombatManager>();
            pEffects = GetComponent<PlayerEffectsManager>();
            pInventory = GetComponent<PlayerInventoryManager>();
            pLocomotion = GetComponent<PlayerLocomotionManager>();
            pStats = GetComponent<PlayerStatsManager>();
            pWeaponSlot = GetComponent<PlayerWeaponSlotManager>();
        }
        protected override void Start()
        {
            base.Start();

            #region 检测空引用异常
            if (input == null)
                Debug.LogError($"{transform.name}: input == null");
            if (pCamera == null)
                Debug.LogError($"{transform.name}: pCamera == null");
            if (ui == null)
                Debug.LogError($"{transform.name}: pUI == null");

            if (pAnimator == null)
                Debug.LogError($"{transform.name}: pAnimator == null");
            if (!noArmor && pArmor == null)
                Debug.LogError($"{transform.name}: pArmor == null");
            if (pCombat == null)
                Debug.LogError($"{transform.name}: pCombat == null");
            if (pEffects == null)
                Debug.LogError($"{transform.name}: pEffects == null");
            if (pInventory == null)
                Debug.LogError($"{transform.name}: pInventory == null");
            if (pLocomotion == null)
                Debug.LogError($"{transform.name}: pLocomotion == null");
            if (pStats == null)
                Debug.LogError($"{transform.name}: pStats == null");
            if (pWeaponSlot == null)
                Debug.LogError($"{transform.name}: pWeaponSlot == null");

            if (cameraTransformWhileAiming == null)
                Debug.LogError($"{transform.name}: cameraTransformWhileAiming == null");
            #endregion
        }
        protected override void Update()
        {
            if (pStats.isDead) return;
            base.Update();
            ResetCollider();
            CheckForInteractableObject();
            ui.popUps.HandleCloseViewInfoWindow();

            if (!ui.escWin.GetSettingWin().gameSettingsData.auto)
                ui.popUps.talkUI.AdvanceDialogue(storyNpc, false);

            if (ui.escWin.GetSettingWin().gameSettingsData.skip)
                ui.popUps.talkUI.Handle_SkipTalk_Input(storyNpc);
        }

        //private void OnDrawGizmosSelected()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawSphere(transform.position, interactRadius);
        //}

        /// <summary>
        /// 检测可交互对象
        /// </summary>
        public void CheckForInteractableObject()
        {
            InteractUI interactUI = ui.popUps.interactUI;

            // 目前只能一个个处理
            int iColliderCnt = Physics.OverlapSphereNonAlloc(transform.position, interactRadius, interactColliders, LayerMask.interactable | LayerMask.npc);

            if (iColliderCnt > 0 && interactColliders[0].tag == "Interactable")
            {
                Interactable interaction = interactColliders[0].GetComponent<Interactable>(); // 通过其子类WeaponPickUp获取Interactable的

                if (interaction != null)
                {
                    interactUI.SetInteractTip(interaction.interactTipText);
                    interactUI.PopUpInteractInfoUI();

                    if (input.interacte_Tap_Input) // 按Enter键交互
                    {
                        interactColliders[0].GetComponent<Interactable>().Interact(this);
                        input.interacte_Tap_Input = false;
                    }
                }
            }
            else
            {
                interactUI.CloseInteractInfoUI();

                if (input.interacte_Tap_Input)
                    interactUI.CloseInteractionInfoUI();
            }
        }
        
        /// <summary>
        /// 打开宝箱交互
        /// </summary>
        public void OpenChestInteracting(Transform playerStandingHereWhenOpeningChest, Vector3 chestPosition)
        {
            if (isInteracting) return;

            rigidBody.linearVelocity = Vector3.zero; // 防止角色打滑
            transform.position = playerStandingHereWhenOpeningChest.position;

            Vector3 targetDirection = chestPosition - playerStandingHereWhenOpeningChest.position;
            targetDirection.y = 0;
            targetDirection.Normalize();
            Quaternion tr = Quaternion.LookRotation(targetDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, 300 * Time.deltaTime);
            transform.rotation = targetRotation;

            pAnimator.PlayTargetAnimation("Open Chest - Start", true);
        }

        void ResetCollider()
        {
            if (isRolling || isJumping)
                return;

            cCollider.center = pLocomotion.defaultColliderCenter;
            cCollider.height = pLocomotion.defaultColliderHeight;
            if (!aimingRadiusApplied)
                cCollider.radius = pLocomotion.defaultColliderRadius;
        }

        protected override void UpdateBoolsValue()
        {
            base.UpdateBoolsValue();
            if (aimingMode)
                aimHeld += Time.deltaTime;
            animator.SetBool("aimingMode", aimingMode);
        }
    }
}