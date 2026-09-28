using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class DialogueAdvanceTests
    {
        const string MissingController = "Animator does not have an AnimatorController";

        [Test]
        public void ManualSentence_Start_WritesFullLineAndRemovesIt()
        {
            var root = new GameObject("talk-rig");
            var player = root.AddComponent<PlayerManager>();
            player.animator = root.AddComponent<Animator>();
            var ui = root.AddComponent<PlayerUIManager>();
            var esc = root.AddComponent<EscWindowsManager>();
            var settings = root.AddComponent<SettingsWindowManager>();
            var data = ScriptableObject.CreateInstance<GameSettingsData>();
            data.auto = false;
            data.wordForWord = false;
            settings.gameSettingsData = data;
            Set(esc, "settingsWin", settings);
            ui.escWin = esc;
            player.ui = ui;

            var talk = root.AddComponent<TalkUI>();
            Set(talk, "player", player);
            var lineText = root.AddComponent<TextMeshProUGUI>();
            var nameText = new GameObject("name").AddComponent<TextMeshProUGUI>();
            Set(talk, "talkContentText", lineText);
            Set(talk, "talkerNameText", nameText);

            var sentence = ScriptableObject.CreateInstance<DialogueTextData>();
            sentence.text = "你好";
            sentence.isPlayerSaid = false;
            talk.talkContent = new List<DialogueTextData> { sentence };

            var npcGo = new GameObject("npc");
            var npc = npcGo.AddComponent<EnemyManager>();
            npc.animator = npcGo.AddComponent<Animator>();
            var npcStats = npcGo.AddComponent<EnemyStatsManager>();
            npcStats.cName = "甲";
            npc.eStats = npcStats;

            LogAssert.Expect(LogType.Warning, MissingController);
            talk.AdvanceDialogue(npc, true);

            Assert.AreEqual("你好", lineText.text);
            Assert.AreEqual(0, talk.talkContent.Count);

            Object.DestroyImmediate(sentence);
            Object.DestroyImmediate(data);
            Object.DestroyImmediate(nameText.gameObject);
            Object.DestroyImmediate(npcGo);
            Object.DestroyImmediate(root);
        }

        static void Set(Object target, string fieldName, object value)
        {
            var field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            field.SetValue(target, value);
        }
    }
}
