using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Pyromancy Spell Action")]
    public class PyromancySpellAction : MagicSpellAction
    {
        public override void PerformAction(CharacterManager character)
        {
            PerformSpellAction(character, SpellType.pyromancy);
        }
    }
}