using CatchMoon;
using UnityEngine;

public class HandleTurnState : StateMachineBehaviour
{
    EnemyManager enemy;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy == null)
            enemy = animator.GetComponent<EnemyManager>();

        enemy.transform.rotation *= Quaternion.Euler(0, enemy.turnAngle, 0);
    }
}
