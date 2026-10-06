using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SpellAttributeGateTests
    {
        [Test]
        public void IntelligenceBelowRequirement_DoesNotAllowCast()
        {
            SpellItem spell = NewSpell(requiredIntelligence: 20, requiredFaith: 0);

            Assert.IsFalse(MagicSpellAction.AttributesAllowCast(spell, 19, 99));
            Assert.IsTrue(MagicSpellAction.AttributesAllowCast(spell, 20, 0));

            Object.DestroyImmediate(spell);
        }

        [Test]
        public void FaithBelowRequirement_DoesNotAllowCast()
        {
            SpellItem spell = NewSpell(requiredIntelligence: 0, requiredFaith: 15);

            Assert.IsFalse(MagicSpellAction.AttributesAllowCast(spell, 99, 14));
            Assert.IsTrue(MagicSpellAction.AttributesAllowCast(spell, 0, 15));

            Object.DestroyImmediate(spell);
        }

        [Test]
        public void ZeroRequirements_DoNotBlock()
        {
            SpellItem spell = NewSpell(requiredIntelligence: 0, requiredFaith: 0);

            Assert.IsTrue(MagicSpellAction.AttributesAllowCast(spell, 1, 1));

            Object.DestroyImmediate(spell);
        }

        static SpellItem NewSpell(int requiredIntelligence, int requiredFaith)
        {
            var spell = ScriptableObject.CreateInstance<HealingSpell>();
            spell.requiredIntelligence = requiredIntelligence;
            spell.requiredFaith = requiredFaith;
            return spell;
        }
    }
}
