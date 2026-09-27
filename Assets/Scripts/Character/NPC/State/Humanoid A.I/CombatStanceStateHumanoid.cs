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
        [SerializeField] bool willPerformBlock = false;     // will����
        [SerializeField] bool willPerformDodge = false;     // will����(����)
        [SerializeField] bool willPerformParry = false;     // will����

        private void Awake()
        {
            pursueTargetState = GetComponent<PursueTargetStateHumanoid>();
            attackState = GetComponent<AttackStateHumanoid>();
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
            if (!enemy.enableAI || enemy.eStats.isDead) return this;

            if (enemy.aiSettings.combatStyle == NPCCombatStyle.melee)
                return ProcessMeleeCombatStyle(enemy);
            else if (enemy.aiSettings.combatStyle == NPCCombatStyle.archer)
                return ProcessArcherCombatStyle(enemy);

            return this;
        }

        State ProcessMeleeCombatStyle(EnemyManager enemy)
        {
            // ������, ֹͣ�����˶�
            if (enemy.isInteracting)
            {
                enemy.animator.SetFloat("Vertical", 0);
                enemy.animator.SetFloat("Horizontal", 0);
                return this;
            }

            enemy.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemy.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);

            // ��aggroRadius�����뾶��ʱ׷�����
            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
            {
                ResetStateFlags();
                return pursueTargetState;
            }

            // ������ȴʱΧ��PlayerתȦ(�������)
            DecideCirclingAction(enemy);

            HandleRotateTowardsTarget(enemy);
            
            if (!hadRollForChance)
                RollForChance(enemy);
            // �����ӷ���
            if (enemy.distFromTarget < enemy.cCombat.criticalAttackRange)
            {
                if (willPerformParry)
                    Parry(enemy);
                if (enemy.currentTarget.canBeRiposted)
                    Riposte(enemy);
            }
            // ����
            if (willPerformDodge && enemy.distFromTarget < 2f)
                Dodge(enemy);
            // ��
            if (willPerformBlock)
                Block(enemy);
            else if (enemy.cCombat.isBlocking)
            {
                enemy.cCombat.isBlocking = false;
                enemy.eAnimator.PlayTargetAnimation("Block - End", false, true);
            }
            // ����
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

            // ������,ֹͣ�����˶�
            if (enemy.isInteracting)
            {
                enemy.animator.SetFloat("Vertical", 0);
                enemy.animator.SetFloat("Horizontal", 0);
                return this;
            }

            enemy.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemy.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);

            // ��aggroRadius�����뾶��ʱ׷�����
            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
                return pursueTargetState;

            // ����ұȽϽ���ʱ��, �뿪���
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
        /// [����] ����Ŀ��
        /// </summary>
        protected void HandleRotateTowardsTarget(EnemyManager enemy)
        {
            if (enemy.targetDir == Vector3.zero)
                enemy.targetDir = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(enemy.targetDir);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, enemy.aiSettings.rotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// ������ô����
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
                if (!attackState.setAroundDirection)
                {
                    attackState.setAroundDirection = true;
                    // ������������, 0.5f��-0.5f���ʶ԰�
                    horizontalMovementValue = Random.Range(0, 2) - 0.5f;
                }
                // ������ȴ�У���������ҵľ���
                if (enemy.distFromTarget < 3f)
                    verticalMovementValue = -0.5f;
                else
                    verticalMovementValue = 0f;
            }
            else
            {
                horizontalMovementValue = 0f;
                // ������ȴ����ȴ�򲻵����ӽ�Player
                if (enemy.distFromTarget > 1.4f)
                    verticalMovementValue = 0.5f;
                else
                    verticalMovementValue = 0;
            }
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
                ItemBasedAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemy.distFromTarget <= enemyAttackAction.maxDistNeededToAttack && enemy.distFromTarget >= enemyAttackAction.minDistNeededToAttack)
                    if (enemy.targetDirAngle <= enemyAttackAction.maxAttackAngle && enemy.targetDirAngle >= enemyAttackAction.minAttackAngle)
                        maxScore += enemyAttackAction.attackScore;
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; ++i)
            {
                ItemBasedAttackAction enemyAttackAction = enemyAttacks[i];
                if (enemy.distFromTarget <= enemyAttackAction.maxDistNeededToAttack && enemy.distFromTarget >= enemyAttackAction.minDistNeededToAttack)
                {
                    if (enemy.targetDirAngle <= enemyAttackAction.maxAttackAngle && enemy.targetDirAngle >= enemyAttackAction.minAttackAngle)
                    {
                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            attackState.currentAttack = enemyAttackAction;
                            return;
                        }
                    }
                }
            }
        }

        // ���ݸ��ʾ����Ƿ�������Ϊ
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
                // ����˷�����󷽻��ҷ�����
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

                    // ת�����
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

        void ResetStateFlags()
        {
            hadRollForChance = false;

            willPerformParry = false;
            willPerformDodge = false;
            willPerformBlock = false;
        }
    }
}