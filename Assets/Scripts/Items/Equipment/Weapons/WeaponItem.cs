using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Equipment/Weapon")]
    public class WeaponItem : EquipmentItem
    {
        public GameObject modelPrefab; // 武器预制件
        public Transform transform;

        [Header("Animator Replacer")]
        public AnimatorOverrideController weaponController;
        public string offHandIdleAnimation = "Left_Arm_Idle_01";

        [Header("武器类型")]
        public WeaponType weaponType;
        public CatalystKind catalystKind;
        public bool spellPowerFilled;
        public float spellPower;
        public string infusion;
        public bool cannotBuff;
        public bool stanceArt;
        public int reinforceLevel;
        public UpgradePath upgradePath;
        public bool strengthNeedFilled;
        public int strengthNeed;
        public bool dexterityNeedFilled;
        public int dexterityNeed;
        public bool intelligenceNeedFilled;
        public int intelligenceNeed;
        public bool faithNeedFilled;
        public int faithNeed;
        public bool luckNeedFilled;
        public int luckNeed;

        [Header("伤害")]
        public int pd;
        public int fd;
        public int md;
        public int ld;
        public int dd;

        [Header("伤害倍数")]
        public float laFirstPhaseDM = 1; 
        public float laSecondPhaseDM = 1.2f;
        public float haFirstPhaseDM = 1.6f;
        public float haSecondPhasDM = 1.8f;
        public float criticalAttackDM = 2.4f; // 会心一击伤害系数(对于基础伤害而言)
        public float guardBreakM = 1;         // 格挡被打破时受到的伤害的系数

        [Header("Poise")]
        public float poiseBreak = 25;
        public float offensivePoiseBonus;

        [Header("伤害吸收率")]
        [Range(0, 1)] public float physicalDA;
        [Range(0, 1)] public float fireDA;
        [Range(0, 1)] public float magicDA;
        [Range(0, 1)] public float lightningDA;
        [Range(0, 1)] public float darkDA;

        [Header("防御时的体力兑换率")]
        [Range(0, 1)] public float blockingStabilityRating;

        [Header("稳定率")]
        [Tooltip("挡下攻击时会消耗攻击者攻击消耗体力的系数,例如, 攻击者的攻击消耗了30体力, 那么玩家用盾牌挡下攻击就会消耗30*(1-0.67)的体力")]
        [Range(0, 1)] public float stability = 0.67f;

        [Header("体力消耗")]
        public int baseStaminaCost = 1;     // 基础体力消耗
        public float laStaminaCostM = 1;    // 轻攻击花费体力系数
        public float haStaminaCostM = 1.5f; // 重攻击花费体力系数

        [Header("Item Actions")]
        public WeaponItemAction oh_tap_e_action;
        public WeaponItemAction oh_hold_e_action;
        public WeaponItemAction oh_tap_q_action;
        public WeaponItemAction oh_hold_q_action;
        public WeaponItemAction oh_tap_r_action;
        public WeaponItemAction oh_hold_r_action;
        public WeaponItemAction oh_tap_z_action;
        public WeaponItemAction oh_hold_z_action;

        [Header("Item Actions")]
        public WeaponItemAction th_tap_e_action;
        public WeaponItemAction th_hold_e_action;
        public WeaponItemAction th_tap_q_action;
        public WeaponItemAction th_hold_q_action;
        public WeaponItemAction th_tap_r_action;
        public WeaponItemAction th_hold_r_action;
        public WeaponItemAction th_tap_z_action;
        public WeaponItemAction th_hold_z_action;

        [Header("音效")]
        public AudioClip[] weaponWhooshesSound;
    }
}