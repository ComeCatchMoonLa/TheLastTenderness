using UnityEngine;

namespace CatchMoon
{
    public class InputManager : MonoBehaviour
    {
        PlayerManager player;
        [System.NonSerialized] public InputAcitons inputActions;

        public float horizontal; // 玩家垂直偏移
        public float vertical;   // 玩家水平偏移
        public float moveAmount; // 玩家偏移量
        public float mouseX;     // 鼠标横坐标
        public float mouseY;     // 鼠标纵坐标

        [Header("玩家输入")]
        // 运动输入
        [HideInInspector] public Vector2 cameraRotateInput;
        Vector2 moveInput;
        bool leftShift_Input;
        [HideInInspector] public bool space_Input;

        // 交互输入
        [HideInInspector] public bool interacte_Tap_Input;
        [HideInInspector] public bool interacte_Hold_Input;

        // 战斗输入
        [HideInInspector] public bool f_Input;
        [HideInInspector] public bool one_Input;
        [HideInInspector] public bool two_Input;
        [HideInInspector] public bool y_Input;
        [HideInInspector] public bool x_Input;
        [HideInInspector] public bool tap_e_Input;
        [HideInInspector] public bool hold_e_Input;
        [HideInInspector] public bool tap_q_Input;
        [HideInInspector] public bool hold_q_Input;
        [HideInInspector] public bool tap_r_Input;
        [HideInInspector] public bool tap_z_Input;
        [HideInInspector] public bool up_arrow_Input;
        [HideInInspector] public bool down_arrow_input;
        [HideInInspector] public bool left_Arrow_Input;
        [HideInInspector] public bool right_Arrow_Input;

        // UI开关输入
        [HideInInspector] public bool ui_openEscWin_Input;
        [HideInInspector] public bool ui_tabWinSwitch_Input;

        // UI操作输入
        [HideInInspector] public bool ui_Back_Input;
        [HideInInspector] public bool ui_Confirm_Input;
        [HideInInspector] public bool ui_MoveSelectorToLeft_Input;
        [HideInInspector] public bool ui_MoveSelectorToRight_Input;
        [HideInInspector] public bool ui_MoveSelectorToUp_Input;
        [HideInInspector] public bool ui_MoveSelectorToDown_Input;

        [Header("Flags")]
        public bool backStepFlag;
        public bool rollFlag;      // 是否在翻滚
        public bool sprintFlag;    // 是否在冲刺

        float rollInputTimer; // 滚动输入计时器

