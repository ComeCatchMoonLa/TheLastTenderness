using UnityEngine;
using CatchMoon;

public class HandleUseComsumableState : StateMachineBehaviour
{
    CharacterManager character;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character == null)
            character = animator.GetComponent<CharacterManager>();

        character.cCombat.isUsingConsumable = true;
    }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            character.cCombat.isUsingConsumable = false;
            if (character is PlayerManager player)
                player.pInventory.consumableBeingUsed = null;
        }
}