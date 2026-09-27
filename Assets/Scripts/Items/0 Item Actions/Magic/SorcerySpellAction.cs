using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Sorcery Spell Action")]
    public class SorcerySpellAction : MagicSpellAction
    {
        public override void PerformAction(CharacterManager character)
        {
            PerformSpellAction(character, SpellType.sorvery);
        }
    }
}