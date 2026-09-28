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

            if (!character.cStats.DeductMP(focusPointCost))
                return;

            GameObject instantiatedWarmUpSpellFx = Instantiate(spellWarmUpFX, character.cAnimator.transform);
            character.cAnimator.PlayTargetAnimation(spellAnimation, true, new AnimationOptions { Mirror = character.isUsingLeftHand });
        }
        public override void SuccessfullyCastSpell(CharacterManager character)
        {
            GameObject instantiatedSpellFx = Instantiate(spellCastFX, character.transform);
            character.cStats.AddHP(healAmount);
        }
    }
}