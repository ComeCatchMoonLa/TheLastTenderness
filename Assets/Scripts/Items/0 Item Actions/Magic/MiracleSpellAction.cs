using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Miracle Spell Action")]
    public class MiracleSpellAction : MagicSpellAction
    {
        public override void PerformAction(CharacterManager character)
        {
            PerformSpellAction(character, SpellType.miracle);
        }
    }
}