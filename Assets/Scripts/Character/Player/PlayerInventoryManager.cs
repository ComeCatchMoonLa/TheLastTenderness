using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
     /// <summary>
     /// 玩家库存
     /// </summary>
    public class PlayerInventoryManager : CharacterInventoryManager
    {
        PlayerManager player;

        [Header("库存")]
        [HideInInspector] public List<Item> items;
        public List<WeaponItem> weapons;
        public List<ArmorItem> armors;
        public List<SpellItem> spells;
        public List<ConsumableItem> consumables;

        Dictionary<ConsumableItem, int> consumableLeft;
        Dictionary<AmmoItem, int> ammoLeft;
        public ConsumableItem consumableBeingUsed;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
            consumableLeft = new Dictionary<ConsumableItem, int>();
            ammoLeft = new Dictionary<AmmoItem, int>();
        }
        protected override void Start()
        {
            base.Start();

            if (consumables != null)
            {
                for (int i = 0; i < consumables.Count; i++)
                    RememberConsumable(consumables[i]);
            }
            RememberConsumable(currentConsumable);
            RememberAmmo(currentAmmo);
        }

        public bool TrySpendConsumable(ConsumableItem item)
        {
            if (item == null) return false;
            RememberConsumable(item);
            if (consumableLeft[item] <= 0) return false;
            consumableLeft[item]--;
            return true;
        }
        public bool TrySpendAmmo(AmmoItem item)
        {
            if (item == null) return false;
            RememberAmmo(item);
            if (ammoLeft[item] <= 0) return false;
            ammoLeft[item]--;
            return true;
        }
        public int AmmoRemaining(AmmoItem item)
        {
            if (item == null) return 0;
            RememberAmmo(item);
            return ammoLeft[item];
        }
        void RememberConsumable(ConsumableItem item)
        {
            if (item == null || consumableLeft.ContainsKey(item)) return;
            consumableLeft[item] = item.maxItemAmount;
        }
        void RememberAmmo(AmmoItem item)
        {
            if (item == null || ammoLeft.ContainsKey(item)) return;
            ammoLeft[item] = item.maxCnt;
        }
        protected virtual void Update()
        {
            if (player.pStats.isDead) return;

            HandleTwoHandInput();
            HandleQuickSlotsInput();
            HandleUseConsumableInput();
        }

        public void AddItem(Item item)
        {
            items.Add(item);

            if (item.itemType == ItemType.equipment)
            {
                EquipmentItem equipment = item as EquipmentItem;
                if (equipment.equipmentType == EquipmentType.weapon)
                    weapons.Add(equipment as WeaponItem);
                else if (equipment.equipmentType == EquipmentType.armor)
                    armors.Add(equipment as ArmorItem);
            }
            else if (item.itemType == ItemType.spell)
            {
                spells.Add(item as SpellItem);
            }
            else if (item.itemType == ItemType.consumable)
            {
                consumables.Add(item as ConsumableItem);
            }
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);

            if (item.itemType == ItemType.equipment)
            {
                EquipmentItem equipment = item as EquipmentItem;
                if (equipment.equipmentType == EquipmentType.weapon)
                    weapons.Remove(equipment as WeaponItem);
                else if (equipment.equipmentType == EquipmentType.armor)
                    armors.Remove(equipment as ArmorItem);
            }
            else if (item.itemType == ItemType.spell)
            {
                spells.Remove(item as SpellItem);
            }
            else if (item.itemType == ItemType.consumable)
            {
                consumables.Remove(item as ConsumableItem);
            }
        }

        void HandleTwoHandInput()
        {
            if (player.input.y_Input)
            {
                player.input.y_Input = false;

                // 右手持双手武器或左手持弓时, 才能切换持武器的模式
                if (rightWeapon.weaponType == WeaponType.melee_TH
                    || rightWeapon.weaponType == WeaponType.melee_THL
                    || leftWeapon.weaponType == WeaponType.bow)
                {
                    player.isTwoHandingWeapon = !player.isTwoHandingWeapon;
                    player.pWeaponSlot.LoadWeaponsOnBothHands();
                }
            }
        }
        void HandleQuickSlotsInput()
        {
            if (player.input.right_Arrow_Input)
                ChangeRightWeapon();
            else if (player.input.left_Arrow_Input)
                ChangeLeftWeapon();
            else if (player.input.up_arrow_Input)
                ChangeSpell();
            else if (player.input.down_arrow_input)
                ChangeConsumable();
        }
        void HandleUseConsumableInput()
        {
            if (player.input.x_Input)
            {
                player.input.x_Input = false;
                if (currentConsumable == null) return;

                currentConsumable.AttemptToConsumableItem(player);
            }
        }

        void ChangeLeftWeapon()
        {
            if (player.isInteracting) return;
            if (player.isTwoHandingWeapon && leftWeapon.weaponType != WeaponType.bow) return;

            currentLeftWeaponIdx = ++currentLeftWeaponIdx % weaponsInLeftHandSlot.Length;
            leftWeapon = weaponsInLeftHandSlot[currentLeftWeaponIdx];

            if (player.isTwoHandingWeapon)
            {
                while (leftWeapon.weaponType != WeaponType.bow)
                {
                    currentLeftWeaponIdx = ++currentLeftWeaponIdx % weaponsInLeftHandSlot.Length;
                    leftWeapon = weaponsInLeftHandSlot[currentLeftWeaponIdx];
                }
            }
            else
            {
                if (leftWeapon.weaponType == WeaponType.bow)
                {
                    player.pCamera.Unlock();
                    currentRightWeaponIdx = 0;
                    rightWeapon = weaponsInRightHandSlot[currentRightWeaponIdx];
                    player.pWeaponSlot.LoadWeaponOnSlot(rightWeapon,false);
                }
            }
            player.pWeaponSlot.LoadWeaponsOnBothHands();
        }
        void ChangeRightWeapon()
        {
            if (player.isInteracting) return;
            if (leftWeapon.weaponType == WeaponType.bow) return;

            currentRightWeaponIdx = ++currentRightWeaponIdx % weaponsInRightHandSlot.Length;
            rightWeapon = weaponsInRightHandSlot[currentRightWeaponIdx];

            if (player.isTwoHandingWeapon)
            {
                while (rightWeapon.weaponType != WeaponType.melee_TH && rightWeapon.weaponType != WeaponType.melee_THL)
                {
                    currentRightWeaponIdx = ++currentRightWeaponIdx % weaponsInRightHandSlot.Length;
                    rightWeapon = weaponsInRightHandSlot[currentRightWeaponIdx];
                }
            }
            player.pWeaponSlot.LoadWeaponsOnBothHands();
        }
        void ChangeSpell()
        {
            currentSpellIdx = ++currentSpellIdx % spells.Count;
            currentSpell = spells[currentSpellIdx];
            player.ui.hud.quickSlotsUI.UpdateCurrentSpellIcon(currentSpell);
        }
        void ChangeConsumable()
        {
            currentComsumableIdx = ++ currentComsumableIdx % consumables.Count;
            currentConsumable = consumables[currentComsumableIdx];
            player.ui.hud.quickSlotsUI.UpdateCurrentConsumableIcon(currentConsumable);
        }
    }
}
