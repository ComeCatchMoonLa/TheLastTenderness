using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class ShopTradeTests
    {
        [Test]
        public void BuyAndSell_ChangeSoulsAndBag_OrLeaveBoth()
        {
            Item goods = ScriptableObject.CreateInstance<WeaponItem>();
            List<Item> bag = new List<Item>();

            LogAssert.Expect(LogType.Error, "Shop: price 未填");
            Assert.IsFalse(ShopTrade.Buy("Shop", 100, 0, bag, goods, out int souls));
            Assert.AreEqual(100, souls);
            Assert.AreEqual(0, bag.Count);

            Assert.IsFalse(ShopTrade.Buy("Shop", 40, 50, bag, goods, out souls));
            Assert.AreEqual(40, souls);
            Assert.AreEqual(0, bag.Count);

            Assert.IsTrue(ShopTrade.Buy("Shop", 50, 50, bag, goods, out souls));
            Assert.AreEqual(0, souls);
            Assert.AreEqual(1, bag.Count);

            Assert.IsTrue(ShopTrade.Sell("Shop", souls, 20, bag, goods, out souls));
            Assert.AreEqual(20, souls);
            Assert.AreEqual(0, bag.Count);
        }
    }
}
