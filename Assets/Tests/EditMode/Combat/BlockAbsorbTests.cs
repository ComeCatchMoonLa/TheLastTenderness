using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class BlockAbsorbTests
    {
        [Test]
        public void HeldBlock_Absorbs_BrokenBlock_KeepsIncoming()
        {
            var incoming = new DamageSegments { Physical = 40f, Fire = 10f };
            var rates = new DamageSegments { Physical = 1f, Fire = 0f };

            DamageSegments held = SpellOnShield.Leak(false, incoming, rates);
            Assert.AreEqual(0f, held.Physical, 0.001f);
            Assert.AreEqual(10f, held.Fire, 0.001f);

            DamageSegments broken = SpellOnShield.Leak(true, incoming, rates);
            Assert.AreEqual(40f, broken.Physical, 0.001f);
            Assert.AreEqual(10f, broken.Fire, 0.001f);
        }

        [Test]
        public void HigherStability_CostsLessStamina()
        {
            float soft = SpellOnShield.StaminaCost(40f, 1f, 0f, 0.2f);
            float firm = SpellOnShield.StaminaCost(40f, 1f, 0f, 0.8f);
            Assert.Less(firm, soft);
        }
    }
}
