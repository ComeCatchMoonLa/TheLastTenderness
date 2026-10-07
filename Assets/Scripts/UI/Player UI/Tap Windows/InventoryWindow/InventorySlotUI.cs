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
            // 学会列表上点一下不进入战斗轮换。放进记忆栏的菜单不在这一版。
            if (spell == null) return;
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
            bool placed = false;
            if (EquipmentWinManager.TryHandIndex(selectedSlotType, out bool isLeft, out int index))
            {
                WeaponItem[] hand = isLeft ? pInventory.weaponsInLeftHandSlot : pInventory.weaponsInRightHandSlot;
                if (hand != null && index >= 0 && index < hand.Length)
                {
                    WeaponItem current = hand[index];
                    if (!EquipCompare.Arm(weapon))
                    {
                        EquipCompare.Preview(
                            current != null ? current.pd : 0,
                            weapon.pd,
                            current != null ? current.physicalDA : 0f,
                            weapon.physicalDA);
                        return;
                    }
                    if (hand[index] != null)
                        pInventory.AddItem(hand[index]);
                    hand[index] = weapon;
                    if (isLeft)
                        pInventory.currentLeftWeaponIdx = index;
                    else
                        pInventory.currentRightWeaponIdx = index;
                    EquipmentSlotUI slot = equipmentWin.FindHandSlot(selectedSlotType);
                    if (slot != null)
                        slot.AddItem(weapon);
                    placed = true;
                }
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

            ArmorItem currentArmor = CurrentArmor(pArmor, armor.armorType);
            if (!EquipCompare.Arm(armor))
            {
                EquipCompare.Preview(0, 0, currentArmor != null ? currentArmor.physicalDA : 0f, armor.physicalDA);
                return;
            }

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

        static ArmorItem CurrentArmor(PlayerArmorManager armor, ArmorType type)
        {
            if (armor == null) return null;
            if (type == ArmorType.head) return armor.currentHeadArmor;
            if (type == ArmorType.torso) return armor.currentTorsoArmor;
            if (type == ArmorType.hips) return armor.currentHipsArmor;
            return null;
        }
    }
}