using UnityEngine;

namespace CatchMoon
{
    /// <summary>
    /// ����������
    /// </summary>
    public class PlayerAnimatorManager : CharacterAnimatorManager
    {
        PlayerManager player;

        int vertical;           // ǰ��
        int horizontal;         // ����

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
        /// ���¶�������
        /// </summary>
        /// <param name="verticalMovement">ǰ��ƫ����</param>
        /// <param name="horizontalMovement">����ƫ����</param>
        /// <param name="isSprinting">�Ƿ��ڳ��</param>
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
        /// ���Ŷ���ʱִ�еĲ���
        /// </summary>
        private void OnAnimatorMove()
        {
            if (!player.isInteracting) return;

            player.rigidBody.linearDamping = 0;
            Vector3 deltaPosition = player.animator.deltaPosition; // ��ɫģ�ʹ�ʱ��λ�õ��ý�ɫģ����һ֡�ľ���
            //deltaPosition.y = 0;
            if (Time.deltaTime > 0)
            {
                Vector3 velocity = deltaPosition / Time.deltaTime; // ���㶯���н�ɫģ�͵�����
                player.rigidBody.linearVelocity = velocity;     // ʹ���弴��ɫģ�͵������붯���н�ɫģ�͵��������
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