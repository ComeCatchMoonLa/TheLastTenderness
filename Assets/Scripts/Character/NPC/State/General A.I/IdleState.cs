using UnityEngine;

namespace CatchMoon
{
    public class IdleState : State
    {
        PursueTargetState pursueTargetState;
        
        private void Awake()
        {
            pursueTargetState = GetComponent<PursueTargetState>();
        }

        public override State Tick(EnemyManager enemy)
        {
            if (enemy.eStats.isDead || enemy.isInteracting) return this;

            Collider[] colliders = Physics.OverlapSphere(transform.position, enemy.aiSettings.detectionRadius, LayerMask.player);

            for (int i = 0; i < colliders.Length; ++i)
            {
                CharacterManager targetcharacter = colliders[i].transform.GetComponent<CharacterManager>();

                if (targetcharacter != null && targetcharacter.cStats.teamID != enemy.eStats.teamID)
                {
                    Vector3 targetDirection = targetcharacter.transform.position - transform.position;
                    float viewableAngle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

                    if (viewableAngle > enemy.aiSettings.minViewAngel && viewableAngle < enemy.aiSettings.maxViewAngel)
                    {
                        // aiÊÓÒ°±»µ²
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