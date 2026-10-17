using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class PhysicalKindAbsorptionTests
    {
        [Test]
        public void StrikeAndSlash_UseDifferentHeadAbsorption()
        {
            CharacterStatsManager stats = NewStats(100f);
            stats.headArmorStrike = 0.5f;

            stats.TakeDamage(null, new DamageSegments { Physical = 100f, Kind = PhysicalKind.Strike }, false);
            Assert.AreEqual(30f, stats.currentHP, 0.001f);

            stats.currentHP = 100f;
            stats.TakeDamage(null, new DamageSegments { Physical = 100f, Kind = PhysicalKind.Slash }, false);
            Assert.AreEqual(0f, stats.currentHP, 0.001f);
        }

        [Test]
        public void UnfilledSlash_MatchesStandard()
        {
            Assert.AreEqual(0.2f, ArmorPhysical.Of(Armor(0.2f, false, 0.9f), PhysicalKind.Slash), 0.001f);
            Assert.AreEqual(0.9f, ArmorPhysical.Of(Armor(0.2f, true, 0.9f), PhysicalKind.Slash), 0.001f);
            Assert.AreEqual(0f, ArmorPhysical.Of(null, PhysicalKind.Strike), 0.001f);
        }

        static ArmorItem Armor(float physical, bool slashFilled, float slash)
        {
            ArmorItem armor = ScriptableObject.CreateInstance<HeadArmorItem>();
            armor.physicalDA = physical;
            armor.slashFilled = slashFilled;
            armor.slashDA = slash;
            return armor;
        }

        static CharacterStatsManager NewStats(float currentHP)
        {
            var root = new GameObject("kind-stats");
            var character = root.AddComponent<CharacterManager>();
            var stats = root.AddComponent<CharacterStatsManager>();
            character.cStats = stats;
            typeof(CharacterStatsManager).GetField("character", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(stats, character);
            stats.currentHP = currentHP;
            return stats;
        }
    }
}
