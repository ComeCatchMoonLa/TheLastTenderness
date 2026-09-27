using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Spell/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public float healAmount;

        public override void AttemptToCastSpell(CharacterManager character)
        {
            if (character.cCombat.isUsingSpell) return;

            character.cStats.DeductMP(focusPointCost);
            GameObject instantiatedWarmUpSpellFx = Instantiate(spellWarmUpFX, character.cAnimator.transform);
            character.cAnimator.PlayTargetAnimation(spellAnimation, true, false, mirrorAnim: character.isUsingLeftHand);
        }
        public override void SuccessfullyCastSpell(CharacterManager character)
        {
            GameObject instantiatedSpellFx = Instantiate(spellCastFX, character.transform);
            character.cStats.AddHP(healAmount);
        }
    }
}