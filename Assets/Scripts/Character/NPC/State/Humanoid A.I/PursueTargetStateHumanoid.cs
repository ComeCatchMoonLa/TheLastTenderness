namespace CatchMoon
{
    public class PursueTargetStateHumanoid : PursueTargetState
    {
        private void Awake()
        {
            combatStanceState = GetComponent<CombatStanceStateHumanoid>();
        }
    }
}
