using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class PickupCycleTests
    {
        [Test]
        public void TakenPickup_Hides_NewCycleShowsAgain()
        {
            GameObject body = new GameObject("pickup");
            body.SetActive(false);
            PickUpItem pick = body.AddComponent<PickUpItem>();
            pick.item = ScriptableObject.CreateInstance<Item>();
            body.SetActive(true);

            pick.HideTaken();
            Assert.IsTrue(pick.taken);
            Assert.IsFalse(body.activeSelf);

            pick.ShowForNewCycle();
            Assert.IsFalse(pick.taken);
            Assert.IsTrue(body.activeSelf);

            Object.DestroyImmediate(pick.item);
            Object.DestroyImmediate(body);
        }
    }
}
