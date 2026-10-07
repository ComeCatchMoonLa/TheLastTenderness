using UnityEngine;

namespace CatchMoon
{
    public class CombatStanceStateHumanoid : State
    {
        PursueTargetStateHumanoid pursueTargetState;
        AttackStateHumanoid attackState;

        public ItemBasedAttackAction[] enemyAttacks;

        protected float verticalMovementValue = 0f;
        protected float horizontalMovementValue = 0f;

        [Header("״̬ Flags")]
        [SerializeField] bool hadRollForChance = false;
        [SerializeField] bool willPerformBlock = false;     // will防御
        [SerializeField] bool willPerformDodge = false;     // will闪避(翻滚)
        [SerializeField] bool willPerformParry = false;     // will弹反

        private void Awake()
        {
            pursueTargetState = GetComponent<PursueTargetStateHumanoid>();
            attackState = GetComponent<AttackStateHumanoid>();
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
            if (!enemy.enableAI || enemy.eStats.isDead) return this;

            if (enemy.aiSettings.combatStyle == NPCCombatStyle.melee)
                return ProcessMeleeCombatStyle(enemy);
            else if (enemy.aiSettings.combatStyle == NPCCombatStyle.archer)
                return ProcessArcherCombatStyle(enemy);

            return this;
        }

        State ProcessMeleeCombatStyle(EnemyManager enemy)
        {
            // 交互中, 停止所有运动
            if (enemy.isInteracting)
            {
                enemy.animator.SetFloat("Vertical", 0);
                enemy.animator.SetFloat("Horizontal", 0);
                return this;
            }

            StopHorizontal(enemy);
            enemy.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemy.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);

            // 在aggroRadius外察觉半径内时追向玩家
            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
            {
                ResetStateFlags();
                return pursueTargetState;
            }

            // 攻击冷却时围绕Player转圈(随机左右)
            DecideCirclingAction(enemy);

            HandleRotateTowardsTarget(enemy);
            
            if (!hadRollForChance)
                RollForChance(enemy);
            // 弹反加反击
            if (enemy.distFromTarget < enemy.cCombat.criticalAttackRange)
            {
                if (willPerformParry)
                    Parry(enemy);
                if (enemy.currentTarget.canBeRiposted)
                    Riposte(enemy);
            }
            // 闪避
            if (willPerformDodge && enemy.distFromTarget < 2f)
                Dodge(enemy);
            // 格挡
            if (willPerformBlock)
                Block(enemy);
            else if (enemy.cCombat.isBlocking)
            {
                enemy.cCombat.isBlocking = false;
                enemy.eAnimator.PlayTargetAnimation("Block - End", false, new AnimationOptions { CanRotate = true });
            }
            // 攻击
            if (enemy.currentRecoveryTime <= 0)
            {
                if (attackState.currentAttack == null)
                {
                    GetNewAttack(enemy);
                }
                if (attackState.currentAttack != null)
                {
                    ResetStateFlags();
                    return attackState;
                }
            }
            return this;
        }

        State ProcessArcherCombatStyle(EnemyManager enemy)
        {
            enemy.animator.SetBool("aimingMode", true);

            // 交互中,停止所有运动
            if (enemy.isInteracting)
            {
                enemy.animator.SetFloat("Vertical", 0);
                enemy.animator.SetFloat("Horizontal", 0);
                return this;
            }

            StopHorizontal(enemy);
            enemy.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemy.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);

            // 在aggroRadius外察觉半径内时追向玩家
            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
                return pursueTargetState;

