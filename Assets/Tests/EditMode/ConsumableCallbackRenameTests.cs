using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ConsumableCallbackRenameTests
    {
        [Test]
        public void CallbackRenamed_AnimationEventNameStays()
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            Assert.IsNotNull(typeof(ConsumableItem).GetMethod("SuccessfullyUsedConsumable", flags));
            Assert.IsNull(typeof(ConsumableItem).GetMethod("SucessfullyUsedConsumable", flags));

            string text = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts", "Character", "Player", "PlayerAnimatorManager.cs"));
            Assert.IsTrue(text.Contains("void SuccessfullyCastConsumable()"));
            Assert.IsTrue(text.Contains("used.SuccessfullyUsedConsumable(player)"));
            Assert.IsFalse(text.Contains("SucessfullyUsedConsumable"));
        }
    }
}
