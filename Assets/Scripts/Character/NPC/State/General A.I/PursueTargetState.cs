using UnityEngine;

namespace CatchMoon
{
    public class PursueTargetState : State
    {
        protected State combatStanceState;
        IdleState idleState;

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

            if (enemy.LeftActivity())
            {
                enemy.returningHome = true;
                enemy.returnToPlacement = true;
            }
            else if (enemy.aiSettings != null && Leash.TooFar(enemy.distFromTarget, enemy.aiSettings.detectionRadius))
                enemy.returningHome = true;
            if (enemy.returningHome)
                return TickReturn(enemy);

            HandleRotateTowardsTarget(enemy, enemy.currentTarget.transform.position);

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

        State TickReturn(EnemyManager enemy)
        {
            float arrive = enemy.aiSettings != null ? enemy.aiSettings.returnArrive : 0f;
            Vector3 home = enemy.returnToPlacement ? enemy.placement : enemy.spottedFrom;
            float homeDist = Vector3.Distance(enemy.transform.position, home);
            if (Leash.Arrived(enemy.name, homeDist, arrive, ref enemy.returnArriveWarned))
            {
                if (enemy.returnToPlacement && enemy.eStats != null)
                    enemy.eStats.currentHP = Territory.Refill(enemy.eStats.currentHP, enemy.eStats.maxHP);
                enemy.returningHome = false;
                enemy.returnToPlacement = false;
                enemy.spotRecorded = false;
                enemy.currentTarget = null;
                enemy.animator.SetFloat("Vertical", 0f);
                if (enemy.navmeshAgent != null)
                    enemy.navmeshAgent.enabled = false;
                if (idleState == null)
                    idleState = GetComponent<IdleState>();
                return idleState != null ? idleState : this;
            }

            if (enemy.isPreformingAction)
            {
                enemy.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return this;
            }

            enemy.animator.SetFloat("Vertical", 1f, 0.1f, Time.deltaTime);
            WalkToward(enemy, enemy.returnToPlacement ? enemy.placement : enemy.spottedFrom);
            return this;
        }

        private void HandleRotateTowardsTarget(EnemyManager enemy, Vector3 destination)
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
                WalkToward(enemy, destination);
            }
        }

        void WalkToward(EnemyManager enemy, Vector3 destination)
        {
            enemy.navmeshAgent.enabled = true;
            enemy.navmeshAgent.SetDestination(destination);
            Vector3 velocity = enemy.navmeshAgent.desiredVelocity;
            velocity.y = enemy.rigidBody.linearVelocity.y;
            enemy.rigidBody.linearVelocity = velocity;
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, enemy.navmeshAgent.transform.rotation, enemy.aiSettings.rotationSpeed / Time.deltaTime);
        }
    }
}
