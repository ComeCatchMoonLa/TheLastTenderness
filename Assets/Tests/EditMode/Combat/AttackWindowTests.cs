using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class AttackWindowTests
    {
        [Test]
        public void Open_KeepsTheSwing_Close_ThenTheSameHitStaggers()
        {
            CharacterCombatManager combat = NewCombat(out CharacterStatsManager stats);
            AttackPoiseWindow.Open(stats, 0f);
            Assert.IsFalse(stats.attackPoiseActive);

            stats.currentHP = 100f;
            stats.armorPoiseBonus = 0f;
            int first = AttackPoiseWindow.Open(stats, 30f);
            Assert.IsTrue(stats.attackPoiseActive);
            Assert.AreEqual(30f, stats.attackPoise, 0.001f);

            combat.ResolveIncomingHit(null, null, Physical(10f), true, 10f, 0f);
            Assert.IsTrue(stats.attackPoiseActive);
            Assert.AreEqual(20f, stats.attackPoise, 0.001f);
            Assert.IsFalse(combat.lastHitBrokePoise);
            Assert.AreEqual(90f, stats.currentHP, 0.001f);

            float defence = stats.totalPoiseDefence;
            int second = AttackPoiseWindow.Open(stats, 30f);
            Assert.AreNotEqual(first, second);
            Assert.AreEqual(defence, stats.totalPoiseDefence, 0.001f);
            Assert.AreEqual(30f, stats.attackPoise, 0.001f);
            AttackPoiseWindow.Close(stats, first);
            Assert.IsTrue(stats.attackPoiseActive);
            AttackPoiseWindow.Close(stats, second);
            Assert.IsFalse(stats.attackPoiseActive);
            combat.ResolveIncomingHit(null, null, Physical(10f), true, 10f, 0f);
            Assert.IsTrue(combat.lastHitBrokePoise);
            Assert.AreEqual(80f, stats.currentHP, 0.001f);
        }

        static IncomingDamage Physical(float physical)
        {
            return new IncomingDamage
            {
                Multiplier = 1f,
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
