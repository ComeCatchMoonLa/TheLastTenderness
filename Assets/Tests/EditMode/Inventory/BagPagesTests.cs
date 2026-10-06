using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class BagPagesTests
    {
        [Test]
        public void Collect_KeepsOnePageFromMixingWithAnother()
        {
            WeaponItem weapon = ScriptableObject.CreateInstance<WeaponItem>();
            Item consumable = ScriptableObject.CreateInstance<Item>();
            List<Item> bag = new List<Item> { weapon, consumable };
            Item[] into = new Item[2];

            int weapons = BagPages.Collect(bag, BagPage.weapon, into);
            Assert.AreEqual(1, weapons);
            Assert.AreSame(weapon, into[0]);

            int items = BagPages.Collect(bag, BagPage.item, into);
            Assert.AreEqual(1, items);
            Assert.AreSame(consumable, into[0]);
        }
    }
}
