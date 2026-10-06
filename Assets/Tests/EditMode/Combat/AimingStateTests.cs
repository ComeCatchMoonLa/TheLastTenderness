using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class AimingStateTests
    {
        [Test]
        public void Player_EnterAndExit_ChangesRadiusAndDoesNotWriteIsAiming()
        {
            var go = new GameObject("player");
            var capsule = go.AddComponent<CapsuleCollider>();
            capsule.radius = 0.5f;
            go.AddComponent<Animator>();
            var player = go.AddComponent<PlayerManager>();
            var combat = go.AddComponent<CharacterCombatManager>();
            player.characterType = CharacterType.player;
            player.cCombat = combat;
            player.animator = go.GetComponent<Animator>();
            player.cCollider = capsule;

            var state = ScriptableObject.CreateInstance<HandleAimingState>();
            state.OnStateEnter(player.animator, default, 0);

            Assert.IsFalse(combat.isAiming);
            Assert.AreEqual(0.4f, capsule.radius, 0.001f);
            Assert.IsTrue(player.aimingRadiusApplied);

            state.OnStateExit(player.animator, default, 0);

            Assert.AreEqual(0.5f, capsule.radius, 0.001f);
            Assert.IsFalse(player.aimingRadiusApplied);
            Object.DestroyImmediate(state);
            Object.DestroyImmediate(go);
        }

        [Test]
        public void NonPlayer_EnterAndExit_WritesIsAiming()
        {
            var go = new GameObject("enemy");
            go.AddComponent<Animator>();
            var combat = go.AddComponent<EnemyCombatManager>();
            var enemy = go.AddComponent<EnemyManager>();
            enemy.characterType = CharacterType.npc;
            enemy.cCombat = combat;
            enemy.animator = go.GetComponent<Animator>();

            var state = ScriptableObject.CreateInstance<HandleAimingState>();
            state.OnStateEnter(enemy.animator, default, 0);
            Assert.IsTrue(combat.isAiming);

            state.OnStateExit(enemy.animator, default, 0);
            Assert.IsFalse(combat.isAiming);
            Object.DestroyImmediate(state);
            Object.DestroyImmediate(go);
        }
    }
}
