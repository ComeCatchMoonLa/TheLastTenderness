using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Spell/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public float healAmount;
        public float healPerSecond;
        public float regenSeconds;

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
            if (healPerSecond > 0f)
            {
                if (regenSeconds <= 0f)
                {
                    Debug.LogError($"{name}: regenSeconds 未填");
                    return;
                }
                character.cStats.BeginRegen(healPerSecond, regenSeconds);
                return;
            }
            GameObject instantiatedSpellFx = Instantiate(spellCastFX, character.transform);
            character.cStats.AddHP(healAmount);
        }
    }
}