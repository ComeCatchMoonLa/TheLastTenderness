using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class AddSoulsHudTests
    {
        [Test]
        public void AddSouls_WritesSoulCountAndHudText()
        {
            var root = new GameObject("player");
            var player = root.AddComponent<PlayerManager>();
            var stats = root.AddComponent<PlayerStatsManager>();
            FieldInfo playerField = typeof(PlayerStatsManager).GetField("player", BindingFlags.Instance | BindingFlags.NonPublic);
            if (playerField.GetValue(stats) == null)
                playerField.SetValue(stats, player);

            var uiObject = new GameObject("ui");
            var ui = uiObject.AddComponent<PlayerUIManager>();
            var hudObject = new GameObject("hud");
            var hud = hudObject.AddComponent<HUDWindowsManager>();
            var soulObject = new GameObject("soul");
            var textObject = new GameObject("text");
            textObject.transform.SetParent(soulObject.transform);
            var text = textObject.AddComponent<TextMeshProUGUI>();
            var soulCountUI = soulObject.AddComponent<SoulCountUI>();
            typeof(SoulCountUI).GetField("soulCntText", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(soulCountUI, text);

            hud.soulCountUI = soulCountUI;
            ui.hud = hud;
            player.ui = ui;

            stats.AddSouls(5);

            Assert.AreEqual(5, stats.soulCount);
            Assert.AreEqual("5", text.text);

            Object.DestroyImmediate(root);
            Object.DestroyImmediate(uiObject);
            Object.DestroyImmediate(hudObject);
            Object.DestroyImmediate(soulObject);
        }
    }
}
