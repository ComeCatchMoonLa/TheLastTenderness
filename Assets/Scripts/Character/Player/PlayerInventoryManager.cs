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
        public List<SpellItem> memorized;
        public List<ConsumableItem> consumables;
        public Item[] ringSlots = new Item[EquipmentLayout.RingSlots];

        Dictionary<ConsumableItem, int> consumableLeft;
        Dictionary<AmmoItem, int> ammoLeft;
        public ConsumableItem consumableBeingUsed;

        public int flaskTotal = 3;
        public int estusShare = 3;
        public int ashShare = 0;
        public int estusLeft = 3;
        public int ashLeft = 0;
        PhantomLoadout phantomSaved;
        public int boneTier = 0;
        public FlaskRecoveryTable recoveryTable;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
            ringSlots = EquipmentLayout.Ensure(ringSlots, EquipmentLayout.RingSlots);
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
            estusLeft = estusShare;
            ashLeft = ashShare;

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
            player.ui.hud.quickSlotsUI.SetFlaskCounts(estusLeft, ashLeft);
        }

        public bool TryAllocateFlasks(int estus, int ash)
        {
            if (estus < 0 || ash < 0) return false;
            if (estus + ash != flaskTotal) return false;
            estusShare = estus;
            ashShare = ash;
            return true;
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

        public bool EnterPhantom(PhantomSign sign, float maxHp, float unemberedMax, out float maxNow)
        {
            bool entered = PhantomEntry.Enter(
                sign, estusLeft, estusShare, ashLeft, ashShare, maxHp, unemberedMax,
                out phantomSaved, out int estusNow, out int ashNow, out maxNow);
            if (!entered) return false;
            estusLeft = estusNow;
            ashLeft = ashNow;
            return true;
        }

        public bool ReturnPhantom(out float maxHp)
        {
            if (!PhantomEntry.Return(phantomSaved, out int estus, out int ash, out maxHp)) return false;
            estusLeft = estus;
            ashLeft = ash;
            phantomSaved = default;
            return true;
        }

        public void AddItem(Item item)
        {
            if (item is CoilFragmentItem fragment)
            {
                int already = consumables != null && consumables.Contains(fragment) ? 1 : 0;
                if (CoilFragment.Pickup(already) == already) return;
            }

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
                    NoteEquippedWeapons();
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

        static bool AdvanceHand(WeaponItem[] hand, ref int index, ref WeaponItem current)
        {
            if (hand == null || hand.Length == 0) return false;
            for (int i = 0; i < hand.Length; i++)
            {
                index = (index + 1) % hand.Length;
                if (hand[index] == null) continue;
                current = hand[index];
                return true;
            }
            return false;
        }

        void ChangeLeftWeapon()
        {
            if (player.isInteracting) return;
            if (player.isTwoHandingWeapon && leftWeapon != null && leftWeapon.weaponType != WeaponType.bow) return;

            if (!AdvanceHand(weaponsInLeftHandSlot, ref currentLeftWeaponIdx, ref leftWeapon)) return;

            if (player.isTwoHandingWeapon)
            {
                int guard = 0;
                while (leftWeapon.weaponType != WeaponType.bow)
                {
                    if (++guard > weaponsInLeftHandSlot.Length) return;
                    if (!AdvanceHand(weaponsInLeftHandSlot, ref currentLeftWeaponIdx, ref leftWeapon)) return;
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
            NoteEquippedWeapons();
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
            NoteEquippedWeapons();
            return true;
        }

        void NoteEquippedWeapons()
        {
            if (player == null || player.pCombat == null) return;
            player.pCombat.weaponBuff = WeaponBuff.ClearIfUnequipped(player.pCombat.weaponBuff, rightWeapon, leftWeapon);
            WeaponBuff.ShowFire(player.pCombat.buffFire, player.pCombat.weaponBuff.active);
        }

        void ChangeRightWeapon()
        {
            if (player.isInteracting) return;
            if (leftWeapon != null && leftWeapon.weaponType == WeaponType.bow) return;

            if (!AdvanceHand(weaponsInRightHandSlot, ref currentRightWeaponIdx, ref rightWeapon)) return;

            if (player.isTwoHandingWeapon)
            {
                int guard = 0;
                while (rightWeapon.weaponType != WeaponType.melee_TH && rightWeapon.weaponType != WeaponType.melee_THL)
                {
                    if (++guard > weaponsInRightHandSlot.Length) return;
                    if (!AdvanceHand(weaponsInRightHandSlot, ref currentRightWeaponIdx, ref rightWeapon)) return;
                }
            }
            player.pWeaponSlot.LoadWeaponsOnBothHands();
            NoteEquippedWeapons();
        }
        public bool Memorize(SpellItem spell, bool atBonfire)
        {
            if (!atBonfire) return false;
            if (spell == null || spells == null || !spells.Contains(spell)) return false;
            if (memorized == null)
                memorized = new List<SpellItem>();
            if (memorized.Contains(spell)) return true;
            memorized.Add(spell);
            return true;
        }

        void ChangeSpell()
        {
            SpellItem next = SpellMemory.Next(memorized, ref currentSpellIdx);
            if (next == null) return;
            currentSpell = next;
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
