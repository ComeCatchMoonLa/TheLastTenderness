namespace CatchMoon
{
    public class RotateTowardsTargetStateHumanoid : State
    {
        CombatStanceStateHumanoid combatStanceState;
        PursueTargetStateHumanoid pursueTargetState;

        private void Awake()
        {
            combatStanceState = GetComponent<CombatStanceStateHumanoid>();
            pursueTargetState = GetComponent<PursueTargetStateHumanoid>();
        }

        public override State Tick(EnemyManager enemy)
        {
            if (!enemy.enableAI || enemy.eStats.isDead || enemy.isInteracting) return this;

            // 停止移动
            enemy.animator.SetFloat("Vertical", 0);
            enemy.animator.SetFloat("Horizontal", 0);

            // 若玩家不在敌人视野内, 且敌人不在交互中, 则敌人会向后转身
            if (!enemy.isInteracting)
            {
                if (45f < enemy.targetDirAngle)
                {
                    if (135f < enemy.targetDirAngle)
                    {
                        if (enemy.targetDirAngle < 180f)
                        {
                            // (135, 180)
                            enemy.eAnimator.PlayTargetAnimation("Turn Around Left", true);
                            enemy.turnAngle = 180;
                        }
                    }
                    else
                    {
                        // (45, 135]
                        enemy.eAnimator.PlayTargetAnimation("Turn Left", true);
                        enemy.turnAngle = -90;
                    }
                }
                else if (enemy.targetDirAngle < -45f)
                {
                    if (enemy.targetDirAngle < -135f)
                    {
                        // [-180, -135)
                        enemy.eAnimator.PlayTargetAnimation("Turn Around Right", true);
                        enemy.turnAngle = 180;
                    }
                    else
                    {
                        // [-135, -45)
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