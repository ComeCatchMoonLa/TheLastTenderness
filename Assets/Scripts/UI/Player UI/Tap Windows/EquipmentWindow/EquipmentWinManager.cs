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
            player = transform.root.GetComponent<PlayerManager>();
        }
        private void Start()
        {
            SetAllSlotIcon();
        }
        void SetAllSlotIcon()
        {
            PlayerInventoryManager pInventory = player.pInventory;
            PlayerArmorManager pArmor = player.pArmor;

            if (pInventory.weaponsInRightHandSlot[0] != null)
                weaponSlotsUI[0].AddItem(pInventory.weaponsInRightHandSlot[0]);

            if (pInventory.weaponsInRightHandSlot[1] != null)
                weaponSlotsUI[1].AddItem(pInventory.weaponsInRightHandSlot[1]);

            if (pInventory.weaponsInLeftHandSlot[0] != null)
                weaponSlotsUI[2].AddItem(pInventory.weaponsInLeftHandSlot[0]);

            if (pInventory.weaponsInLeftHandSlot[1] != null)
                weaponSlotsUI[3].AddItem(pInventory.weaponsInLeftHandSlot[1]);

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