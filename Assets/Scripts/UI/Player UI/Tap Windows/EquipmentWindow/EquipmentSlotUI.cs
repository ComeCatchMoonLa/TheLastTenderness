using UnityEngine;
using UnityEngine.UI;

namespace CatchMoon
{
    public class EquipmentSlotUI : MonoBehaviour
    {
        PlayerManager player;

        [SerializeField] EquipmentItem equipment;
        [SerializeField] Image icon;

        public EquipmentSlotType slotType;

        private void Awake()
        {
            player = transform.root.GetComponent<PlayerManager>();
            
        }
        private void Start()
        {
            #region 检错
            if (icon == null)
            {
                Debug.LogError("icon is null.");
                return;
            }
            if (player == null)
            {
                Debug.LogError("player is null.");
                return;
            }
            #endregion

            icon.preserveAspect = true;
        }

        public void AddItem(EquipmentItem newEquipment)
        {
            if (newEquipment == null)
            {
                Debug.LogError("newWeapon is null.");
                return;
            }

            equipment = newEquipment;
            icon.sprite = equipment.itemIcon;
            icon.enabled = true;
        }
        public void ClearItem()
        {
            equipment = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        public void SelectThisSlot()
        {
            player.ui.tapWin.GetEquipmentWin().SelectCurrentSlot(slotType);
            player.ui.tapWin.GetItemStatsWin().UpdateEquipmentItemStats(equipment);
        }
    }
}