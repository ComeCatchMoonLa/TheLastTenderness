using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class IllusionWallTests
    {
        [Test]
        public void Strike_OpensTheWall_AndRestLeavesItOpen()
        {
            GameObject root = new GameObject("wall");
            IllusionWall wall = root.AddComponent<IllusionWall>();
            BoxCollider col = root.AddComponent<BoxCollider>();
            MeshRenderer renderer = root.AddComponent<MeshRenderer>();

            wall.NotifyContact(false);
            Assert.IsFalse(wall.opened);
            Assert.IsTrue(col.enabled);

            wall.AfterRest();
            Assert.IsFalse(wall.opened);
            Assert.IsTrue(col.enabled);

            wall.NotifyContact(true);
            Assert.IsTrue(wall.opened);
            Assert.IsFalse(col.enabled);
            Assert.IsFalse(renderer.enabled);

            wall.AfterRest();
            Assert.IsTrue(wall.opened);
            Assert.IsFalse(col.enabled);
            Assert.IsFalse(renderer.enabled);

            Object.DestroyImmediate(root);
        }
    }
}
