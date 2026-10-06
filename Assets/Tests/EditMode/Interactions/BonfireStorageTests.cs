using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class BonfireStorageTests
    {
        [Test]
        public void DepositAndWithdraw_MoveTheSameItem_EquippedStaysInBag()
        {
            Item item = ScriptableObject.CreateInstance<WeaponItem>();
            List<Item> bag = new List<Item> { item };
            List<Item> box = new List<Item>();

            Assert.IsFalse(BonfireStorage.TryDeposit(box, bag, item, true));
            Assert.AreEqual(1, bag.Count);
            Assert.AreEqual(0, box.Count);

            Assert.IsTrue(BonfireStorage.TryDeposit(box, bag, item, false));
            Assert.AreEqual(0, bag.Count);
            Assert.AreSame(item, box[0]);

            List<Item> otherFireBag = new List<Item>();
            Assert.IsTrue(BonfireStorage.TryWithdraw(box, otherFireBag, item));
            Assert.AreEqual(0, box.Count);
            Assert.AreSame(item, otherFireBag[0]);
        }
    }
}
