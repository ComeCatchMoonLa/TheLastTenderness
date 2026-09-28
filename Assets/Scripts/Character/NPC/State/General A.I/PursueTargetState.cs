using UnityEngine;

namespace CatchMoon
{
    public class PursueTargetState : State
    {
        protected State combatStanceState;

        private void Awake()
        {
            if (combatStanceState == null)
                combatStanceState = GetComponent<CombatStanceStateHumanoid>();
            if (combatStanceState == null)
                combatStanceState = GetComponent<CombatStanceState>();
        }

        public override State Tick(EnemyManager enemy)
        {
            if (!enemy.enableAI || enemy.eStats.isDead || enemy.isInteracting) return this;

            HandleRotateTowardsTarget(enemy);

            if (enemy.isPreformingAction)
            {
                enemy.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return this;
            }

            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
                enemy.animator.SetFloat("Vertical", 1f, 0.1f, Time.deltaTime);

            if (enemy.distFromTarget <= enemy.aiSettings.aggroRadius)
                return combatStanceState;
            else
                return this;
        }

        private void HandleRotateTowardsTarget(EnemyManager enemy)
        {
            if (enemy.isPreformingAction)
            {
                if (enemy.targetDir == Vector3.zero)
                    enemy.targetDir = transform.forward;

                Quaternion targetRotation = Quaternion.LookRotation(enemy.targetDir);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, enemy.aiSettings.rotationSpeed / Time.deltaTime);
            }
            else
            {
                Vector3 targetVelocity = enemy.rigidBody.linearVelocity;
                enemy.navmeshAgent.enabled = true;
                enemy.navmeshAgent.SetDestination(enemy.currentTarget.transform.position);
                enemy.rigidBody.linearVelocity = targetVelocity;
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, enemy.navmeshAgent.transform.rotation, enemy.aiSettings.rotationSpeed / Time.deltaTime);
            }
        }
    }
}
