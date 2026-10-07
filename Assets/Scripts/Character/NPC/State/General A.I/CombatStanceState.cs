using UnityEngine;

namespace CatchMoon
{
    /// <summary>
    /// 战斗状态
    /// </summary>
    public class CombatStanceState : State
    {
        public const float ApproachDistance = 1.4f;

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
         *  检测攻击范围
         *  if 在攻击范围内:
         *      if 攻击行为已冷却:
         *          进入攻击状态
         *      else: 
         *          继续保持战斗姿态状态
         *  else:
         *      切换至跟踪目标状态
         */
        public override State Tick(EnemyManager enemy)
        {
            if (enemy.eStats.isDead) return this;

            if (enemy.LeftActivity())
            {
                enemy.returningHome = true;
                enemy.returnToPlacement = true;
                return pursueTargetState;
            }

            enemy.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemy.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);

            if (enemy.isInteracting)
            {
                enemy.animator.SetFloat("Vertical", 0);
                enemy.animator.SetFloat("Horizontal", 0);
                return this;
            }

            attackState.hasPerformedAttack = false;

            //Debug.Log(string.Format($"到玩家的距离: {enemy.distanceFromTarget:N0}"));
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
        /// [处理] 朝向目标
        /// </summary>
        protected void HandleRotateTowardsTarget(EnemyManager enemy)
        {
            // 人工旋转
            if (enemy.isPreformingAction)
            {
                if (enemy.targetDir == Vector3.zero)
                    enemy.targetDir = transform.forward;

                Quaternion targetRotation = Quaternion.LookRotation(enemy.targetDir);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, enemy.aiSettings.rotationSpeed * Time.deltaTime);
            }
            // 根据navmesh导航旋转
            else
            {
                enemy.navmeshAgent.enabled = true;
                enemy.navmeshAgent.SetDestination(enemy.currentTarget.transform.position);
                Vector3 velocity = enemy.navmeshAgent.desiredVelocity;
                velocity.y = enemy.rigidBody.linearVelocity.y;
                enemy.rigidBody.linearVelocity = velocity;
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, enemy.navmeshAgent.transform.rotation, enemy.aiSettings.rotationSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// 决定绕圈动画
        /// </summary>
        protected void DecideCirclingAction(EnemyManager enemy)
        {
            WalkAroundTarget(enemy);
        }

        /// <summary>
        /// 围绕目标走
        /// </summary>
        protected void WalkAroundTarget(EnemyManager enemy)
        {
            if (enemy.currentRecoveryTime > 0)
            {
                if (!setAroundDirection)
                {
                    horizontalMovementValue = Random.Range(0, 2) - 0.5f; // 随机向左或向右, 0.5f与-0.5f概率对半
                    setAroundDirection = true;
                }
            }
            else
            {
                horizontalMovementValue = 0;
                setAroundDirection = false;
            }

            if (enemy.distFromTarget > ApproachDistance)
                verticalMovementValue = 0.5f;
            else
                verticalMovementValue = 0f;
        }

        /// <summary>
        /// 获取新的攻击行为
        /// </summary>
        /// <param name="enemyManger">敌人管理器</param>
        protected virtual void GetNewAttack(EnemyManager enemy)
        {
            EnemyAttackWindows windows = new EnemyAttackWindows { Attacks = enemyAttacks };
            int index = AttackScore.PickIndex(
                enemyAttacks.Length,
                enemy.distFromTarget,
                enemy.targetDirAngle,
                attackState.currentAttack != null,
                abortIfHasCurrent: true,
                stopAfterAssign: true,
                windows);
            if (index >= 0)
                attackState.currentAttack = enemyAttacks[index];
        }
    }
}