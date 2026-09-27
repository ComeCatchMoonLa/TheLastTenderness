using UnityEngine;
using CatchMoon;

public class HandleAttackState : StateMachineBehaviour
{
    CharacterManager character;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character == null)
            character = animator.GetComponent<CharacterManager>();

        character.cCombat.isAttacking = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.cCombat.isAttacking = false;
        if (character.cEffects.leftWeaponFX != null)
            character.cEffects.leftWeaponFX.StopWeaponTrialFX();
        if (character.cEffects.rightWeaponFX != null)   
            character.cEffects.rightWeaponFX.StopWeaponTrialFX();
    }
}