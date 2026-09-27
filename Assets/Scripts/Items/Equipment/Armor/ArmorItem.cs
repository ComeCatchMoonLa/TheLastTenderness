using UnityEngine;

namespace CatchMoon
{
    public class ArmorItem : EquipmentItem
    {
        [Header("Armor Type")]
        public ArmorType armorType;
        [Header("Defense Bonus")]
        [Range(0, 1)] public float physicalDA;
        [Range(0, 1)] public float fireDA;
        [Range(0, 1)] public float magicDA;
        [Range(0, 1)] public float lightningDA;
        [Range(0, 1)] public float darkDA;
    }
}
