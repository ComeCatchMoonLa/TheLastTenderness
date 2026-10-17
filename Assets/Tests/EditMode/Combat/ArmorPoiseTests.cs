using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ArmorPoiseTests
    {
        [Test]
        public void TwoPiecesOfFifty_FoldToSeventyFive()
        {
            Assert.AreEqual(75f, ArmorPoise.FromPieces(50f, 50f, 0f, 0f), 0.001f);
        }

        [Test]
        public void ArmorRaisesTheWindow_BareDoesNot()
        {
            CharacterCombatManager worn = NewCombat(out CharacterStatsManager wornStats);
            wornStats.currentHP = 100f;
            wornStats.armorPoiseBonus = 40f;
            AttackPoiseWindow.Open(wornStats, 10f);
            worn.ResolveIncomingHit(null, null, Physical(10f), true, 20f, 0f);
            Assert.IsFalse(worn.lastHitBrokePoise);
            Assert.AreEqual(90f, wornStats.currentHP, 0.001f);

            CharacterCombatManager bare = NewCombat(out CharacterStatsManager bareStats);
            bareStats.currentHP = 100f;
            bareStats.armorPoiseBonus = 0f;
            AttackPoiseWindow.Open(bareStats, 10f);
            bare.ResolveIncomingHit(null, null, Physical(10f), true, 20f, 0f);
            Assert.IsTrue(bare.lastHitBrokePoise);
            Assert.AreEqual(90f, bareStats.currentHP, 0.001f);
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
            var root = new GameObject("poise");
            CharacterManager character = root.AddComponent<CharacterManager>();
            stats = root.AddComponent<CharacterStatsManager>();
            CharacterCombatManager combat = root.AddComponent<CharacterCombatManager>();
            CharacterSoundFXManager sound = root.AddComponent<CharacterSoundFXManager>();
            sound.takingDamageSounds = new AudioClip[0];
            character.cStats = stats;
            character.cCombat = combat;
            character.cSoundFX = sound;
            Set(stats, character);
            Set(combat, character);
            return combat;
        }

        static void Set(Object target, CharacterManager character)
        {
            target.GetType().GetField("character", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(target, character);
        }
    }
}
