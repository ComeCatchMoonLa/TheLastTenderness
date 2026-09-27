using UnityEngine;
using CatchMoon;

public class HandleJumpState : StateMachineBehaviour
{
    CharacterManager character;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character == null)
            character = animator.GetComponent<CharacterManager>();

        character.isJumping = true;

        if (character.characterType == CharacterType.player)
        {
            PlayerManager player = character as PlayerManager;
            player.aimingMode = false;
            player.cCollider.center = player.pLocomotion.jumpingColliderCenter;
            player.cCollider.height = player.pLocomotion.jumpingColliderHeight;
            player.cCollider.radius = player.pLocomotion.defaultColliderRadius;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.isJumping = false;
    }
}