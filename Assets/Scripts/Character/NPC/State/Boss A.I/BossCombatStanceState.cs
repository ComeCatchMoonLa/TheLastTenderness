using UnityEngine;

namespace CatchMoon
{
    public class BossCombatStanceState : CombatStanceState
    {
        [Header("Second Phase Attacks")]
        public bool hasPhaseShifted;
        public EnemyAttackAction[] sencondPhaseAttacks;

        protected override void GetNewAttack(EnemyManager enemy)
        {
            if (hasPhaseShifted)
            {
                Vector3 targetsDirection = enemy.currentTarget.transform.position - enemy.transform.position;
                float viewableAngle = Vector3.Angle(targetsDirection, enemy.transform.forward);
                float distanceFromTarget = Vector3.Distance(enemy.currentTarget.transform.position, enemy.transform.position);

                int maxScore = 0;

                for (int i = 0; i < sencondPhaseAttacks.Length; ++i)
                {
                    EnemyAttackAction enemyAttackAction = sencondPhaseAttacks[i];

                    if (distanceFromTarget <= enemyAttackAction.maxDistNeededToAttack && distanceFromTarget >= enemyAttackAction.minDistNeededToAttack)
                        if (viewableAngle <= enemyAttackAction.maxAttackAngle && viewableAngle >= enemyAttackAction.minAttackAngle)
                            maxScore += enemyAttackAction.attackScore;
                }

                int randomValue = Random.Range(0, maxScore);
                int temporaryScore = 0;

                for (int i = 0; i < sencondPhaseAttacks.Length; ++i)
                {
                    EnemyAttackAction enemyAttackAction = sencondPhaseAttacks[i];

                    if (distanceFromTarget <= enemyAttackAction.maxDistNeededToAttack && distanceFromTarget >= enemyAttackAction.minDistNeededToAttack)
                    {
                        if (viewableAngle <= enemyAttackAction.maxAttackAngle && viewableAngle >= enemyAttackAction.minAttackAngle)
                        {
                            if (attackState.currentAttack != null)
                                return;

                            temporaryScore += enemyAttackAction.attackScore;

                            if (temporaryScore > randomValue)
                                attackState.currentAttack = enemyAttackAction;
                        }
                    }
                }
            }
            else
            {
                base.GetNewAttack(enemy);
            }
        }
    }
}