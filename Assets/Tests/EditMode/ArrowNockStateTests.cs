using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ArrowNockStateTests
    {
        [Test]
        public void NockCheckUsesStateHash_GetArrowMethodStays()
        {
            string effects = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts", "Character", "Player", "PlayerEffectsManager.cs"));
            Assert.IsTrue(effects.Contains("Animator.StringToHash(\"Get Arrow\")"));
            Assert.IsTrue(effects.Contains("Animator.StringToHash(\"Aiming - Start\")"));
            Assert.IsTrue(effects.Contains("shortNameHash"));
            Assert.IsTrue(effects.Contains("player.aimingMode"));
            Assert.IsFalse(effects.Contains("IsName"));

            string animator = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts", "Character", "CharacterAnimatorManager.cs"));
            Assert.IsTrue(animator.Contains("void SucessfullyGetArrow()"));
        }
    }
}
