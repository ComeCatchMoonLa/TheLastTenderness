using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class StatusPageLinesTests
    {
        [Test]
        public void Format_ListsTheExistingLevelsAndSouls()
        {
            string text = StatusPageLines.Format(10, 11, 12, 13, 14, 15, 16, 17, 80);
            Assert.AreEqual(
                "生命 10\n精力 11\n专注 12\n韧性 13\n力量 14\n敏捷 15\n智力 16\n信仰 17\n灵魂 80",
                text);
            Assert.IsFalse(text.Contains("体力"));
            Assert.IsFalse(text.Contains("幸运"));
            Assert.IsFalse(text.Contains("加点"));
        }

        [Test]
        public void Prefab_StatusButtonOpensTheStatusPage()
        {
            string prefab = File.ReadAllText(Path.Combine(Application.dataPath, "Prefabs", "UI", "Global UI", "Global UI.prefab"));
            Assert.IsTrue(prefab.Contains("m_MethodName: SelectStatusWin"));
            Assert.IsTrue(prefab.Contains("m_text: \"\\u72B6\\u6001\""));
            Assert.IsTrue(prefab.Contains("m_Name: Status Window"));
            Assert.IsFalse(prefab.Contains("m_text: \"\\u6280\\u80FD\""));
        }
    }
}
