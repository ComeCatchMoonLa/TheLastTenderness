namespace CatchMoon
{
    public class RotateTowardsTargetStateHumanoid : RotateTowardsTargetState
    {
        private void Awake()
        {
            combatStanceState = GetComponent<CombatStanceStateHumanoid>();
            pursueTargetState = GetComponent<PursueTargetState>();
        }
    }
}
