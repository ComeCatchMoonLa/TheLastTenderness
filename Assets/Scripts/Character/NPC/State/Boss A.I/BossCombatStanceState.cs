using UnityEngine;

namespace CatchMoon
{
    public class BossCombatStanceState : CombatStanceState
    {
        [Header("Second Phase Attacks")]
        public bool hasPhaseShifted;
        public float phaseThreshold;
        public bool phaseBlocksHits;
        public EnemyAttackAction[] sencondPhaseAttacks;

        protected override void GetNewAttack(EnemyManager enemy)
        {
            if (hasPhaseShifted)
            {
                Vector3 targetsDirection = enemy.currentTarget.transform.position - enemy.transform.position;
                float viewableAngle = Vector3.Angle(targetsDirection, enemy.transform.forward);
                float distanceFromTarget = Vector3.Distance(enemy.currentTarget.transform.position, enemy.transform.position);

                EnemyAttackWindows windows = new EnemyAttackWindows { Attacks = sencondPhaseAttacks };
                int index = AttackScore.PickIndex(
                    sencondPhaseAttacks.Length,
                    distanceFromTarget,
                    viewableAngle,
                    attackState.currentAttack != null,
                    abortIfHasCurrent: true,
                    stopAfterAssign: true,
                    windows);
                if (index >= 0)
                    attackState.currentAttack = sencondPhaseAttacks[index];
            }
            else
            {
                base.GetNewAttack(enemy);
            }
        }
    }
}