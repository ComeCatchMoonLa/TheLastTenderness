using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class QuickSlotConsumableCountTests
    {
        [Test]
        public void SpendAndRefill_WritesRemainingOnQuickSlot()
        {
            var root = new GameObject("player");
            var player = root.AddComponent<PlayerManager>();
            var inventory = root.AddComponent<PlayerInventoryManager>();
            player.pInventory = inventory;
            typeof(PlayerInventoryManager).GetField("player", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(inventory, player);
            typeof(PlayerInventoryManager).GetField("consumableLeft", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(inventory, new Dictionary<ConsumableItem, int>());

            var uiObject = new GameObject("ui");
            var ui = uiObject.AddComponent<PlayerUIManager>();
            var hudObject = new GameObject("hud");
            var hud = hudObject.AddComponent<HUDWindowsManager>();
            var slotObject = new GameObject("quickSlots");
            var slots = slotObject.AddComponent<QuickSlotsUI>();
            var textObject = new GameObject("count");
            var text = textObject.AddComponent<TextMeshProUGUI>();
            typeof(QuickSlotsUI).GetField("consumableCountText", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(slots, text);

            hud.quickSlotsUI = slots;
            ui.hud = hud;
            player.ui = ui;

            var flask = ScriptableObject.CreateInstance<FlaskItem>();
            flask.maxItemAmount = 3;
            inventory.currentConsumable = flask;

            Assert.IsTrue(inventory.TrySpendConsumable(flask));
            Assert.AreEqual("2", text.text);
            Assert.IsTrue(inventory.TrySpendConsumable(flask));
            Assert.IsTrue(inventory.TrySpendConsumable(flask));
            Assert.AreEqual("0", text.text);

            inventory.RefillConsumablesToMax();
            Assert.AreEqual("3", text.text);

            Object.DestroyImmediate(flask);
            Object.DestroyImmediate(textObject);
            Object.DestroyImmediate(slotObject);
            Object.DestroyImmediate(hudObject);
            Object.DestroyImmediate(uiObject);
            Object.DestroyImmediate(root);
        }
    }
}
