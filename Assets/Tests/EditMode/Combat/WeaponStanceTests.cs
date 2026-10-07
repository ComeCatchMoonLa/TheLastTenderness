using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class WeaponStanceTests
    {
        [Test]
        public void StanceArt_HoldsThenDerivesOrRecovers_HitOutsidePoiseBreaks()
        {
            Assert.IsFalse(WeaponStance.Enter(false, false, false));
            Assert.IsFalse(WeaponStance.Enter(true, true, false));
            Assert.IsTrue(WeaponStance.Enter(true, false, false));
            Assert.AreEqual(StanceDerive.Light, WeaponStance.TakeSwing(true, true));
            Assert.AreEqual(StanceDerive.Heavy, WeaponStance.TakeSwing(true, false));
            Assert.AreEqual(StanceDerive.None, WeaponStance.TakeSwing(false, true));

            var root = new GameObject("fighter");
            var character = root.AddComponent<CharacterManager>();
            var inventory = root.AddComponent<CharacterInventoryManager>();
            character.cInventory = inventory;
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.stanceArt = true;
            inventory.currentItemBeingUsed = weapon;
            ParryAction art = ScriptableObject.CreateInstance<ParryAction>();

            art.PerformAction(character);
            Assert.IsTrue(character.stanceHolding);
            art.PerformAction(character);
            Assert.IsFalse(character.stanceHolding);
            Assert.AreEqual(StanceDerive.None, character.stanceDerive);

            weapon.stanceArt = false;
            character.isTwoHandingWeapon = true;
            art.PerformAction(character);
            Assert.IsFalse(character.stanceHolding);

            CharacterManager swinger = NewSwinger();
            swinger.cStats.currentStamina = 10f;
            swinger.cStats.currentMP = 40f;
            swinger.stanceHolding = true;
            swinger.isInteracting = true;
            LightAttackAction light = ScriptableObject.CreateInstance<LightAttackAction>();
            light.PerformAction(swinger);
            Assert.IsTrue(swinger.stanceHolding);
            Assert.AreEqual(StanceDerive.None, swinger.stanceDerive);
            Assert.AreEqual(40f, swinger.cStats.currentMP, 0.001f);

            swinger.isInteracting = false;
            light.PerformAction(swinger);
            Assert.IsFalse(swinger.stanceHolding);
            Assert.AreEqual(StanceDerive.Light, swinger.stanceDerive);
            Assert.AreEqual(40f, swinger.cStats.currentMP, 0.001f);

            swinger.stanceHolding = true;
            HeavyAttackAction heavy = ScriptableObject.CreateInstance<HeavyAttackAction>();
            heavy.PerformAction(swinger);
            Assert.IsFalse(swinger.stanceHolding);
            Assert.AreEqual(StanceDerive.Heavy, swinger.stanceDerive);

            CharacterCombatManager combat = NewCombat(out CharacterStatsManager stats, out CharacterManager defender);
            defender.stanceHolding = true;
            stats.currentHP = 100f;
            stats.attackPoise = 30f;
            stats.attackPoiseActive = true;
            combat.ResolveIncomingHit(null, null, Physical(10f), true, 10f, 0f);
            Assert.IsTrue(defender.stanceHolding);

            defender.stanceHolding = true;
            stats.attackPoise = 10f;
            stats.attackPoiseActive = true;
            combat.ResolveIncomingHit(null, null, Physical(10f), true, 10f, 0f);
            Assert.IsFalse(defender.stanceHolding);

            defender.stanceHolding = true;
            stats.attackPoiseActive = false;
            stats.totalPoiseDefence = 0f;
            combat.ResolveIncomingHit(null, null, Physical(10f), true, 10f, 0f);
            Assert.IsFalse(defender.stanceHolding);
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

        static CharacterManager NewSwinger()
        {
            var root = new GameObject("swinger");
            CharacterManager character = root.AddComponent<CharacterManager>();
            CharacterStatsManager stats = root.AddComponent<CharacterStatsManager>();
            CharacterCombatManager combat = root.AddComponent<CharacterCombatManager>();
            CharacterEffectsManager effects = root.AddComponent<CharacterEffectsManager>();
            CharacterAnimatorManager animator = root.AddComponent<CharacterAnimatorManager>();
            character.cStats = stats;
            character.cCombat = combat;
            character.cEffects = effects;
            character.cAnimator = animator;
            SetCharacter(stats, character);
            SetCharacter(combat, character);
            SetCharacter(effects, character);
            SetCharacter(animator, character);
            return character;
        }

        static void SetCharacter(Object target, CharacterManager character)
        {
            target.GetType().GetField("character", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(target, character);
        }
    }
}
