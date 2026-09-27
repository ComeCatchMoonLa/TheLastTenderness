using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class InventoryWinManager : MonoBehaviour
    {
        PlayerManager player;

        [Header("被选择的窗口")]
        [SerializeField] InventoryWinType selectedWin;

        [Header("武器库存槽")]
        [SerializeField] Transform weaponInventorySlotsParent;
        [SerializeField] GameObject weaponInventorySlotPerfab;
        [SerializeField] List<InventorySlotUI> weaponInventorySlots;

        [Header("盔甲库存槽")]
        [SerializeField] Transform armorInventorySlotsParent;
        [SerializeField] GameObject armorInventorySlotPerfab;
        [SerializeField] List<InventorySlotUI> armorInventorySlots;

        [Header("法术库存槽")]
        [SerializeField] Transform spellInventorySlotsParent;
        [SerializeField] GameObject spellInventorySlotPerfab;
        [SerializeField] List<InventorySlotUI> spellInventorySlots;

        [Header("消耗品库存槽")]
        [SerializeField] Transform consumableInventorySlotsParent;
        [SerializeField] GameObject consumableInventorySlotPerfab;
        [SerializeField] List<InventorySlotUI> consumableInventorySlots;

        private void Awake()
        {
            player = transform.root.GetComponent<PlayerManager>();
            weaponInventorySlots.Add(weaponInventorySlotsParent.GetComponentInChildren<InventorySlotUI>());
            armorInventorySlots.Add(armorInventorySlotsParent.GetComponentInChildren<InventorySlotUI>());
            spellInventorySlots.Add(spellInventorySlotsParent.GetComponentInChildren<InventorySlotUI>());
            consumableInventorySlots.Add(consumableInventorySlotsParent.GetComponentInChildren<InventorySlotUI>());
        }
        private void Start()
        {
            #region 检测空引用异常
            if (weaponInventorySlotsParent == null)
                Debug.LogError("weaponInventorySlotsParent == null");
            if (weaponInventorySlotPerfab == null)
                Debug.LogError("weaponInventorySlotPerfab == null");
            if (weaponInventorySlots == null)
                Debug.LogError("weaponInventorySlots == null");

            if (armorInventorySlotsParent == null)
                Debug.LogError("armorInventorySlotsParent == null");
            if (armorInventorySlotPerfab == null)
                Debug.LogError("armorInventorySlotPerfab == null");
            if (armorInventorySlots == null)
                Debug.LogError("armorInventorySlots == null");

            if (spellInventorySlotsParent == null)
                Debug.LogError("spellInventorySlotsParent == null");
            if (spellInventorySlotPerfab == null)
                Debug.LogError("spellInventorySlotPerfab == null");
            if (spellInventorySlots == null)
                Debug.LogError("spellInventorySlots == null");

            if (consumableInventorySlotsParent == null)
                Debug.LogError("consumableInventorySlotsParent == null");
            if (consumableInventorySlotPerfab == null)
                Debug.LogError("consumableInventorySlotPerfab == null");
            if (consumableInventorySlots == null)
                Debug.LogError("consumableInventorySlots == null");
            #endregion
        }

        public void Open()
        {
            gameObject.SetActive(true);
            UpdateUI();
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void SelectWeaponInventoryWin()
        {
            UnselectCurrentWin();
            selectedWin = InventoryWinType.weapon;
            weaponInventorySlotsParent.gameObject.SetActive(true);
        }
        public void SelectArmorInventoryWin()
        {
            UnselectCurrentWin();
            selectedWin = InventoryWinType.armor;
            armorInventorySlotsParent.gameObject.SetActive(true);
        }
        public void SelectSpellInventoryWin()
        {
            UnselectCurrentWin();
            selectedWin = InventoryWinType.spell;
            spellInventorySlotsParent.gameObject.SetActive(true);
        }
        public void SelectConsumableInventoryWin()
        {
            UnselectCurrentWin();
            selectedWin = InventoryWinType.consumable;
            consumableInventorySlotsParent.gameObject.SetActive(true);
        }
        public void UnselectCurrentWin()
        {
            if (selectedWin == InventoryWinType.weapon)
            {
                weaponInventorySlotsParent.gameObject.SetActive(false);
            }
            else if (selectedWin == InventoryWinType.armor)
            {
                armorInventorySlotsParent.gameObject.SetActive(false);
            }
            else if (selectedWin == InventoryWinType.spell)
            {
                spellInventorySlotsParent.gameObject.SetActive(false);
            }
            else if (selectedWin == InventoryWinType.consumable)
            {
                consumableInventorySlotsParent.gameObject.SetActive(false);
            }
        }

        public void UpdateUI()
        {
            for (int i = 0; i < weaponInventorySlots.Count; i++)
            {
                if (i < player.pInventory.weapons.Count)
                {
                    if (weaponInventorySlots.Count < player.pInventory.weapons.Count) // 添加新的槽
                    {
                        GameObject newWeaponInventorySlotPerfab = Instantiate(weaponInventorySlotPerfab, weaponInventorySlotsParent);
                        weaponInventorySlots.Add(newWeaponInventorySlotPerfab.GetComponent<InventorySlotUI>());
                    }

                    if (i < player.pInventory.weapons.Count)
                        weaponInventorySlots[i].AddItem(player.pInventory.weapons[i]);     // 将项添加到槽中
                    else
                        weaponInventorySlots[i].AddItem(player.pInventory.weapons[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearItem();
                }
            }

            for (int i = 0; i < armorInventorySlots.Count; i++)
            {
                if (i < player.pInventory.armors.Count)
                {
                    if (armorInventorySlots.Count < player.pInventory.armors.Count) // 添加新的槽
                    {
                        GameObject newArmorInventorySlotPerfab = Instantiate(armorInventorySlotPerfab, armorInventorySlotsParent);
                        armorInventorySlots.Add(newArmorInventorySlotPerfab.GetComponent<InventorySlotUI>());
                    }

                    if (i < player.pInventory.armors.Count)
                        armorInventorySlots[i].AddItem(player.pInventory.armors[i]);     // 将项添加到槽中
                    else
                        armorInventorySlots[i].AddItem(player.pInventory.armors[i]);
                }
                else
                {
                    armorInventorySlots[i].ClearItem();
                }
            }

            for (int i = 0; i < spellInventorySlots.Count; i++)
            {
                if (i < player.pInventory.spells.Count)
                {
                    if (spellInventorySlots.Count < player.pInventory.spells.Count) // 添加新的槽
                    {
                        GameObject newSpellInventorySlotPerfab = Instantiate(spellInventorySlotPerfab, spellInventorySlotsParent);
                        spellInventorySlots.Add(newSpellInventorySlotPerfab.GetComponent<InventorySlotUI>());
                    }

                    if (i < player.pInventory.spells.Count)
                        spellInventorySlots[i].AddItem(player.pInventory.spells[i]);     // 将项添加到槽中
                    else
                        spellInventorySlots[i].AddItem(player.pInventory.spells[i]);
                }
                else
                {
                    spellInventorySlots[i].ClearItem();
                }
            }

            for (int i = 0; i < consumableInventorySlots.Count; i++)
            {
                if (i < player.pInventory.consumables.Count)
                {
                    if (consumableInventorySlots.Count < player.pInventory.consumables.Count) // 添加新的槽
                    {
                        GameObject newConsumableInventorySlotPerfab = Instantiate(consumableInventorySlotPerfab, consumableInventorySlotsParent);
                        consumableInventorySlots.Add(newConsumableInventorySlotPerfab.GetComponent<InventorySlotUI>());
                    }

                    if (i < player.pInventory.consumables.Count)
                        consumableInventorySlots[i].AddItem(player.pInventory.consumables[i]);     // 将项添加到槽中
                    else
                        consumableInventorySlots[i].AddItem(player.pInventory.consumables[i]);
                }
                else
                {
                    consumableInventorySlots[i].ClearItem();
                }
            }
        }
    }
}