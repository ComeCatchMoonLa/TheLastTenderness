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

        public int flaskTotal = 3;
        public int estusShare = 3;
        public int ashShare = 0;
        public int boneTier = 0;
        public FlaskRecoveryTable recoveryTable;

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
            if (item == currentConsumable)
                RefreshCurrentConsumableCount();
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
        public int ConsumableRemaining(ConsumableItem item)
        {
            if (item == null) return 0;
            RememberConsumable(item);
            return consumableLeft[item];
        }
        public void RefillConsumablesToMax()
        {
            if (consumableLeft == null || consumableLeft.Count == 0) return;
            var items = new List<ConsumableItem>(consumableLeft.Keys);
            for (int i = 0; i < items.Count; i++)
            {
                ConsumableItem item = items[i];
                if (item == null) continue;
                consumableLeft[item] = item.maxItemAmount;
            }

            if (currentConsumable != null)
                RefreshCurrentConsumableCount();
        }
        void RefreshCurrentConsumableCount()
        {
            if (player == null || player.ui == null || player.ui.hud == null || player.ui.hud.quickSlotsUI == null)
                return;
            if (currentConsumable == null)
                return;
            player.ui.hud.quickSlotsUI.SetConsumableCount(ConsumableRemaining(currentConsumable));
        }

        public void ObtainAshFlask()
        {
            if (ashShare > 0) return;
            if (flaskTotal >= 15) return;
            flaskTotal++;
            ashShare = 1;
        }

        public void AddFlaskShard()
        {
            if (flaskTotal >= 15) return;
            flaskTotal++;
        }

        public void AddBoneShard()
        {
            if (boneTier >= 10) return;
            boneTier++;
        }

        public bool TryGetFlaskSip(FlaskType flaskType, out int amount)
        {
            amount = 0;
            if (recoveryTable == null)
            {
                Debug.LogError("PlayerInventoryManager.recoveryTable 未填");
                return false;
            }

            FlaskSipRow[] rows = recoveryTable.rows;
            if (rows == null || rows.Length != 11 || boneTier < 0 || boneTier >= rows.Length)
            {
                Debug.LogError(recoveryTable.name + ".rows 未填");
                return false;
            }

            FlaskSipRow row = rows[boneTier];
            if (row.health <= 0)
            {
                Debug.LogError(recoveryTable.name + ".rows[" + boneTier + "].health 未填");
                return false;
            }

            if (row.focus <= 0)
            {
                Debug.LogError(recoveryTable.name + ".rows[" + boneTier + "].focus 未填");
                return false;
            }

            amount = flaskType == FlaskType.ashen ? row.focus : row.health;
            return true;
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
        public bool SelectHandWeapon(bool isLeft, int index)
        {
            WeaponItem[] hand = isLeft ? weaponsInLeftHandSlot : weaponsInRightHandSlot;
            if (hand == null || index < 0 || index >= hand.Length)
                return false;
            if (isLeft)
            {
                currentLeftWeaponIdx = index;
                leftWeapon = hand[index];
            }
            else
            {
                currentRightWeaponIdx = index;
                rightWeapon = hand[index];
            }
            return true;
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
