using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace CatchMoon.Tests
{
    public class RestRefillConsumableTests
    {
        [Test]
        public void RestoreVitalsToMax_RefillsSpentConsumableToMax()
        {
            var root = new GameObject("player");
            var player = root.AddComponent<PlayerManager>();
            var stats = root.AddComponent<PlayerStatsManager>();
            var inventory = root.AddComponent<PlayerInventoryManager>();
            typeof(PlayerStatsManager).GetField("player", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(stats, player);
            typeof(PlayerInventoryManager).GetField("consumableLeft", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(inventory, new Dictionary<ConsumableItem, int>());
            player.pInventory = inventory;
            player.pStats = stats;
            player.ui = BarUi();

            var flask = ScriptableObject.CreateInstance<FlaskItem>();
            flask.maxItemAmount = 3;
            Assert.AreEqual(3, inventory.ConsumableRemaining(flask));
            Assert.IsTrue(inventory.TrySpendConsumable(flask));
            Assert.IsTrue(inventory.TrySpendConsumable(flask));
            Assert.AreEqual(1, inventory.ConsumableRemaining(flask));

            stats.RestoreVitalsToMax();

            Assert.AreEqual(3, inventory.ConsumableRemaining(flask));
            Object.DestroyImmediate(flask);
            Object.DestroyImmediate(root);
        }

        static PlayerUIManager BarUi()
        {
            var uiObject = new GameObject("ui");
            var ui = uiObject.AddComponent<PlayerUIManager>();
            var hudObject = new GameObject("hud");
            var hud = hudObject.AddComponent<HUDWindowsManager>();
            hud.healthBar = Bar<HealthBar>();
            hud.staminaBar = Bar<StaminaBar>();
            hud.manaBar = Bar<FocusPointsBar>();
            ui.hud = hud;
            return ui;
        }

        static T Bar<T>() where T : BaseBar
        {
            var barObject = new GameObject(typeof(T).Name);
            var slider = barObject.AddComponent<Slider>();
            var bar = barObject.AddComponent<T>();
            bar.slider = slider;
            return bar;
        }
    }
}
