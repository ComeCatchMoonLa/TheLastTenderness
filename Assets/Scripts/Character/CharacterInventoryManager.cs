using UnityEngine;

namespace CatchMoon
{
    public class CharacterInventoryManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("当前使用的物品")]
        public Item currentItemBeingUsed;

        [Header("Quik Slot Items")]
        public SpellItem currentSpell;           // 法术
        public WeaponItem leftWeapon;            // 左手武器
        public WeaponItem rightWeapon;           // 右手武器
        public ConsumableItem currentConsumable; // 当前道具

        [Header("弹药")]
        public AmmoItem currentAmmo;

        [Header("武器")]
        public WeaponItem[] weaponsInLeftHandSlot = new WeaponItem[EquipmentLayout.WeaponsPerHand];
        public WeaponItem[] weaponsInRightHandSlot = new WeaponItem[EquipmentLayout.WeaponsPerHand];
        [HideInInspector] public int currentLeftWeaponIdx = 0;
        [HideInInspector] public int currentRightWeaponIdx = 0;

        [Header("法术 & 消耗品")]
        public int currentSpellIdx = 0;
        public int currentComsumableIdx = 0;

        [Header("默认武器")]
        public WeaponItem unarmed;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
            weaponsInLeftHandSlot = EquipmentLayout.Ensure(weaponsInLeftHandSlot, EquipmentLayout.WeaponsPerHand);
            weaponsInRightHandSlot = EquipmentLayout.Ensure(weaponsInRightHandSlot, EquipmentLayout.WeaponsPerHand);
        }
        protected virtual void Start()
        {
            leftWeapon = weaponsInLeftHandSlot[currentLeftWeaponIdx];
            rightWeapon = weaponsInRightHandSlot[currentRightWeaponIdx];

            character.cWeaponSlot.LoadWeaponsOnBothHands();
            character.cWeaponSlot.LoadTwoHandIKTarget();
            if (leftWeapon == null)
            { Debug.LogError("leftWeapon is null."); }
            if (rightWeapon == null)
            { Debug.LogError("rightWeapon is null."); }
        }
    }
}