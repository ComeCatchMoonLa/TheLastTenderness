using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class HandWeaponSelectTests
    {
        [Test]
        public void SelectIndexTwoAndThree_SetsCurrentWeapon()
        {
            PlayerInventoryManager inventory = NewInventory();
            WeaponItem rightTwo = NewWeapon();
            WeaponItem rightThree = NewWeapon();
            WeaponItem leftTwo = NewWeapon();
            WeaponItem leftThree = NewWeapon();
            inventory.weaponsInRightHandSlot[2] = rightTwo;
            inventory.weaponsInRightHandSlot[3] = rightThree;
            inventory.weaponsInLeftHandSlot[2] = leftTwo;
            inventory.weaponsInLeftHandSlot[3] = leftThree;

            Assert.IsTrue(inventory.SelectHandWeapon(false, 2));
            Assert.AreEqual(2, inventory.currentRightWeaponIdx);
            Assert.AreSame(rightTwo, inventory.rightWeapon);

            Assert.IsTrue(inventory.SelectHandWeapon(false, 3));
            Assert.AreEqual(3, inventory.currentRightWeaponIdx);
            Assert.AreSame(rightThree, inventory.rightWeapon);

            Assert.IsTrue(inventory.SelectHandWeapon(true, 2));
            Assert.AreEqual(2, inventory.currentLeftWeaponIdx);
            Assert.AreSame(leftTwo, inventory.leftWeapon);

            Assert.IsTrue(inventory.SelectHandWeapon(true, 3));
            Assert.AreEqual(3, inventory.currentLeftWeaponIdx);
            Assert.AreSame(leftThree, inventory.leftWeapon);
        }

        [Test]
        public void SelectPastArray_LeavesCurrentWeapon()
        {
            PlayerInventoryManager inventory = NewInventory();
            WeaponItem held = NewWeapon();
            inventory.weaponsInRightHandSlot[0] = held;
            inventory.SelectHandWeapon(false, 0);

            Assert.IsFalse(inventory.SelectHandWeapon(false, 4));
            Assert.AreEqual(0, inventory.currentRightWeaponIdx);
            Assert.AreSame(held, inventory.rightWeapon);
        }

        static PlayerInventoryManager NewInventory()
        {
            var root = new GameObject("inventory");
            var inventory = root.AddComponent<PlayerInventoryManager>();
            inventory.weaponsInRightHandSlot = new WeaponItem[4];
            inventory.weaponsInLeftHandSlot = new WeaponItem[4];
            return inventory;
        }

        static WeaponItem NewWeapon()
        {
            return ScriptableObject.CreateInstance<WeaponItem>();
        }
    }
}
