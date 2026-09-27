using UnityEngine;

namespace CatchMoon
{
    /// <summary>
    /// ս��״̬
    /// </summary>
    public class CombatStanceState : State
    {
        PursueTargetState pursueTargetState;
        protected AttackState attackState;
        public EnemyAttackAction[] enemyAttacks;

        bool setAroundDirection = false;
        protected float verticalMovementValue = 0f;
        protected float horizontalMovementValue = 0f;

        private void Awake()
        {
            pursueTargetState = GetComponent<PursueTargetState>();
            attackState = GetComponent<AttackState>();
        }

        /**
         *  ��⹥����Χ
         *  if �ڹ�����Χ��:
         *      if ������Ϊ����ȴ:
         *          ���빥��״̬
         *      else: 
         *          ��������ս����̬״̬
         *  else:
         *      �л�������Ŀ��״̬
         */
        public override State Tick(EnemyManager enemy)
        {
            if (enemy.eStats.isDead) return this;

            enemy.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemy.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);

            if (enemy.isInteracting)
            {
                enemy.animator.SetFloat("Vertical", 0);
                enemy.animator.SetFloat("Horizontal", 0);
                return this;
            }

            attackState.hasPerformedAttack = false;

            //Debug.Log(string.Format($"����ҵľ���: {enemy.distanceFromTarget:N0}"));
            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
                return pursueTargetState;

            DecideCirclingAction(enemy);
            HandleRotateTowardsTarget(enemy);
            if (enemy.currentRecoveryTime <= 0 && attackState.currentAttack != null)
                return attackState;
            else
                GetNewAttack(enemy);
            return this;
        }

        /// <summary>
        /// [����] ����Ŀ��
        /// </summary>
        protected void HandleRotateTowardsTarget(EnemyManager enemy)
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

        /// <summary>
        /// ������Ȧ����
        /// </summary>
        protected void DecideCirclingAction(EnemyManager enemy)
        {
            WalkAroundTarget(enemy);
        }

        /// <summary>
        /// Χ��Ŀ����
        /// </summary>
        protected void WalkAroundTarget(EnemyManager enemy)
        {
            if (enemy.currentRecoveryTime > 0)
            {
                if (!setAroundDirection)
                {
                    horizontalMovementValue = Random.Range(0, 2) - 0.5f; // ������������, 0.5f��-0.5f���ʶ԰�
                    setAroundDirection = true;
                }
            }
            else
            {
                horizontalMovementValue = 0;
                setAroundDirection = false;
            }

            if (enemy.distFromTarget > 1.4f)
                verticalMovementValue = 0.5f;
            else
                verticalMovementValue = 0f;
        }

        /// <summary>
        /// ��ȡ�µĹ�����Ϊ
        /// </summary>
        /// <param name="enemyManger">���˹�����</param>
        protected virtual void GetNewAttack(EnemyManager enemy)
        {
            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; ++i)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemy.distFromTarget <= enemyAttackAction.maxDistNeededToAttack && enemy.distFromTarget >= enemyAttackAction.minDistNeededToAttack)
                    if (enemy.targetDirAngle <= enemyAttackAction.maxAttackAngle && enemy.targetDirAngle >= enemyAttackAction.minAttackAngle)
                        maxScore += enemyAttackAction.attackScore;
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; ++i)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];
                if (enemy.distFromTarget <= enemyAttackAction.maxDistNeededToAttack && enemy.distFromTarget >= enemyAttackAction.minDistNeededToAttack)
                {
                    if (enemy.targetDirAngle <= enemyAttackAction.maxAttackAngle && enemy.targetDirAngle >= enemyAttackAction.minAttackAngle)
                    {
                        if (attackState.currentAttack != null) return;

                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                            attackState.currentAttack = enemyAttackAction;
                    }
                }
            }
        }
    }
}