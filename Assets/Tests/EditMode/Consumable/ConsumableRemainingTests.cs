using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ConsumableRemainingTests
    {
        [Test]
        public void NullItem_ReturnsZero()
        {
            PlayerInventoryManager inventory = NewInventory();

            Assert.AreEqual(0, inventory.ConsumableRemaining(null));
        }

        [Test]
        public void FirstRead_UsesMaxAmount_SpendLeavesZero()
        {
            PlayerInventoryManager inventory = NewInventory();
            var flask = ScriptableObject.CreateInstance<FlaskItem>();
            flask.maxItemAmount = 1;

            Assert.AreEqual(1, inventory.ConsumableRemaining(flask));
            Assert.IsTrue(inventory.TrySpendConsumable(flask));
            Assert.AreEqual(0, inventory.ConsumableRemaining(flask));

            Object.DestroyImmediate(flask);
        }

        static PlayerInventoryManager NewInventory()
        {
            var root = new GameObject("inventory");
            var inventory = root.AddComponent<PlayerInventoryManager>();
            FieldInfo field = typeof(PlayerInventoryManager).GetField("consumableLeft", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field.GetValue(inventory) == null)
                field.SetValue(inventory, new Dictionary<ConsumableItem, int>());
            return inventory;
        }
    }
}
