using UnityEngine;
using UnityEngine.TextCore.Text;

namespace CatchMoon
{
    public class AttackState : State
    {
        RotateTowardsTargetState rotateTowardsTargetState;
        PursueTargetState pursueTargetState;
        public EnemyAttackAction currentAttack;

        bool willDoComboOnNextAttack = false;
        public bool hasPerformedAttack = false; // 经行了攻击

        private void Awake()
        {
            rotateTowardsTargetState = GetComponent<RotateTowardsTargetState>();
            pursueTargetState = GetComponent<PursueTargetState>();
        }

        public override State Tick(EnemyManager enemy)
        {
            if (enemy.eStats.isDead)
                return this;

            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
                return pursueTargetState;

            RotateTowardsTargrtWhilstAttacking(enemy);
            
            if (!hasPerformedAttack)
            {
                AttackTarget(enemy);
                RollForComboChance(enemy);
            }
            if (willDoComboOnNextAttack)
            {
                if (enemy.canDoCombo)
                    AttackTargetWithCombo(enemy);
                else
                    return this;
            }

            return rotateTowardsTargetState;
        }

        private void AttackTarget(EnemyManager enemy)
        {
            enemy.UpdateWhichHandCharacterIsUsing(usingRightHand: !currentAttack.isLeftHandedAction);
            hasPerformedAttack = true;
            enemy.eAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemy.eEffects.PlayWeaponTrialFX();
            enemy.currentRecoveryTime = currentAttack.recoveryTime; // 重置攻击冷却时间
        }

        private void AttackTargetWithCombo(EnemyManager enemy)
        {
            enemy.UpdateWhichHandCharacterIsUsing(usingRightHand: !currentAttack.isLeftHandedAction);
            willDoComboOnNextAttack = false;
            enemy.eAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemy.eEffects.PlayWeaponTrialFX();
            enemy.currentRecoveryTime = currentAttack.recoveryTime; // 重置攻击冷却时间
            currentAttack = null;
        }

        /// <summary>
        /// [处理] 攻击时旋转(朝向目标)
        /// </summary>
        private void RotateTowardsTargrtWhilstAttacking(EnemyManager enemy)
        {
            AttackFacing.Apply(enemy.transform, transform, ref enemy.targetDir, enemy.canRotate, enemy.isInteracting, enemy.aiSettings.rotationSpeed / Time.deltaTime);
        }

        /// <summary>
        /// 随机决定是否连击
        /// </summary>
        private void RollForComboChance(EnemyManager enemy)
        {
            int comboChance = Random.Range(0, 100);

            if (enemy.aiSettings.allowAIToPerformCombos && comboChance <= enemy.aiSettings.comboLikeHood)
            {
                if (currentAttack.comboAction != null)
                {
                    willDoComboOnNextAttack = true;
                    currentAttack = currentAttack.comboAction;
                }
                else
                {
                    willDoComboOnNextAttack = false;
                    currentAttack = null;
                }
            }
        }
    }
}