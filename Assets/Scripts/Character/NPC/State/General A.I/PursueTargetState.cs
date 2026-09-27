using UnityEngine;

namespace CatchMoon
{
    /// <summary>
    /// ׷��״̬
    /// </summary>
    public class PursueTargetState : State
    {
        CombatStanceState combatStanceState;
        RotateTowardsTargetState rotateTowardsState;

        private void Awake()
        {
            combatStanceState = GetComponent<CombatStanceState>();
            rotateTowardsState = GetComponent<RotateTowardsTargetState>();
        }
        /**
         *  ����Ŀ��
         *  if Ŀ���ڵжԷ�Χ��:
         *      �л�����ս��״̬
         */
        public override State Tick(EnemyManager enemy)
        {
            if (enemy.eStats.isDead)
                return this;
            if (enemy.isInteracting)
                return this;

            HandleRotateTowardsTarget(enemy);

            if (enemy.isPreformingAction)
            {
                enemy.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return this;
            }

            // ִ�ж���ʱֹͣ�ƶ�
            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
                enemy.animator.SetFloat("Vertical", 1f, 0.1f, Time.deltaTime);

            // �ڹ�����Χ��
            if (enemy.distFromTarget <= enemy.aiSettings.aggroRadius)
                return combatStanceState;
            else
                return this;
        }

        /// <summary>
        /// [����] ����Ŀ��
        /// </summary>
        private void HandleRotateTowardsTarget(EnemyManager enemy)
        {
            // �˹���ת
            if (enemy.isPreformingAction)
            {
                if (enemy.targetDir == Vector3.zero)
                    enemy.targetDir = transform.forward;

                Quaternion targetRotation = Quaternion.LookRotation(enemy.targetDir);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, enemy.aiSettings.rotationSpeed * Time.deltaTime);
            }
            // ����navmesh������ת
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemy.navmeshAgent.desiredVelocity); // ��Է���
                Vector3 targetVelocity = enemy.rigidBody.linearVelocity;

                enemy.navmeshAgent.enabled = true;
                enemy.navmeshAgent.SetDestination(enemy.currentTarget.transform.position);
                enemy.rigidBody.linearVelocity = targetVelocity;
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, enemy.navmeshAgent.transform.rotation, enemy.aiSettings.rotationSpeed * Time.deltaTime);
            }
        }
    }
}