using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace CatchMoon.Tests
{
    public class ArmorSwapTests
    {
        [Test]
        public void ReplaceHead_ReturnsOldToBackpackAndPutsNewInSlot()
        {
            var oldArmor = Armor("old");
            var newArmor = Armor("new");
            var inventory = new GameObject("inventory").AddComponent<PlayerInventoryManager>();
            inventory.items = new List<Item> { newArmor };
            inventory.armors = new List<ArmorItem> { newArmor };

            var slotObject = new GameObject("slot");
            var slot = slotObject.AddComponent<EquipmentSlotUI>();
            var icon = slotObject.AddComponent<Image>();
            typeof(EquipmentSlotUI).GetField("icon", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(slot, icon);

            HeadArmorItem equipped = oldArmor;
            InventorySlotUI.ReplaceEquippedArmor(newArmor, ref equipped, slot, inventory);

            Assert.Contains(oldArmor, inventory.armors);
            Assert.IsFalse(inventory.armors.Contains(newArmor));
            Assert.AreSame(newArmor, equipped);
            Assert.AreSame(newArmor, typeof(EquipmentSlotUI).GetField("equipment", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(slot));

            Object.DestroyImmediate(oldArmor);
            Object.DestroyImmediate(newArmor);
            Object.DestroyImmediate(inventory.gameObject);
            Object.DestroyImmediate(slotObject);
        }

        static HeadArmorItem Armor(string name)
        {
            var armor = ScriptableObject.CreateInstance<HeadArmorItem>();
            armor.name = name;
            armor.itemType = ItemType.equipment;
            armor.equipmentType = EquipmentType.armor;
            armor.armorType = ArmorType.head;
            return armor;
        }
    }
}
