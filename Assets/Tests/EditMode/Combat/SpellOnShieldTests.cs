using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SpellOnShieldTests
    {
        [Test]
        public void FrontShield_AbsorbsAndSpendsStamina_UnblockedSpellDoesNot()
        {
            Assert.IsTrue(SpellOnShield.UseShield(true, true));
            Assert.IsFalse(SpellOnShield.UseShield(true, false));
            Assert.IsFalse(SpellOnShield.UseShield(false, true));
            Assert.IsTrue(CharacterCombatManager.ComesFromFront(new Vector3(0f, 0f, 1f), Vector3.zero, Vector3.forward));
            Assert.IsFalse(CharacterCombatManager.ComesFromFront(new Vector3(0f, 0f, -1f), Vector3.zero, Vector3.forward));

            CharacterCombatManager combat = NewCombat(out CharacterStatsManager stats);
            combat.isBlocking = true;
            stats.currentHP = 100f;
            stats.currentStamina = 100f;
            stats.blockingMDA = 0.5f;

            combat.ResolveIncomingHit(null, "Block - Hit", Magic(10f), true, 0f, 1f, true);

            Assert.IsTrue(combat.lastHitWasBlocked);
            Assert.AreEqual(95f, stats.currentHP, 0.001f);
            Assert.AreEqual(90f, stats.currentStamina, 0.001f);

            stats.currentHP = 100f;
            stats.currentStamina = 100f;
            combat.isBlocking = false;
            stats.TakeDamage(null, new DamageSegments { Magic = 10f }, false);
            Assert.AreEqual(90f, stats.currentHP, 0.001f);
            Assert.AreEqual(100f, stats.currentStamina, 0.001f);
        }

        static IncomingDamage Magic(float magic)
        {
            return new IncomingDamage
            {
                Multiplier = 1f,
                Segments = new DamageSegments { Magic = magic }
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
