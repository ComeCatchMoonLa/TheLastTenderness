using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class DialogueAdvanceTests
    {
        [Test]
        public void ManualSentence_Start_WritesFullLineAndKeepsIt()
        {
            var root = new GameObject("talk-rig");
            var player = root.AddComponent<PlayerManager>();

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
            var npcStats = npcGo.AddComponent<EnemyStatsManager>();
            npcStats.cName = "甲";
            npc.eStats = npcStats;

            talk.UpdateDialogueSBS_Helper(npc);

            Assert.AreEqual("你好", lineText.text);
            Assert.AreEqual("甲", nameText.text);
            Assert.AreEqual(1, talk.talkContent.Count);
            Assert.IsTrue(npc.talkWithSB);
            Assert.IsFalse(player.talkWithSB);

            Object.DestroyImmediate(sentence);
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
