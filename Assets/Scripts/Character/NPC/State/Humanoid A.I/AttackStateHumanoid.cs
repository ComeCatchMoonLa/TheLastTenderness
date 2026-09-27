using UnityEngine;

namespace CatchMoon
{
    public class AttackStateHumanoid : State
    {
        PursueTargetStateHumanoid pursueTargetState;
        RotateTowardsTargetStateHumanoid rotateTowardsTargetState;
        public ItemBasedAttackAction currentAttack;

        public bool setAroundDirection = false;   // 设置了围绕玩家旋转的方向
        public bool willDoComboOnNextAttack = false;
        public bool hasPerformedAttack = false;

        private void Awake()
        {
            pursueTargetState = GetComponent<PursueTargetStateHumanoid>();
            rotateTowardsTargetState = GetComponent<RotateTowardsTargetStateHumanoid>();
        }

        public override State Tick(EnemyManager enemy)
        {
            if (!enemy.enableAI || enemy.eStats.isDead) return this;

            if (enemy.aiSettings.combatStyle == NPCCombatStyle.melee)
                return ProcessMeleeCombatStyle(enemy);
            else if (enemy.aiSettings.combatStyle == NPCCombatStyle.archer)
                return ProcessArcherCombatStyle(enemy);

            return this;
        }

        State ProcessMeleeCombatStyle(EnemyManager enemy)
        {
            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
            {
                return pursueTargetState;
            }

            RotateTowardsTargrtWhilstAttacking(enemy);

            if (!hasPerformedAttack)
            {
                if (!enemy.isInteracting)
                {
                    AttackTarget(enemy);
                    RollForComboChance(enemy);
                }
            }
            else
            {
                if (willDoComboOnNextAttack)
                    AttackTargetWithCombo(enemy);
            }
            return rotateTowardsTargetState;
        }

        State ProcessArcherCombatStyle(EnemyManager enemy)
        {
            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
            {
                enemy.animator.SetBool("aimingMode", false);
                return pursueTargetState;
            }

            Vector3 targetPositon = enemy.currentTarget.transform.position;
            targetPositon.y = 0;
            enemy.transform.LookAt(targetPositon);

            if (!hasPerformedAttack)
                AttackTarget(enemy);

            return rotateTowardsTargetState;
        }

        private void AttackTarget(EnemyManager enemy)
        {
            currentAttack.PerformAttackAction(enemy);
            hasPerformedAttack = true;
        }

        private void AttackTargetWithCombo(EnemyManager enemy)
        {
            currentAttack.PerformAttackAction(enemy);
            willDoComboOnNextAttack = false;

            hasPerformedAttack = false;
            setAroundDirection = false;
            enemy.currentRecoveryTime = currentAttack.recoveryTime; // 重置攻击冷却时间
            currentAttack = null;
        }

        /// <summary>
        /// [处理] 攻击时旋转(朝向目标)
        /// </summary>
        private void RotateTowardsTargrtWhilstAttacking(EnemyManager enemy)
        {
            if (enemy.canRotate && enemy.isInteracting)
            {
                if (enemy.targetDir == Vector3.zero)
                    enemy.targetDir = transform.forward;

                Quaternion targetRotation = Quaternion.LookRotation(enemy.targetDir);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, enemy.aiSettings.rotationSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// 随机决定是否连击
        /// </summary>
        private void RollForComboChance(EnemyManager enemy)
        {
            if (enemy.aiSettings.allowAIToPerformCombos)
            {
                int comboChance = Random.Range(0, 100);
                if (comboChance < enemy.aiSettings.comboLikeHood)
                {
                    willDoComboOnNextAttack = true;
                    return;
                }
            }

            hasPerformedAttack = false;
            setAroundDirection = false;
            enemy.currentRecoveryTime = currentAttack.recoveryTime; // 重置攻击冷却时间
            currentAttack = null;
        }
    }
}