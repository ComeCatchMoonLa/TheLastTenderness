using TMPro;
using UnityEngine;

namespace CatchMoon
{
    public class TapWindowsManager : MonoBehaviour
    {
        [Header("被选择的窗口")]
        [SerializeField] TapWinType selectedWin;

        [Header("子窗口")]
        [SerializeField] EquipmentWinManager equipmentWin;
        [SerializeField] InventoryWinManager inventoryWin;
        [SerializeField] ItemStatsWinManager itemStatsWin;
        [SerializeField] StatusWinManager statusWin;

        [Header("子窗口标题文本(用于加粗文本, 凸显当前选择项)")]
        [SerializeField] TextMeshProUGUI selectButton_Equipment_Text;
        [SerializeField] TextMeshProUGUI selectButton_Inventory_Text;
        [SerializeField] TextMeshProUGUI selectButton_Skill_Text;
        [SerializeField] TextMeshProUGUI selectButton_Map_Text;

        private void Start()
        {
            #region 检测空引用异常
            if (equipmentWin == null)
                Debug.LogError("equipmentWin == null");
            if (inventoryWin == null)
                Debug.LogError("inventoryWin == null");
            if (itemStatsWin == null)
                Debug.LogError("itemStatsWin == null");
            #endregion

            HandleWinInit();
        }

        void HandleWinInit()
        {
            if(selectedWin == TapWinType.equipment)
            {
                equipmentWin.Open();
            }
            else if (selectedWin == TapWinType.inventory)
            {
                inventoryWin.Open();
            }
            else if (selectedWin == TapWinType.skill)
            {

            }
            else if (selectedWin == TapWinType.map)
            {

            }
            else if (selectedWin == TapWinType.status)
            {
                if (statusWin != null)
                    statusWin.Open();
            }
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
        public void Switch()
        {
            if (gameObject.activeSelf)
                Close();
            else
                Open();
        }

        public EquipmentWinManager GetEquipmentWin()
        {
            #region 检测空引用异常
            if (equipmentWin == null)
                Debug.LogError("equipmentWin == null");
            #endregion
            return equipmentWin;
        }
        public InventoryWinManager GetInventoryWin()
        {
            #region 检测空引用异常
            if (inventoryWin == null)
                Debug.LogError("inventoryWin == null");
            #endregion
            return inventoryWin;
        }
        public ItemStatsWinManager GetItemStatsWin()
        {
            #region 检测空引用异常
            if (itemStatsWin == null)
                Debug.LogError("itemStatsWin == null");
            #endregion
            return itemStatsWin;
        }

        public void SelectEquipmentWin()
        {
            UnselectCurrentWin();
            selectedWin = TapWinType.equipment;
            selectButton_Equipment_Text.fontStyle = FontStyles.Bold;
            equipmentWin.Open();
        }
        public void SelectInventoryWin()
        {
            UnselectCurrentWin();
            selectedWin = TapWinType.inventory;
            selectButton_Inventory_Text.fontStyle = FontStyles.Bold;
            inventoryWin.Open();
        }
        public void SelectSkillWin()
        {
            UnselectCurrentWin();
            selectedWin = TapWinType.skill;
            selectButton_Skill_Text.fontStyle = FontStyles.Bold;
        }
        public void SelectMapWin()
        {
            UnselectCurrentWin();
            selectedWin = TapWinType.map;
            selectButton_Map_Text.fontStyle = FontStyles.Bold;
        }
        public void SelectStatusWin()
        {
            UnselectCurrentWin();
            selectedWin = TapWinType.status;
            selectButton_Skill_Text.fontStyle = FontStyles.Bold;
            if (statusWin == null)
            {
                Debug.LogError("statusWin == null");
                return;
            }
            statusWin.Open();
        }
        void UnselectCurrentWin()
        {
            if (selectedWin == TapWinType.equipment)
            {
                selectButton_Equipment_Text.fontStyle = FontStyles.Normal;
                equipmentWin.Close();
            }
            else if(selectedWin == TapWinType.inventory)
            {
                selectButton_Inventory_Text.fontStyle = FontStyles.Normal;
                inventoryWin.Close();
            }
            else if (selectedWin == TapWinType.skill)
            {

            }
            else if (selectedWin == TapWinType.map)
            {

            }
            else if (selectedWin == TapWinType.status)
            {
                selectButton_Skill_Text.fontStyle = FontStyles.Normal;
                if (statusWin != null)
                    statusWin.Close();
            }
        }
    }
}