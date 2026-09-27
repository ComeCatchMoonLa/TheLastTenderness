using UnityEngine;

namespace CatchMoon
{
    public class IdleState : State
    {
        PursueTargetState pursueTargetState;
        Collider[] detectionOverlapResults = new Collider[16];
        
        private void Awake()
        {
            pursueTargetState = GetComponent<PursueTargetState>();
        }

        int CollectOverlaps(Vector3 position, float radius, int layerMask, ref Collider[] results)
        {
            int count = Physics.OverlapSphereNonAlloc(position, radius, results, layerMask);
            while (count == results.Length)
            {
                results = new Collider[results.Length * 2];
                count = Physics.OverlapSphereNonAlloc(position, radius, results, layerMask);
            }
            return count;
        }

        public override State Tick(EnemyManager enemy)
        {
            if (enemy.eStats.isDead || enemy.isInteracting) return this;

            int detectionCount = CollectOverlaps(transform.position, enemy.aiSettings.detectionRadius, LayerMask.player, ref detectionOverlapResults);
            for (int i = 0; i < detectionCount; ++i)
            {
                CharacterManager targetcharacter = detectionOverlapResults[i].transform.GetComponent<CharacterManager>();

                if (targetcharacter != null && targetcharacter.cStats.teamID != enemy.eStats.teamID)
                {
                    Vector3 targetDirection = targetcharacter.transform.position - transform.position;
                    float viewableAngle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

                    if (viewableAngle > enemy.aiSettings.minViewAngel && viewableAngle < enemy.aiSettings.maxViewAngel)
                    {
                        // ai视野被挡
                        if (Physics.Linecast(enemy.lockOnTransform.position, targetcharacter.lockOnTransform.position,
                            LayerMask.defaultLayerMask | LayerMask.environment, QueryTriggerInteraction.Ignore))
                        {
                            return this;
                        }
                        else
                        {
                            enemy.currentTarget = targetcharacter;
                        }
                    }
                }
            }

            if (enemy.currentTarget != null)
                return pursueTargetState;
            else return this;
        }
    }
}