using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SpellRequirementTests
    {
        [Test]
        public void BelowIntelligenceOrFaith_Refuses()
        {
            SpellItem spell = ScriptableObject.CreateInstance<HealingSpell>();
            spell.requiredIntelligence = 20;
            Assert.IsFalse(MagicSpellAction.AttributesAllowCast(spell, 19, 99));
            spell.requiredIntelligence = 0;
            spell.requiredFaith = 15;
            Assert.IsFalse(MagicSpellAction.AttributesAllowCast(spell, 99, 14));
            Object.DestroyImmediate(spell);
        }

        [Test]
        public void MetOrUnset_Allows()
        {
            SpellItem spell = ScriptableObject.CreateInstance<HealingSpell>();
            spell.requiredIntelligence = 20;
            Assert.IsTrue(MagicSpellAction.AttributesAllowCast(spell, 20, 0));
            spell.requiredIntelligence = 0;
            spell.requiredFaith = 0;
            Assert.IsTrue(MagicSpellAction.AttributesAllowCast(spell, 1, 1));
            Object.DestroyImmediate(spell);
        }
    }
}
