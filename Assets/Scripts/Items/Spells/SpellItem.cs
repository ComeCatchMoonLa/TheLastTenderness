using UnityEngine;

namespace CatchMoon
{
    abstract public class SpellItem : Item
    {
        public GameObject spellWarmUpFX;
        public GameObject spellCastFX;

        public string spellAnimation;

        [Header("Spell Cost")]
        public float focusPointCost;

        [Header("Spell Type")]
        public SpellType spellType;

        [Header("Spell Description")]
        [TextArea] public string spellDescription;

        abstract public void AttemptToCastSpell(CharacterManager character);
        abstract public void SuccessfullyCastSpell(CharacterManager character);
    }
}