using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SpellBookStockTests
    {
        [Test]
        public void HandIn_GrowsTheCatalog_DeathClosesIt_LearnedStays()
        {
            Item book = ScriptableObject.CreateInstance<Item>();
            SpellItem spell = ScriptableObject.CreateInstance<HealingSpell>();
            List<Item> bag = new List<Item> { book };
            List<SpellItem> catalog = new List<SpellItem>();
            List<SpellItem> learned = new List<SpellItem> { spell };

            Assert.IsFalse(SpellBookStock.TryHandIn(bag, book, false, catalog, new[] { spell }));
            Assert.AreEqual(1, bag.Count);
            Assert.AreEqual(0, catalog.Count);

            Assert.IsTrue(SpellBookStock.TryHandIn(bag, book, true, catalog, new[] { spell }));
            Assert.AreEqual(0, bag.Count);
            Assert.AreSame(spell, catalog[0]);
            Assert.IsTrue(SpellBookStock.CanOpen(true));

            Assert.IsFalse(SpellBookStock.CanOpen(false));
            Assert.AreEqual(1, learned.Count);
            Assert.AreSame(spell, learned[0]);

            Object.DestroyImmediate(book);
            Object.DestroyImmediate(spell);
        }
    }
}
