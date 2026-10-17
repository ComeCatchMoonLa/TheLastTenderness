using UnityEngine;

namespace CatchMoon
{
    public class ArmorItem : EquipmentItem
    {
        [Header("Armor Type")]
        public ArmorType armorType;
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
