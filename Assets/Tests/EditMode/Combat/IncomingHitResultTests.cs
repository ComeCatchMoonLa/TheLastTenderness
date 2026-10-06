using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class IncomingHitResultTests
    {
        [Test]
        public void WithoutBlockOrPoise_RecordsDamageTotalOnly()
        {
            CharacterCombatManager combat = NewCombat(out CharacterStatsManager stats);
            stats.currentHP = 100f;

            combat.ResolveIncomingHit(null, null, Physical(1f, 10f), false, 0f, 0f);

            Assert.IsFalse(combat.lastHitWasBlocked);
            Assert.IsFalse(combat.lastHitBrokePoise);
            Assert.AreEqual(10f, combat.lastHitDamageTotal, 0.001f);
            Assert.AreEqual(90f, stats.currentHP, 0.001f);
        }

        [Test]
        public void PoiseSpent_RecordsBreakWithoutBlocking()
        {
            CharacterCombatManager combat = NewCombat(out CharacterStatsManager stats);
            stats.currentHP = 100f;
            stats.totalPoiseDefence = 0f;

            combat.ResolveIncomingHit(null, null, Physical(1f, 10f), true, 5f, 0f);

            Assert.IsFalse(combat.lastHitWasBlocked);
            Assert.IsTrue(combat.lastHitBrokePoise);
            Assert.AreEqual(10f, combat.lastHitDamageTotal, 0.001f);
        }

        [Test]
        public void Multiplier_ScalesDamageBeforeTheTotal()
        {
            CharacterCombatManager combat = NewCombat(out CharacterStatsManager stats);
            stats.currentHP = 100f;

            combat.ResolveIncomingHit(null, null, Physical(2f, 10f), false, 0f, 0f);

            Assert.AreEqual(20f, combat.lastHitDamageTotal, 0.001f);
            Assert.AreEqual(80f, stats.currentHP, 0.001f);
        }

        static IncomingDamage Physical(float multiplier, float physical)
        {
            return new IncomingDamage
            {
                Multiplier = multiplier,
                Segments = new DamageSegments { Physical = physical }
            };
        }

        static CharacterCombatManager NewCombat(out CharacterStatsManager stats)
        {
            var root = new GameObject("fighter");
            var character = root.AddComponent<CharacterManager>();
            stats = root.AddComponent<CharacterStatsManager>();
            var combat = root.AddComponent<CharacterCombatManager>();
            var sound = root.AddComponent<CharacterSoundFXManager>();
            sound.takingDamageSounds = new AudioClip[0];
            character.cStats = stats;
            character.cCombat = combat;
            character.cSoundFX = sound;
            SetCharacter(stats, character);
            SetCharacter(combat, character);
            return combat;
        }

        static void SetCharacter(Object target, CharacterManager character)
        {
            target.GetType().GetField("character", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(target, character);
        }
    }
}
