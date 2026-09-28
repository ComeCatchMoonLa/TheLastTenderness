using UnityEngine;
using UnityEngine.UI;

namespace CatchMoon
{
    public class InventorySlotUI : MonoBehaviour
    {
        PlayerManager player;

        [SerializeField] Image icon;
        Item item;

        private void Awake()
        {
            player = transform.root.GetComponent<PlayerManager>();
        }
        private void Start()
        {
            #region 检测空引用异常
            if (player == null)
                Debug.LogError("player == null");
            if (icon == null)
                Debug.LogError("icon == null");
            #endregion

            icon.preserveAspect = true;
        }

        public void AddItem(Item newItem)
        {
            #region 检错
            if (newItem == null)
            {
                Debug.LogError("newItem is null.");
                return;
            }
            #endregion
            
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }
        public void ClearItem()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void EquipThisItem()
        {
            if (item.itemType == ItemType.spell)
            {
                SelectSpell(item as SpellItem);
                return;
            }
            if (item.itemType == ItemType.consumable)
            {
                SelectConsumable(item as ConsumableItem);
                return;
            }
            if (item.itemType != ItemType.equipment) return;

            EquipmentItem equipment = item as EquipmentItem;

            if (equipment.equipmentType == EquipmentType.weapon)
                EquipWeapon(equipment as WeaponItem);
            else if (equipment.equipmentType == EquipmentType.armor)
                EquipArmor(equipment as ArmorItem);
        }

        void SelectSpell(SpellItem spell)
        {
            if (spell == null) return;
            PlayerInventoryManager pInventory = player.pInventory;
            pInventory.currentSpell = spell;
            int index = pInventory.spells.IndexOf(spell);
            if (index >= 0)
                pInventory.currentSpellIdx = index;
            player.ui.hud.quickSlotsUI.UpdateCurrentSpellIcon(spell);
        }

        void SelectConsumable(ConsumableItem consumable)
        {
            if (consumable == null) return;
            PlayerInventoryManager pInventory = player.pInventory;
            pInventory.currentConsumable = consumable;
            int index = pInventory.consumables.IndexOf(consumable);
            if (index >= 0)
                pInventory.currentComsumableIdx = index;
            player.ui.hud.quickSlotsUI.UpdateCurrentConsumableIcon(consumable);
        }

        void EquipWeapon(WeaponItem weapon)
        {
            PlayerInventoryManager pInventory = player.pInventory;
            EquipmentWinManager equipmentWin = player.ui.tapWin.GetEquipmentWin();
            EquipmentSlotType selectedSlotType = player.ui.tapWin.GetEquipmentWin().selectedSlotType;
            bool placed = true;
            // 移除当前装备的物品
            if (selectedSlotType == EquipmentSlotType.weapon_RH_Slot_1)
            {
                pInventory.AddItem(pInventory.weaponsInRightHandSlot[0]);
                pInventory.weaponsInRightHandSlot[0] = weapon;
                equipmentWin.GetRightHandSlot_1().AddItem(pInventory.weaponsInRightHandSlot[0]);
            }
            else if (selectedSlotType == EquipmentSlotType.weapon_RH_Slot_2)
            {
                pInventory.AddItem(pInventory.weaponsInRightHandSlot[1]);
                pInventory.weaponsInRightHandSlot[1] = weapon;
                equipmentWin.GetRightHandSlot_2().AddItem(pInventory.weaponsInRightHandSlot[1]);
            }
            else if (selectedSlotType == EquipmentSlotType.weapon_LH_Slot_1)
            {
                pInventory.AddItem(pInventory.weaponsInLeftHandSlot[0]);
                pInventory.weaponsInLeftHandSlot[0] = weapon;
                equipmentWin.GetLeftHandSlot_1().AddItem(pInventory.weaponsInLeftHandSlot[0]);
            }
            else if (selectedSlotType == EquipmentSlotType.weapon_LH_Slot_2)
            {
                pInventory.AddItem(pInventory.weaponsInLeftHandSlot[1]);
                pInventory.weaponsInLeftHandSlot[1] = weapon;
                equipmentWin.GetLeftHandSlot_2().AddItem(pInventory.weaponsInLeftHandSlot[1]);
            }
            else
            {
                placed = false;
            }

            if (!placed)
                return;

            pInventory.RemoveItem(weapon);

            // 加载模型
            pInventory.rightWeapon = pInventory.weaponsInRightHandSlot[pInventory.currentRightWeaponIdx];
            player.pWeaponSlot.LoadWeaponOnSlot(pInventory.rightWeapon, false);
            // 如果是双手持武器, 则装备武器至武器槽时不再加载左手武器
            if (!player.isTwoHandingWeapon)
            {
                pInventory.leftWeapon = pInventory.weaponsInLeftHandSlot[pInventory.currentLeftWeaponIdx];
                player.pWeaponSlot.LoadWeaponOnSlot(pInventory.leftWeapon, true);
            }
        }
        void EquipArmor(ArmorItem armor)
        {
            if (player.noArmor) return;
            PlayerArmorManager pArmor = player.pArmor;
            PlayerInventoryManager pInventory = player.pInventory;
            EquipmentWinManager equipmentWin = player.ui.tapWin.GetEquipmentWin();

            if (armor.armorType == ArmorType.head)
            {
                HeadArmorItem headArmor = armor as HeadArmorItem;
                ReplaceEquippedArmor(headArmor, ref pArmor.currentHeadArmor, equipmentWin.GetHeadArmorSlot(), pInventory);
            }
            if (armor.armorType == ArmorType.torso)
            {
                TorsoArmorItem toroArmor = armor as TorsoArmorItem;
                ReplaceEquippedArmor(toroArmor, ref pArmor.currentTorsoArmor, equipmentWin.GetTorsoArmorSlot(), pInventory);
            }
            if (armor.armorType == ArmorType.hips)
            {
                HipsArmorItem hipsArmor = armor as HipsArmorItem;
                ReplaceEquippedArmor(hipsArmor, ref pArmor.currentHipsArmor, equipmentWin.GetHipArmorSlot(), pInventory);
            }

            // 加载模型
            pArmor.EquipAllArmorModels();
        }

        public static void ReplaceEquippedArmor<T>(T incoming, ref T equipped, EquipmentSlotUI slot, PlayerInventoryManager inventory)
            where T : ArmorItem
        {
            if (equipped != null)
                inventory.AddItem(equipped);

            equipped = incoming;
            inventory.RemoveItem(incoming);
            slot.AddItem(equipped);
        }
    }
}