        private void OnEnable()
        {
            if (inputActions == null) // 若玩家控制器为空，则将其初始化
            {
                inputActions = new InputAcitons();

                // 运动输入(相机旋转、人物运动)
                inputActions.Locomotion.CameraRotate.performed += i => cameraRotateInput = i.ReadValue<Vector2>();

                inputActions.Locomotion.Move.performed += i => moveInput = i.ReadValue<Vector2>();
                inputActions.Locomotion.RollOrBackStepOrSprint.performed += i => leftShift_Input = true;
                inputActions.Locomotion.RollOrBackStepOrSprint.canceled += i => leftShift_Input = false;
                inputActions.Locomotion.Jump.performed += i => space_Input = true;

                // 交互输入(点击或按住)
                inputActions.Interacte.Tap.performed += i => interacte_Tap_Input = true;
                inputActions.Interacte.Hold.performed += i => interacte_Hold_Input = true;
                inputActions.Interacte.Hold.canceled += i => interacte_Hold_Input = false;

                // 战斗输入
                inputActions.Combat.SwitchCameraLockOnMode.performed += i => f_Input = true;
                inputActions.Combat.LookOnTargetLeftOne.performed += i => one_Input = true;
                inputActions.Combat.LookOnTargetRightOne.performed += i => two_Input = true;

                inputActions.Combat.SwitchHoldWeaponMode.performed += i => y_Input = true;
                inputActions.Combat.UseComsumable.performed += i => x_Input = true;
                inputActions.Combat.RightWeaponLightAttackOrShootArrow.performed += i => tap_e_Input = true;
                inputActions.Combat.CriticalAttack.performed += i => hold_e_Input = true;
                inputActions.Combat.CriticalAttack.canceled += i => hold_e_Input = false;
                inputActions.Combat.RightWeaponHeavyAttack.performed += i => tap_r_Input = true;
                inputActions.Combat.LeftWeaponLightAttack.performed += i => tap_q_Input = true;
                inputActions.Combat.BlockOrAim.performed += i => hold_q_Input = true;
                inputActions.Combat.BlockOrAim.canceled += i => hold_q_Input = false;
                inputActions.Combat.LeftWeaponHeavyAttackOrParry.performed += i => tap_z_Input = true;
                inputActions.Combat.SwitchLeftWeapon.performed += i => left_Arrow_Input = true;
                inputActions.Combat.SwitchRightWeapon.performed += i => right_Arrow_Input = true;
                inputActions.Combat.SwitchSpell.performed += i => up_arrow_Input = true;
                inputActions.Combat.SwitchComsumable.performed += i => down_arrow_input = true;

                // UI窗口开关(打开或关闭)输入
                inputActions.UISwitch.OpenEscWin.performed += i => ui_openEscWin_Input = true;
                inputActions.UISwitch.TabWinSwitch.performed += i => ui_tabWinSwitch_Input = true;

                // UI窗口操作输入(打开UI窗口后, 控制UI的按键操作, 选择器的移动、返回、确认)
                inputActions.UIOperation.Back.performed += i => ui_Back_Input = true;
                inputActions.UIOperation.Confirm.performed += i => ui_Confirm_Input = true;
                inputActions.UIOperation.MoveSelectorToLeft.performed += i => ui_MoveSelectorToLeft_Input = true;
                inputActions.UIOperation.MoveSelectorToRight.performed += i => ui_MoveSelectorToRight_Input = true;
                inputActions.UIOperation.MoveSelectorToUp.performed += i => ui_MoveSelectorToUp_Input = true;
                inputActions.UIOperation.MoveSelectorToDown.performed += i => ui_MoveSelectorToDown_Input = true;
            }
            inputActions.Enable();
            inputActions.UIOperation.Disable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }
        private void Update()
        {
            if (player.pStats.isDead) return;

            HandleMoveInput();
            HandleRollInput();
        }
        private void LateUpdate()
        {
            up_arrow_Input = false;
            down_arrow_input = false;
            left_Arrow_Input = false;
            right_Arrow_Input = false;
            interacte_Tap_Input = false;
            ui_openEscWin_Input = false;
        }

        /// <summary>
        /// 处理[移动]输入
        /// </summary>
        void HandleMoveInput()
        {
            horizontal = moveInput.x;
            vertical = moveInput.y;
            // 对于键盘和摇杆是有差别的, 但竖直偏移的平方+水平偏移的平方<=1, 至于为什么需要将其范围限制到[0,1](单次位移偏移量？？？)
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraRotateInput.x;
            mouseY = cameraRotateInput.y;
        }

        /// <summary>
        /// 处理[翻滚]输入
        /// </summary>
        void HandleRollInput()
        {
            if (player.cCombat.isBlocking)
                player.isSprinting = false;

            if (player.isInteracting) return;

            if (leftShift_Input)
            {
                rollInputTimer += Time.deltaTime;

                if (player.pStats.currentStamina <= player.pLocomotion.sprintNeedMinStamina)
                {
                    player.isSprinting = leftShift_Input = false;
                }
                else
                {
                    if (moveAmount > 0 && !player.aimingMode && !player.cCombat.isBlocking)
                        player.isSprinting = true;
                    else
                        player.isSprinting = false;
                }
            }
            else // 松开[左shift]
            {
                player.isSprinting = false;

                // 短按[左shift]就是翻滚([冲刺]时间小于0.5s, 则松开[左shift]会进行翻滚)
                if (0 < rollInputTimer && rollInputTimer < 0.5f)
                {
                    if (moveAmount > 0)
                        rollFlag = true;
                    else
                        backStepFlag = true;
                }
                rollInputTimer = 0;
            }

            // 瞄准的时候不能冲刺
            if (player.aimingMode)
                player.isSprinting = false;
        }
    }
}
