using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class BossSoulTradeTests
    {
        [Test]
        public void Exchange_TakesTheSoul_AndAddsOnlyTheChosenItem()
        {
            Item soul = ScriptableObject.CreateInstance<Item>();
            Item chosen = ScriptableObject.CreateInstance<WeaponItem>();
            Item other = ScriptableObject.CreateInstance<WeaponItem>();
            List<Item> bag = new List<Item> { soul };

            Assert.IsFalse(BossSoulTrade.TryExchange(false, bag, soul, chosen));
            Assert.AreSame(soul, bag[0]);
            Assert.AreEqual(1, bag.Count);

            Assert.IsTrue(BossSoulTrade.TryExchange(true, bag, soul, chosen));
            Assert.IsFalse(bag.Contains(soul));
            Assert.IsTrue(bag.Contains(chosen));
            Assert.IsFalse(bag.Contains(other));
            Assert.AreEqual(1, bag.Count);

            Object.DestroyImmediate(soul);
            Object.DestroyImmediate(chosen);
            Object.DestroyImmediate(other);
        }
    }
}
