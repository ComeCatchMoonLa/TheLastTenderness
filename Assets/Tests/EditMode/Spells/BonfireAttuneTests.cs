using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class BonfireAttuneTests
    {
        [Test]
        public void Memorize_AwayFromBonfire_LeavesTheBarEmpty()
        {
            GameObject root = new GameObject("attune");
            PlayerInventoryManager inventory = root.AddComponent<PlayerInventoryManager>();
            SpellItem learned = ScriptableObject.CreateInstance<HealingSpell>();
            inventory.spells = new List<SpellItem> { learned };
            inventory.memorized = new List<SpellItem>();

            Assert.IsFalse(inventory.Memorize(learned, false));
            Assert.AreEqual(0, inventory.memorized.Count);

            Object.DestroyImmediate(learned);
            Object.DestroyImmediate(root);
        }
    }
}
