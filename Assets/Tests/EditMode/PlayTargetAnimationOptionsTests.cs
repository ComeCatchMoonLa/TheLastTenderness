using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class PlayTargetAnimationOptionsTests
    {
        [Test]
        public void MissingState_LogsNameAndLeavesInteractingFalse()
        {
            CharacterAnimatorManager animator = NewAnimator(out CharacterManager character, withState: false);
            LogAssert.Expect(LogType.Error, new Regex("actor: Animator 没有状态 Missing"));

            animator.PlayTargetAnimation("Missing", true);

            Assert.IsFalse(character.isInteracting);
        }

        [Test]
        public void DefaultOptions_FollowInteractingOnly()
        {
            CharacterAnimatorManager animator = NewAnimator(out CharacterManager character, withState: true);

            animator.PlayTargetAnimation("Hit", true);

            Assert.IsTrue(character.isInteracting);
            Assert.IsTrue(character.animator.applyRootMotion);
            Assert.IsFalse(character.canRotate);
            Assert.IsFalse(character.animator.GetBool("isMirrored"));
            Assert.IsFalse(character.isRotatingWithRootMotion);
        }

        [Test]
        public void AllOptions_WriteTheThreeFlags()
        {
            CharacterAnimatorManager animator = NewAnimator(out CharacterManager character, withState: true);

            animator.PlayTargetAnimation("Hit", false, new AnimationOptions
            {
                CanRotate = true,
                Mirror = true,
                RootMotion = true
            });

            Assert.IsFalse(character.isInteracting);
            Assert.IsFalse(character.animator.applyRootMotion);
            Assert.IsTrue(character.canRotate);
            Assert.IsTrue(character.animator.GetBool("isMirrored"));
            Assert.IsTrue(character.isRotatingWithRootMotion);
        }

        static CharacterAnimatorManager NewAnimator(out CharacterManager character, bool withState)
        {
            var root = new GameObject("actor");
            character = root.AddComponent<CharacterManager>();
            var unityAnimator = root.AddComponent<Animator>();
            var manager = root.AddComponent<CharacterAnimatorManager>();
            character.animator = unityAnimator;
            character.cAnimator = manager;
            typeof(CharacterAnimatorManager).GetField("character", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(manager, character);
            if (withState)
            {
                var controller = new AnimatorController();
                controller.AddParameter("isMirrored", AnimatorControllerParameterType.Bool);
                controller.AddLayer("Base Layer");
                AnimatorControllerLayer[] layers = controller.layers;
                layers[0].stateMachine.AddState("Hit");
                controller.layers = layers;
                unityAnimator.runtimeAnimatorController = controller;
            }

            return manager;
        }
    }
}
