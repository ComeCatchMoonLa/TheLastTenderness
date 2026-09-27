using UnityEngine;

namespace CatchMoon
{
    /// <summary>
    /// 追击状态
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
         *  跟踪目标
         *  if 目标在敌对范围内:
         *      切换至跟战斗状态
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

            // 执行动作时停止移动
            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
                enemy.animator.SetFloat("Vertical", 1f, 0.1f, Time.deltaTime);

            // 在攻击范围内
            if (enemy.distFromTarget <= enemy.aiSettings.aggroRadius)
                return combatStanceState;
            else
                return this;
        }

        /// <summary>
        /// [处理] 朝向目标
        /// </summary>
        private void HandleRotateTowardsTarget(EnemyManager enemy)
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
                Vector3 relativeDirection = transform.InverseTransformDirection(enemy.navmeshAgent.desiredVelocity); // 相对方向
                Vector3 targetVelocity = enemy.rigidBody.velocity;

                enemy.navmeshAgent.enabled = true;
                enemy.navmeshAgent.SetDestination(enemy.currentTarget.transform.position);
                enemy.rigidBody.velocity = targetVelocity;
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, enemy.navmeshAgent.transform.rotation, enemy.aiSettings.rotationSpeed * Time.deltaTime);
            }
        }
    }
}