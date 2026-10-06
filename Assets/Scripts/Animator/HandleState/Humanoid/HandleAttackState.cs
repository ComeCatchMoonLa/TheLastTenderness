using UnityEngine;
using CatchMoon;

public class HandleAttackState : StateMachineBehaviour
{
    CharacterManager character;
    int poiseToken;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character == null)
            character = animator.GetComponent<CharacterManager>();

        character.cCombat.isAttacking = true;
        poiseToken = 0;
        WeaponItem weapon = AttackWeapon();
        if (weapon != null)
            poiseToken = AttackPoiseWindow.Open(character.cStats, weapon.offensivePoiseBonus);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.cCombat.isAttacking = false;
        AttackPoiseWindow.Close(character.cStats, poiseToken);
        if (character.cEffects.leftWeaponFX != null)
            character.cEffects.leftWeaponFX.StopWeaponTrialFX();
        if (character.cEffects.rightWeaponFX != null)   
            character.cEffects.rightWeaponFX.StopWeaponTrialFX();
    }

    WeaponItem AttackWeapon()
    {
        if (character.cInventory == null) return null;
        if (character.isUsingRightHand) return character.cInventory.rightWeapon;
        if (character.isUsingLeftHand) return character.cInventory.leftWeapon;
        return character.cInventory.currentItemBeingUsed as WeaponItem;
    }
}