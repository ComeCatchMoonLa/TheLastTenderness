using CatchMoon;
using UnityEngine;

public class HandleAimingState : StateMachineBehaviour
{
    CharacterManager character;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character == null)
            character = animator.GetComponent<CharacterManager>();

        character.cCombat.isAiming = true;

        if (character.characterType == CharacterType.player)
        {
            PlayerManager player = character as PlayerManager;
            player.cCollider.radius = 0.4f;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.cCombat.isAiming = false;
    }
}
