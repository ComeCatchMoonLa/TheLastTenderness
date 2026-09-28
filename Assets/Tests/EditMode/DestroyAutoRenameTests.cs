using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class DestroyAutoRenameTests
    {
        const string ScriptGuid = "f8ce2664a73f11840a0fe2731eec1cf5";

        [Test]
        public void ClassRenamed_GuidAndWaitTimesStay()
        {
            Assert.IsNotNull(typeof(DestroyAuto));
            string scripts = Path.Combine(Application.dataPath, "Scripts");
            Assert.IsTrue(File.Exists(Path.Combine(scripts, "DestroyAuto.cs")));
            Assert.IsFalse(File.Exists(Path.Combine(scripts, "DestoryAuto.cs")));
            string meta = File.ReadAllText(Path.Combine(scripts, "DestroyAuto.cs.meta"));
            Assert.IsTrue(meta.Contains("guid: " + ScriptGuid));

            AssertPrefab("Perfabs/FX/BloodSplat_FX.prefab", "timeUntilDestoryed: 3");
            AssertPrefab("Perfabs/FX/FX_Explosion.prefab", "timeUntilDestoryed: 5");
        }

        static void AssertPrefab(string relativePath, string wait)
        {
            string text = File.ReadAllText(Path.Combine(Application.dataPath, relativePath));
            Assert.IsTrue(text.Contains("guid: " + ScriptGuid), relativePath);
            Assert.IsTrue(text.Contains(wait), relativePath);
            Assert.IsFalse(text.Contains("DestoryAuto"), relativePath);
        }
    }
}
