using UnityEngine;
using CatchMoon;

public class HandleEmptyState : StateMachineBehaviour
{
    CharacterManager character;

    public string isMirroredBool = "isMirrored";
    public bool isMirroredStatus = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetLayerName(layerIndex) == "Upper Body")
            return;

        if (character == null)
            character = animator.GetComponent<CharacterManager>();

        character.canRotate = true;
        character.isInteracting = false;
        character.isRotatingWithRootMotion = false;

        character.cStats.isInvulnerable = false;
        character.isUsingLeftHand = false;
        character.isUsingRightHand = false;
        character.cCombat.isAttacking = false;
        character.isBeingBackStabbed = false;
        character.isBeingRiposted = false;
        character.isPerformingBackSttbbed = false;
        character.isPerformingRiposted = false;
        character.canBeParried = false;

        animator.SetBool(isMirroredBool, isMirroredStatus);
    }
}
