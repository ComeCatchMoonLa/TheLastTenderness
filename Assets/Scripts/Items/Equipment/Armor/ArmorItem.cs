using UnityEngine;

namespace CatchMoon
{
    public class ArmorItem : EquipmentItem
    {
        [Header("Armor Type")]
        public ArmorType armorType;
        [Header("Poise")]
        public float poise;

        [Header("耐久")]
        public bool durabilityFilled;
        public float durabilityMax;
        public float durability;
        public bool lossPerHitFilled;
        public float lossPerHit;

        [Header("Defense Bonus")]
        [Range(0, 1)] public float physicalDA;
        public bool strikeFilled;
        [Range(0, 1)] public float strikeDA;
        public bool slashFilled;
        [Range(0, 1)] public float slashDA;
        public bool thrustFilled;
        [Range(0, 1)] public float thrustDA;
        [Range(0, 1)] public float fireDA;
        [Range(0, 1)] public float magicDA;
        [Range(0, 1)] public float lightningDA;
        [Range(0, 1)] public float darkDA;
    }
}
