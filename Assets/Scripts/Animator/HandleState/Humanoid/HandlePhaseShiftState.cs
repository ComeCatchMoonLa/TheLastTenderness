using CatchMoon;
using UnityEngine;

public class HandlePhaseShiftState : StateMachineBehaviour
{
    EnemyManager enemy;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy == null)
            enemy = animator.GetComponent<EnemyManager>();

        enemy.isPhaseShifting = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.isPhaseShifting = false;
    }
}
