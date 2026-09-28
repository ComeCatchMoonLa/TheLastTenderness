namespace CatchMoon
{
    public class RotateTowardsTargetState : State
    {
        protected State combatStanceState;
        protected PursueTargetState pursueTargetState;

        private void Awake()
        {
            if (combatStanceState == null)
                combatStanceState = GetComponent<CombatStanceStateHumanoid>();
            if (combatStanceState == null)
                combatStanceState = GetComponent<CombatStanceState>();
            if (pursueTargetState == null)
                pursueTargetState = GetComponent<PursueTargetState>();
        }

        public override State Tick(EnemyManager enemy)
        {
            if (!enemy.enableAI || enemy.eStats.isDead || enemy.isInteracting) return this;

            enemy.animator.SetFloat("Vertical", 0);
            enemy.animator.SetFloat("Horizontal", 0);

            if (!enemy.isInteracting)
            {
                if (45f < enemy.targetDirAngle)
                {
                    if (135f < enemy.targetDirAngle)
                    {
                        if (enemy.targetDirAngle < 180f)
                        {
                            enemy.eAnimator.PlayTargetAnimation("Turn Around Left", true);
                            enemy.turnAngle = 180;
                        }
                    }
                    else
                    {
                        enemy.eAnimator.PlayTargetAnimation("Turn Left", true);
                        enemy.turnAngle = -90;
                    }
                }
                else if (enemy.targetDirAngle < -45f)
                {
                    if (enemy.targetDirAngle < -135f)
                    {
                        enemy.eAnimator.PlayTargetAnimation("Turn Around Right", true);
                        enemy.turnAngle = 180;
                    }
                    else
                    {
                        enemy.eAnimator.PlayTargetAnimation("Turn Right", true);
                        enemy.turnAngle = 90;
                    }
                }
            }

            if (enemy.distFromTarget > enemy.aiSettings.aggroRadius)
                return pursueTargetState;
            else
                return combatStanceState;
        }
    }
}
