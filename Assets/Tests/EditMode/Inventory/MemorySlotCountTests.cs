using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class MemorySlotCountTests
    {
        [Test]
        public void Thresholds_MatchAttunementTable()
        {
            Assert.AreEqual(0, CharacterStatsManager.MemorySlotCount(9));
            Assert.AreEqual(1, CharacterStatsManager.MemorySlotCount(10));
            Assert.AreEqual(1, CharacterStatsManager.MemorySlotCount(13));
            Assert.AreEqual(2, CharacterStatsManager.MemorySlotCount(14));
            Assert.AreEqual(9, CharacterStatsManager.MemorySlotCount(80));
            Assert.AreEqual(9, CharacterStatsManager.MemorySlotCount(98));
            Assert.AreEqual(10, CharacterStatsManager.MemorySlotCount(99));
        }

        [Test]
        public void FocusFourteen_MaxMpStaysLevelTimesTen()
        {
            var root = new GameObject("stats");
            var stats = root.AddComponent<CharacterStatsManager>();
            stats.focusLevel = 14;
            typeof(CharacterStatsManager).GetMethod("SetMaxFocusPointsFromFocusLevel", BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(stats, null);
            Assert.AreEqual(140f, stats.maxMP);
            Object.DestroyImmediate(root);
        }
    }
}
