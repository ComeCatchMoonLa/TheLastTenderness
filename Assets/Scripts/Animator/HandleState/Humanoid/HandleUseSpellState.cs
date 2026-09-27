using CatchMoon;
using UnityEngine;

public class HandleUseSpellState : StateMachineBehaviour
{
    CharacterManager character;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character == null)
            character = animator.GetComponent<CharacterManager>();

        character.cCombat.isUsingSpell = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.cCombat.isUsingSpell = false;
    }
}
