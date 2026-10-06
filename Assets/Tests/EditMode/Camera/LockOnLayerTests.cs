using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class LockOnLayerTests
    {
        [Test]
        public void LockOverlapUsesNpcLayer_RadiusStays_MeleeRootIsNpc()
        {
            string query = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts", "Camera", "LockOnQuery.cs"));
            Assert.IsTrue(query.Contains("CollectOverlaps(camera.Player.transform.position, maxLockOnDist, LayerMask.npc, ref lockOnOverlapResults)"));
            Assert.IsFalse(query.Contains("~0"));

            string melee = File.ReadAllText(Path.Combine(Application.dataPath, "Prefabs", "Humanoid A.I - Melee.prefab"));
            int nameAt = melee.IndexOf("m_Name: Humanoid A.I - Melee");
            Assert.GreaterOrEqual(nameAt, 0);
            string around = melee.Substring(Mathf.Max(0, nameAt - 200), 240);
            Assert.IsTrue(around.Contains("m_Layer: 9"), around);
        }
    }
}
