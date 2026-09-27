using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Magic Spell Action")]
    public class MagicSpellAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character) { }

        protected void PerformSpellAction(CharacterManager character, SpellType spellType)
        {
            if (character.isInteracting) return;

            if (character.cInventory.currentSpell != null && character.cInventory.currentSpell.spellType == spellType)
            {
                if (character.cStats.currentMP >= character.cInventory.currentSpell.focusPointCost)
                    character.cInventory.currentSpell.AttemptToCastSpell(character);
                else
                    character.cAnimator.PlayTargetAnimation("Shrug", true);
            }
        }
    }
}