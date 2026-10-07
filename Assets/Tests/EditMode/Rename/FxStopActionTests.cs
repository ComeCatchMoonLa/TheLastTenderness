using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class FxStopActionTests
    {
        const string ScriptGuid = "f8ce2664a73f11840a0fe2731eec1cf5";

        [Test]
        public void BloodLastsThreeSeconds_ExplosionLastsFive_WithoutDestroyAuto()
        {
            string scripts = Path.Combine(Application.dataPath, "Scripts", "DestroyAuto.cs");
            Assert.IsFalse(File.Exists(scripts));

            AssertPrefab("Prefabs/FX/BloodSplat_FX.prefab", "lengthInSec: 0.1", "scalar: 2.9");
            AssertPrefab("Prefabs/FX/FX_Explosion.prefab", "lengthInSec: 0.5\n  simulationSpeed: 1\n  stopAction: 2", "scalar: 4.5");
        }

        static void AssertPrefab(string relativePath, string emission, string lifetime)
        {
            string text = File.ReadAllText(Path.Combine(Application.dataPath, relativePath));
            Assert.IsTrue(text.Contains(emission), relativePath);
            Assert.IsTrue(text.Contains(lifetime), relativePath);
            Assert.IsTrue(text.Contains("stopAction: 2"), relativePath);
            Assert.IsFalse(text.Contains("guid: " + ScriptGuid), relativePath);
            Assert.IsFalse(text.Contains("timeUntilDestroyed"), relativePath);
        }
    }
}
