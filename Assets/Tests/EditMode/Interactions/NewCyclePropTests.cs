using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class NewCyclePropTests
    {
        [Test]
        public void ChestStaysOpenAfterRest_NewCycleCloses()
        {
            Assert.IsTrue(ChestLoot.AfterRest(true));
            Assert.IsFalse(ChestLoot.ForNewCycle(true));
        }

        [Test]
        public void IllusionWall_CloseForNewCycle_EnablesCollider()
        {
            GameObject body = new GameObject("wall");
            IllusionWall wall = body.AddComponent<IllusionWall>();
            BoxCollider collider = body.AddComponent<BoxCollider>();
            wall.opened = true;
            collider.enabled = false;

            wall.CloseForNewCycle();

            Assert.IsFalse(wall.opened);
            Assert.IsTrue(collider.enabled);
            Object.DestroyImmediate(body);
        }
    }
}
