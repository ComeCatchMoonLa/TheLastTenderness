using UnityEngine;

namespace CatchMoon
{
    public class EnemyAnimatorManager : CharacterAnimatorManager
    {
        EnemyManager enemy;

        protected override void Awake()
        {
            base.Awake();

            enemy = GetComponent<EnemyManager>();
        }

        public static readonly int LocomotionState = Animator.StringToHash("Locomotion");

        public static bool WritesHorizontalRootVelocity(bool isInteracting, int baseLayerShortNameHash)
        {
            return isInteracting || baseLayerShortNameHash == LocomotionState;
        }

        /// <summary>
        /// 播放动画时执行的操作
        /// </summary>
        private void OnAnimatorMove()
        {
            if (WritesHorizontalRootVelocity(enemy.isInteracting, enemy.animator.GetCurrentAnimatorStateInfo(0).shortNameHash))
            {
                enemy.rigidBody.linearDamping = 0;
                Vector3 deltaPosition = enemy.animator.deltaPosition;
                deltaPosition.y = 0;
                if (Time.deltaTime > 0f)
                {
                    Vector3 velocity = deltaPosition / Time.deltaTime;
                    enemy.rigidBody.linearVelocity = velocity;
                }
            }
            // 朝向仍只看这个旗标，不跟水平速度绑在一起
            if (enemy.isRotatingWithRootMotion)
                enemy.transform.rotation = enemy.animator.deltaRotation;
        }

        void InstantiateBossParticleFX()
        {
            BossCombatFXManager bossCombatFXManager = GetComponentInChildren<BossCombatFXManager>();

            bossCombatFXManager.secondPhaseWeaponFX.SetActive(true);
        }
    }
}