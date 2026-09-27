using CatchMoon;
using UnityEngine;

public class HandleRollState : StateMachineBehaviour
{
    CharacterManager character;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character == null)
            character = animator.GetComponent<CharacterManager>();

        character.isRolling = true;

        if (character.characterType == CharacterType.player)
        {
            PlayerManager player = character as PlayerManager;
            player.aimingMode = false;
            player.cCollider.center = player.pLocomotion.rollingColliderCenter;
            player.cCollider.height = player.pLocomotion.rollingColliderHeight;
            player.cCollider.radius = player.pLocomotion.defaultColliderRadius;
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.isRolling = false;
    }
}