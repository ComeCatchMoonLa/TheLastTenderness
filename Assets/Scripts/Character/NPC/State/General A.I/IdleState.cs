using UnityEngine;

namespace CatchMoon
{
    public class IdleState : State
    {
        public static int SightMask => ~0 & ~(LayerMask.player | LayerMask.npc | LayerMask.characterCollisionBlocker);

        PursueTargetState pursueTargetState;
        Collider[] detectionOverlapResults = new Collider[16];

        private void Awake()
        {
            pursueTargetState = GetComponent<PursueTargetState>();
        }

        public override State Tick(EnemyManager enemy)
        {
            if (!enemy.enableAI || enemy.eStats.isDead || enemy.isInteracting) return this;

            int detectionCount = OverlapQuery.CollectOverlaps(transform.position, enemy.aiSettings.detectionRadius, LayerMask.player, ref detectionOverlapResults);
            for (int i = 0; i < detectionCount; ++i)
            {
                CharacterManager targetcharacter = detectionOverlapResults[i].transform.GetComponent<CharacterManager>();

                if (targetcharacter != null && targetcharacter.cStats.teamID != enemy.eStats.teamID)
                {
                    enemy.targetDir = targetcharacter.transform.position - transform.position;
                    enemy.targetDirAngle = Vector3.SignedAngle(enemy.targetDir, transform.forward, Vector3.up);

                    if (enemy.targetDirAngle > enemy.aiSettings.minViewAngel && enemy.targetDirAngle < enemy.aiSettings.maxViewAngel)
                    {
                        if (Physics.Linecast(enemy.lockOnTransform.position, targetcharacter.lockOnTransform.position,
                            SightMask, QueryTriggerInteraction.Ignore))
                        {
                            return this;
                        }
                        else
                        {
                            enemy.currentTarget = targetcharacter;
                            AllyCall.WakeGroup(enemy, targetcharacter);
                        }
                    }
                }
            }
            if (enemy.currentTarget != null)
            {
                if (!enemy.spotRecorded)
                {
                    enemy.spottedFrom = enemy.transform.position;
                    enemy.spotRecorded = true;
                }
                return pursueTargetState;
            }
            else return this;
        }
    }
}
