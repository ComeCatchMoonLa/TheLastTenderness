using CatchMoon;
using UnityEngine;

public class HandleAimingState : StateMachineBehaviour
{
    CharacterManager character;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character == null)
            character = animator.GetComponent<CharacterManager>();

        if (character.characterType == CharacterType.player)
        {
            PlayerManager player = character as PlayerManager;
            if (!player.aimingRadiusApplied)
            {
                player.colliderRadiusBeforeAim = player.cCollider.radius;
                player.aimingRadiusApplied = true;
            }
            player.cCollider.radius = 0.4f;
        }
        else
        {
            character.cCombat.isAiming = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character.characterType == CharacterType.player)
        {
            PlayerManager player = character as PlayerManager;
            if (player.aimingRadiusApplied)
            {
                player.cCollider.radius = player.colliderRadiusBeforeAim;
                player.aimingRadiusApplied = false;
            }
        }
        else
        {
            character.cCombat.isAiming = false;
        }
    }
}
