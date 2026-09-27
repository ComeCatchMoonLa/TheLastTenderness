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
        /// ���Ŷ���ʱִ�еĲ���
        /// </summary>
        private void OnAnimatorMove()
        {
            // ���ø�����ٶ�Ϊ������ˮƽ���ϵ��ٶ�
            enemy.rigidBody.linearDamping = 0;
            Vector3 deltaPosition = enemy.animator.deltaPosition;
            deltaPosition.y = 0;
            if (Time.deltaTime > 0f)
            {
                Vector3 velocity = deltaPosition / Time.deltaTime;
                enemy.rigidBody.linearVelocity = velocity;
            }
            // �ж��Ƿ���ҪӦ�ö����ĸ��˶�������ת(��ת֮����α���Ȼͨ��isInteracting����)
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