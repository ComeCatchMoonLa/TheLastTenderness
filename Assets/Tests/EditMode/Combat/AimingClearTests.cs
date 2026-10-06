using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class AimingClearTests
    {
        [Test]
        public void JumpRollAndRelease_ClearAimingMode()
        {
            var root = new GameObject("player");
            root.AddComponent<CapsuleCollider>();
            root.AddComponent<InputManager>();
            root.AddComponent<PlayerLocomotionManager>();
            var player = root.AddComponent<PlayerManager>();
            player.cCollider = root.GetComponent<CapsuleCollider>();
            player.pLocomotion = root.GetComponent<PlayerLocomotionManager>();
            var combat = root.AddComponent<PlayerCombatManager>();
            player.pCombat = combat;
            typeof(PlayerCombatManager).GetField("player", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(combat, player);
            typeof(CharacterCombatManager).GetField("character", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(combat, player);
            var animator = root.AddComponent<Animator>();

            player.aimingMode = true;
            ScriptableObject.CreateInstance<HandleJumpState>().OnStateEnter(animator, default, 0);
            Assert.IsFalse(player.aimingMode);

            player.aimingMode = true;
            ScriptableObject.CreateInstance<HandleRollState>().OnStateEnter(animator, default, 0);
            Assert.IsFalse(player.aimingMode);

            player.aimingMode = true;
            player.input = root.GetComponent<InputManager>();
            player.input.hold_q_Input = false;
            MethodInfo release = typeof(PlayerCombatManager).GetMethod(
                "Handle_Hold_Q_Input",
                BindingFlags.Instance | BindingFlags.NonPublic);
            release.Invoke(combat, new object[] { null, null });
            Assert.IsFalse(player.aimingMode);

            Object.DestroyImmediate(root);
        }
    }
}
