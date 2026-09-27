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

        /// <summary>
        /// 播放动画时执行的操作
        /// </summary>
        private void OnAnimatorMove()
        {
            // 设置刚体的速度为动画在水平面上的速度
            enemy.rigidBody.drag = 0;
            Vector3 deltaPosition = enemy.animator.deltaPosition;
            deltaPosition.y = 0;
            if (Time.deltaTime > 0f)
            {
                Vector3 velocity = deltaPosition / Time.deltaTime;
                enemy.rigidBody.velocity = velocity;
            }
            // 判断是否需要应用动画的根运动决定旋转(旋转之外的形变依然通过isInteracting决定)
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