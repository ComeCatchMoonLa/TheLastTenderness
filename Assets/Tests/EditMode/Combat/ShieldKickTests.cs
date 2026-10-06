using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ShieldKickTests
    {
        [Test]
        public void ForwardWithoutSprint_BreaksShield_SprintDoesNot()
        {
            Assert.IsTrue(ShieldKick.IsKick(false, true));
            Assert.IsFalse(ShieldKick.IsKick(true, true));
            Assert.IsFalse(ShieldKick.IsKick(false, false));

            CharacterCombatManager combat = NewCombat(out CharacterStatsManager stats, out CharacterManager defender);
            CharacterManager attacker = NewAttacker(defender.transform.position + Vector3.forward);
            attacker.kickArmed = true;
            combat.isBlocking = true;
            stats.currentStamina = 40f;
            stats.currentHP = 100f;

            combat.ResolveIncomingHit(attacker, "Block - Hit", Physical(10f), true, 0f, 1f);

            Assert.AreEqual(0f, stats.currentStamina, 0.001f);
            Assert.IsTrue(defender.canBeRiposted);
            Assert.IsTrue(combat.lastHitWasBlocked);

            attacker.kickArmed = false;
            combat.isBlocking = true;
            stats.currentStamina = 40f;
            defender.canBeRiposted = false;
            combat.ResolveIncomingHit(attacker, "Block - Hit", Physical(10f), true, 0f, 1f);
            Assert.AreEqual(30f, stats.currentStamina, 0.001f);
            Assert.IsFalse(defender.canBeRiposted);
        }

        static IncomingDamage Physical(float physical)
        {
            return new IncomingDamage
            {
                Multiplier = 1f,
                Segments = new DamageSegments { Physical = physical }
            };
        }

        static CharacterCombatManager NewCombat(out CharacterStatsManager stats, out CharacterManager character)
        {
            var root = new GameObject("defender");
            character = root.AddComponent<CharacterManager>();
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

        static CharacterManager NewAttacker(Vector3 position)
        {
            var root = new GameObject("attacker");
            root.transform.position = position;
            return root.AddComponent<CharacterManager>();
        }

        static void SetCharacter(Object target, CharacterManager character)
        {
            target.GetType().GetField("character", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(target, character);
        }
    }
}
