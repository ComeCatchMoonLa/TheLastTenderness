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
            if (item.itemType != ItemType.equipment) return;

            EquipmentItem equipment = item as EquipmentItem;

            if (equipment.equipmentType == EquipmentType.weapon)
                EquipWeapon(equipment as WeaponItem);
            else if (equipment.equipmentType == EquipmentType.armor)
                EquipArmor(equipment as ArmorItem);
        }

        void EquipWeapon(WeaponItem weapon)
        {
            PlayerInventoryManager pInventory = player.pInventory;
            EquipmentWinManager equipmentWin = player.ui.tapWin.GetEquipmentWin();
            EquipmentSlotType selectedSlotType = player.ui.tapWin.GetEquipmentWin().selectedSlotType;
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

                if (pArmor.currentHeadArmor != null)
                    pInventory.AddItem(pArmor.currentHeadArmor);

                pArmor.currentHeadArmor = headArmor;
                pInventory.RemoveItem(headArmor);
                equipmentWin.GetHeadArmorSlot().AddItem(pArmor.currentHeadArmor);
            }
            if (armor.armorType == ArmorType.torso)
            {
                TorsoArmorItem toroArmor = armor as TorsoArmorItem;

                if (pArmor.currentTorsoArmor != null)
                    pInventory.AddItem(pArmor.currentTorsoArmor);

                pArmor.currentTorsoArmor = toroArmor;
                pInventory.RemoveItem(toroArmor);
                equipmentWin.GetTorsoArmorSlot().AddItem(pArmor.currentTorsoArmor);
            }
            if (armor.armorType == ArmorType.hips)
            {
                HipsArmorItem hipsArmor = armor as HipsArmorItem;

                if (pArmor.currentHipsArmor != null)
                    pInventory.AddItem(pArmor.currentHipsArmor);

                pArmor.currentHipsArmor = hipsArmor;
                pInventory.RemoveItem(hipsArmor);
                equipmentWin.GetHipArmorSlot().AddItem(pArmor.currentHipsArmor);
            }

            // 加载模型
            pArmor.EquipAllArmorModels();
        }
    }
}