            // 在玩家比较近的时候, 离开玩家
            if (enemy.currentRecoveryTime <= 0 && attackState.currentAttack != null)
            {
                return attackState;
            }
            else
            {
                RollForChance(enemy);
                if (willPerformDodge)
                    Dodge(enemy);

                GetNewAttack(enemy);
                return this;
            }
        }

        /// <summary>
        /// [处理] 朝向目标
        /// </summary>
        protected void HandleRotateTowardsTarget(EnemyManager enemy)
        {
            if (enemy.targetDir == Vector3.zero)
                enemy.targetDir = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(enemy.targetDir);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, enemy.aiSettings.rotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 决定怎么拉扯
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
                if (!attackState.setAroundDirection)
                {
                    attackState.setAroundDirection = true;
                    // 随机向左或向右, 0.5f与-0.5f概率对半
                    horizontalMovementValue = Random.Range(0, 2) - 0.5f;
                }
                // 攻击冷却中，拉开与玩家的距离
                if (enemy.distFromTarget < 3f)
                    verticalMovementValue = -0.5f;
                else
                    verticalMovementValue = 0f;
            }
            else
            {
                horizontalMovementValue = 0f;
                // 攻击冷却好了却打不到，接近Player
                if (enemy.distFromTarget > CombatStanceState.ApproachDistance)
                    verticalMovementValue = 0.5f;
                else
                    verticalMovementValue = 0;
            }
        }

        /// <summary>
        /// 获取新的攻击行为
        /// </summary>
        /// <param name="enemyManger">敌人管理器</param>
        protected virtual void GetNewAttack(EnemyManager enemy)
        {
            ItemAttackWindows windows = new ItemAttackWindows { Attacks = enemyAttacks };
            int index = AttackScore.PickIndex(
                enemyAttacks.Length,
                enemy.distFromTarget,
                enemy.targetDirAngle,
                attackState.currentAttack != null,
                abortIfHasCurrent: false,
                stopAfterAssign: true,
                windows);
            if (index >= 0)
                attackState.currentAttack = enemyAttacks[index];
        }

        // 根据概率决定是否发生该行为
        void RollForChance(EnemyManager enemy)
        {
            if (!enemy.isInteracting && enemy.currentTarget.cCombat.isAttacking)
            {
                hadRollForChance = true;

                if (enemy.aiSettings.allowAIToPerformBlock)
                    willPerformBlock = (Random.Range(0, 100) <= enemy.aiSettings.blockLikelyHood);
                if (enemy.aiSettings.allowAIToPerformDodge)
                    willPerformDodge = (Random.Range(0, 100) <= enemy.aiSettings.dodgeLikelyHood);
                if (enemy.aiSettings.allowAIToPerformParry)
                    willPerformParry = (Random.Range(0, 100) <= enemy.aiSettings.parryLikelyHood);
            }
        }

        void Block(EnemyManager enemy)
        {
            if (!enemy.eCombat.isBlocking)
            {
                enemy.eInventory.leftWeapon.oh_hold_q_action.PerformAction(enemy);
                
                hadRollForChance = false;
            }
        }

        void Dodge(EnemyManager enemy)
        {
            if (enemy.isRolling) return;
            willPerformDodge = false;

            PlayerManager player = enemy.currentTarget as PlayerManager;

            if (player.aimingMode)
            {
                enemy.transform.LookAt(player.transform);
                // 向敌人方向的左方或右方闪避
                int randomDir = Random.Range(0, 2) * 180 - 90;
                enemy.transform.Rotate(new Vector3(0, randomDir, 0));
            }
            else
            {
                enemy.transform.LookAt(player.transform);
                enemy.transform.Rotate(new Vector3(0, 180, 0));
            }

            enemy.eAnimator.PlayTargetAnimation("Rolling", true);

            hadRollForChance = false;
        }

        void Parry(EnemyManager enemy)
        {
            if (willPerformParry)
            {
                if (enemy.currentTarget.canBeParried)
                {
                    willPerformParry = false;

                    // 转向玩家
                    enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation,
                        Quaternion.LookRotation(enemy.targetDir), enemy.aiSettings.rotationSpeed * Time.deltaTime);
                    enemy.eAnimator.PlayTargetAnimation("Parry", true);

                    hadRollForChance = false;
                }
            }
        }

        void Riposte(EnemyManager enemy)
        {
            if (!enemy.currentTarget.isBeingRiposted && !enemy.isPerformingRiposted)
            {
                enemy.rigidBody.linearVelocity = Vector3.zero;
                enemy.animator.SetFloat("Vertical", 0);
                enemy.eInventory.rightWeapon.oh_hold_e_action.PerformAction(enemy);
            }
        }

        static void StopHorizontal(EnemyManager enemy)
        {
            Vector3 velocity = enemy.rigidBody.linearVelocity;
            velocity.x = 0f;
            velocity.z = 0f;
            enemy.rigidBody.linearVelocity = velocity;
        }

        void ResetStateFlags()
        {
            hadRollForChance = false;

            willPerformParry = false;
            willPerformDodge = false;
            willPerformBlock = false;
        }
    }
}