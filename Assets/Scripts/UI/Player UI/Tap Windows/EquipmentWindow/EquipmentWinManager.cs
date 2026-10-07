using UnityEngine;

namespace CatchMoon
{
    public class EquipmentWinManager : MonoBehaviour
    {
        PlayerManager player;

        [SerializeField] EquipmentSlotUI[] weaponSlotsUI;
        [SerializeField] EquipmentSlotUI[] armorSlotsUI;
        [SerializeField] GameObject ArmorSlots;

        public EquipmentSlotType selectedSlotType;

        private void Awake()
        {
            player = PlayerUIManager.FindPlayer(this);
        }
        private void Start()
        {
            SetAllSlotIcon();
        }
        public static bool TryHandIndex(EquipmentSlotType slotType, out bool isLeft, out int index)
        {
            isLeft = false;
            index = -1;
            switch (slotType)
            {
                case EquipmentSlotType.weapon_RH_Slot_1: index = 0; return true;
                case EquipmentSlotType.weapon_RH_Slot_2: index = 1; return true;
                case EquipmentSlotType.weapon_RH_Slot_3: index = 2; return true;
                case EquipmentSlotType.weapon_RH_Slot_4: index = 3; return true;
                case EquipmentSlotType.weapon_LH_Slot_1: isLeft = true; index = 0; return true;
                case EquipmentSlotType.weapon_LH_Slot_2: isLeft = true; index = 1; return true;
                case EquipmentSlotType.weapon_LH_Slot_3: isLeft = true; index = 2; return true;
                case EquipmentSlotType.weapon_LH_Slot_4: isLeft = true; index = 3; return true;
                default: return false;
            }
        }

        public EquipmentSlotUI FindHandSlot(EquipmentSlotType slotType)
        {
            if (weaponSlotsUI == null) return null;
            for (int i = 0; i < weaponSlotsUI.Length; i++)
            {
                if (weaponSlotsUI[i] != null && weaponSlotsUI[i].slotType == slotType)
                    return weaponSlotsUI[i];
            }
            return null;
        }

        void SetAllSlotIcon()
        {
            PlayerInventoryManager pInventory = player.pInventory;
            PlayerArmorManager pArmor = player.pArmor;

            if (weaponSlotsUI != null)
            {
                for (int i = 0; i < weaponSlotsUI.Length; i++)
                {
                    EquipmentSlotUI slot = weaponSlotsUI[i];
                    if (slot == null || !TryHandIndex(slot.slotType, out bool isLeft, out int index))
                        continue;
                    WeaponItem[] hand = isLeft ? pInventory.weaponsInLeftHandSlot : pInventory.weaponsInRightHandSlot;
                    if (hand == null || index >= hand.Length || hand[index] == null)
                        slot.ClearItem();
                    else
                        slot.AddItem(hand[index]);
                }
            }

            if (!player.noArmor && pArmor.currentHeadArmor != null)
                armorSlotsUI[0].AddItem(pArmor.currentHeadArmor);

            if (!player.noArmor && pArmor.currentTorsoArmor != null)
                armorSlotsUI[1].AddItem(pArmor.currentTorsoArmor);

            if (!player.noArmor && pArmor.currentHipsArmor != null)
                armorSlotsUI[2].AddItem(pArmor.currentHipsArmor);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void SelectCurrentSlot(EquipmentSlotType equipmentSlotType)
        {
            selectedSlotType = equipmentSlotType;
        }

        public EquipmentSlotUI GetRightHandSlot_1()
        {
            return weaponSlotsUI[0];
        }
        public EquipmentSlotUI GetRightHandSlot_2()
        {
            return weaponSlotsUI[1];
        }
        public EquipmentSlotUI GetLeftHandSlot_1()
        {
            return weaponSlotsUI[2];
        }
        public EquipmentSlotUI GetLeftHandSlot_2()
        {
            return weaponSlotsUI[3];
        }
        public EquipmentSlotUI GetHeadArmorSlot()
        {
            return armorSlotsUI[0];
        }
        public EquipmentSlotUI GetTorsoArmorSlot()
        {
            return armorSlotsUI[1];
        }
        public EquipmentSlotUI GetHipArmorSlot()
        {
            return armorSlotsUI[2];
        }
    }
}