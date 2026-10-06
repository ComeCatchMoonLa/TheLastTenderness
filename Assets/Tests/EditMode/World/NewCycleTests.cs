using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class NewCycleTests
    {
        [Test]
        public void Next_KeepsTheLevel_AndClearsTheWorldFlags()
        {
            NewCycle.Snapshot next = NewCycle.Next(new NewCycle.Snapshot
            {
                level = 10,
                fireLit = true,
                doorOpen = true,
                bossDown = true,
                carryingKey = true
            });
            Assert.AreEqual(10, next.level);
            Assert.IsFalse(next.fireLit);
            Assert.IsFalse(next.doorOpen);
            Assert.IsFalse(next.bossDown);
            Assert.IsFalse(next.carryingKey);
        }

        [Test]
        public void StripKeys_RemovesOnlyKeys()
        {
            Item key = ScriptableObject.CreateInstance<Item>();
            key.isKey = true;
            Item sword = ScriptableObject.CreateInstance<Item>();
            var bag = new List<Item> { sword, key };

            Assert.AreEqual(1, NewCycle.StripKeys(bag));
            Assert.AreEqual(1, bag.Count);
            Assert.IsFalse(bag[0].isKey);

            Object.DestroyImmediate(key);
            Object.DestroyImmediate(sword);
        }
    }
}
