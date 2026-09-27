using UnityEngine;

namespace CatchMoon
{
    /// <summary>
    /// 动画处理器
    /// </summary>
    public class PlayerAnimatorManager : CharacterAnimatorManager
    {
        PlayerManager player;

        int vertical;           // 前后
        int horizontal;         // 左右

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }
        private void Start()
        {
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        /// <summary>
        /// 更新动画参数
        /// </summary>
        /// <param name="verticalMovement">前后偏移量</param>
        /// <param name="horizontalMovement">左右偏移量</param>
        /// <param name="isSprinting">是否在冲刺</param>
        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting, bool lockOnFlagAndNoSprint)
        {
            #region Vertical
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1;
            }
            #endregion

            #region Horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            #endregion

            if (isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }

            if (lockOnFlagAndNoSprint)
                h = Mathf.Clamp(horizontalMovement, -0.5f, 0.5f);

            player.animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            player.animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }
      
        /// <summary>
        /// 播放动画时执行的操作
        /// </summary>
        private void OnAnimatorMove()
        {
            if (!player.isInteracting) return;

            player.rigidBody.linearDamping = 0;
            Vector3 deltaPosition = player.animator.deltaPosition; // 角色模型此时的位置到该角色模型上一帧的距离
            //deltaPosition.y = 0;
            if (Time.deltaTime > 0)
            {
                Vector3 velocity = deltaPosition / Time.deltaTime; // 计算动画中角色模型的速率
                player.rigidBody.linearVelocity = velocity;     // 使刚体即角色模型的速率与动画中角色模型的速率相等
            }
        }

        void SuccessfullyCastSpell()
        {
            player.pInventory.currentSpell.SuccessfullyCastSpell(player);
        }
        void SuccessfullyCastConsumable()
        {
            player.pInventory.currentConsumable.SucessfullyUsedConsumable(player);
        }
    }
}