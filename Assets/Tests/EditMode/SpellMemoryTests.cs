using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SpellMemoryTests
    {
        [Test]
        public void Next_SkipsUnmemorized_AndReadsItAfterMemorize()
        {
            GameObject root = new GameObject("spells");
            PlayerInventoryManager inventory = root.AddComponent<PlayerInventoryManager>();
            SpellItem learned = ScriptableObject.CreateInstance<HealingSpell>();
            inventory.spells = new List<SpellItem> { learned };
            inventory.memorized = new List<SpellItem>();

            int index = 0;
            Assert.IsNull(SpellMemory.Next(inventory.memorized, ref index));
            Assert.AreEqual(0, index);

            Assert.IsTrue(inventory.Memorize(learned));
            SpellItem next = SpellMemory.Next(inventory.memorized, ref index);
            Assert.AreSame(learned, next);

            Object.DestroyImmediate(learned);
            Object.DestroyImmediate(root);
        }
    }
}
