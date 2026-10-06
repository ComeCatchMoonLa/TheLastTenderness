using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class MerchantAshTests
    {
        [Test]
        public void Death_PutsAshInBag_HandOver_FillsTheNewList_OldShopStaysClosed()
        {
            Item ash = ScriptableObject.CreateInstance<Item>();
            Item goods = ScriptableObject.CreateInstance<WeaponItem>();
            List<Item> bag = new List<Item>();
            List<Item> catalog = new List<Item>();

            Assert.IsTrue(MerchantAsh.TryDrop(ash, bag));
            Assert.IsFalse(MerchantAsh.TryDrop(ash, bag));
            Assert.AreSame(ash, bag[0]);
            Assert.IsFalse(SpellBookStock.CanOpen(false));

            Assert.IsFalse(MerchantAsh.TryHandOver(false, bag, ash, catalog, new[] { goods }));
            Assert.AreEqual(1, bag.Count);
            Assert.AreEqual(0, catalog.Count);

            Assert.IsTrue(MerchantAsh.TryHandOver(true, bag, ash, catalog, new[] { goods }));
            Assert.AreEqual(0, bag.Count);
            Assert.AreSame(goods, catalog[0]);
            Assert.IsFalse(SpellBookStock.CanOpen(false));

            Object.DestroyImmediate(ash);
            Object.DestroyImmediate(goods);
        }
    }
}
