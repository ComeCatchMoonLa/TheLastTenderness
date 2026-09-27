using UnityEngine;

namespace CatchMoon
{
    public class IdleStateHumanoid : State
    {
        PursueTargetStateHumanoid pursueTargetState;

        private void Awake()
        {
            pursueTargetState = GetComponent<PursueTargetStateHumanoid>();
        }

        public override State Tick(EnemyManager enemy)
        {
            if (!enemy.enableAI || enemy.eStats.isDead || enemy.isInteracting) return this;

            Collider[] colliders = Physics.OverlapSphere(transform.position, enemy.aiSettings.detectionRadius, LayerMask.player);

            for (int i = 0; i < colliders.Length; ++i)
            {
                CharacterManager targetcharacter = colliders[i].transform.GetComponent<CharacterManager>();

                if (targetcharacter != null && targetcharacter.cStats.teamID != enemy.eStats.teamID)
                {
                    enemy.targetDir = targetcharacter.transform.position - transform.position;
                    enemy.targetDirAngle = Vector3.SignedAngle(enemy.targetDir, transform.forward, Vector3.up);

                    if (enemy.targetDirAngle > enemy.aiSettings.minViewAngel && enemy.targetDirAngle < enemy.aiSettings.maxViewAngel)
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