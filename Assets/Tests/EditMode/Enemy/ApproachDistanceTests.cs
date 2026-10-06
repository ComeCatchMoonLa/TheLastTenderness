using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ApproachDistanceTests
    {
        [Test]
        public void BothStances_ReadTheSameApproachDistance()
        {
            Assert.AreEqual(1.4f, CombatStanceState.ApproachDistance);

            string general = Source("General A.I", "CombatStanceState.cs");
            string humanoid = Source("Humanoid A.I", "CombatStanceStateHumanoid.cs");

            Assert.IsTrue(general.Contains("ApproachDistance"));
            Assert.IsTrue(humanoid.Contains("CombatStanceState.ApproachDistance"));
            Assert.IsFalse(humanoid.Contains("1.4f"));
            Assert.IsTrue(humanoid.Contains("3f"));
        }

        static string Source(string folder, string fileName)
        {
            string path = Path.Combine(Application.dataPath, "Scripts", "Character", "NPC", "State", folder, fileName);
            return File.ReadAllText(path);
        }
    }
}
