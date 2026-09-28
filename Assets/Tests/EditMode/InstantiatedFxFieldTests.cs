using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class InstantiatedFxFieldTests
    {
        [Test]
        public void NewFieldExists_OldFieldDoesNot_NullRefsStay()
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            Assert.IsNotNull(typeof(PlayerEffectsManager).GetField("instantiatedFXModel", flags));
            Assert.IsNull(typeof(PlayerEffectsManager).GetField("instantialtedFXModel", flags));

            AssertYaml("Perfabs/Player/Player.prefab", 1);
            AssertYaml("Scenes/Game_Before_0.7.unity", 3);
        }

        static void AssertYaml(string relativePath, int nullRefs)
        {
            string text = File.ReadAllText(Path.Combine(Application.dataPath, relativePath));
            Assert.IsFalse(text.Contains("instantialtedFXModel"), relativePath);
            string needle = "instantiatedFXModel: {fileID: 0}";
            int count = 0;
            int from = 0;
            while (true)
            {
                int at = text.IndexOf(needle, from);
                if (at < 0)
                    break;
                count++;
                from = at + needle.Length;
            }
            Assert.AreEqual(nullRefs, count, relativePath);
        }
    }
}
