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

            if (character is PlayerManager player && !SpellIsMemorized(player, player.pInventory.currentSpell))
            {
                character.cAnimator.PlayTargetAnimation("Shrug", true);
                return;
            }

            SpellItem spell = character.cInventory.currentSpell;
            if (spell != null && spell.spellType == spellType)
            {
                WeaponItem held = character.isUsingLeftHand ? character.cInventory.leftWeapon : character.cInventory.rightWeapon;
                if (held != null && held.catalystKind != CatalystKind.none && !CatalystMatch.Allows(held.catalystKind, spell.spellType))
                {
                    character.cAnimator.PlayTargetAnimation("Shrug", true);
                    return;
                }
                if (!AttributesAllowCast(spell, character.cStats.intelligenceLevel, character.cStats.faithLevel))
                {
                    character.cAnimator.PlayTargetAnimation("Shrug", true);
                    return;
                }

                if (character.cStats.currentMP >= spell.focusPointCost)
                    spell.AttemptToCastSpell(character);
                else
                    character.cAnimator.PlayTargetAnimation("Shrug", true);
            }
        }

        static bool SpellIsMemorized(PlayerManager player, SpellItem spell)
        {
            if (spell == null || player.pInventory.memorized == null) return false;
            for (int i = 0; i < player.pInventory.memorized.Count; i++)
            {
                if (player.pInventory.memorized[i] == spell)
                    return true;
            }
            return false;
        }

        public static bool AttributesAllowCast(SpellItem spell, int intelligence, int faith)
        {
            if (spell == null) return false;
            if (spell.requiredIntelligence > 0 && intelligence < spell.requiredIntelligence) return false;
            if (spell.requiredFaith > 0 && faith < spell.requiredFaith) return false;
            return true;
        }
    }
}