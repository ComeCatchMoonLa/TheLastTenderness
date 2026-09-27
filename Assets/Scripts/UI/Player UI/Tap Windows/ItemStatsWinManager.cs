using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CatchMoon
{
    public class ItemStatsWinManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemNameText;
        [SerializeField] Image itemIcon;

        [Header("Equipment Stats Texts UI")]
        [SerializeField] GameObject weaponStats;
        [SerializeField] GameObject armorStats;

        [Header("Weapon Stats")]
        [SerializeField] TextMeshProUGUI physicalAttackPowerText;
        [SerializeField] TextMeshProUGUI fireAttackPowerText;
        [SerializeField] TextMeshProUGUI magicAttackPowerText;
        [SerializeField] TextMeshProUGUI lightningAttackPowerText;
        [SerializeField] TextMeshProUGUI darkAttackerPowerText;
        [SerializeField] TextMeshProUGUI criticalDamageMuiltiplierText;

        [SerializeField] TextMeshProUGUI weaponPhysicalDAText;
        [SerializeField] TextMeshProUGUI weaponFireDAText;
        [SerializeField] TextMeshProUGUI weaponMagicDAText;
        [SerializeField] TextMeshProUGUI weaponLightningDAText;
        [SerializeField] TextMeshProUGUI weaponDarkDAText;

        [Header("Armor Stats")]
        [SerializeField] TextMeshProUGUI armorPhysicalDAText;
        [SerializeField] TextMeshProUGUI armorFireDAText;
        [SerializeField] TextMeshProUGUI armorMagicDAText;
        [SerializeField] TextMeshProUGUI armorLightningDAText;
        [SerializeField] TextMeshProUGUI armorDarkDAText;

        public void UpdateEquipmentItemStats(EquipmentItem equipment)
        {
            weaponStats.SetActive(false);
            armorStats.SetActive(false);

            if (UpdateNameAndIcon(equipment))
            {
                if (equipment.equipmentType == EquipmentType.weapon)
                    UpdateWeaponStats(equipment as WeaponItem);
                else if (equipment.equipmentType == EquipmentType.armor)
                    UpdateArmorStats(equipment as ArmorItem);
            }
        }

        void UpdateWeaponStats(WeaponItem weapon)
        {
            itemNameText.fontSize = 36;
            physicalAttackPowerText.text = weapon.pd.ToString();
            fireAttackPowerText.text = weapon.fd.ToString();
            magicAttackPowerText.text = weapon.md.ToString();
            lightningAttackPowerText.text = weapon.ld.ToString();
            darkAttackerPowerText.text = weapon.dd.ToString();
            weaponPhysicalDAText.text = weapon.physicalDA * 100 + "%";
            weaponFireDAText.text = weapon.fireDA * 100 + "%";
            weaponMagicDAText.text = weapon.magicDA * 100 + "%";
            weaponLightningDAText.text = weapon.lightningDA * 100 + "%";
            weaponDarkDAText.text = weapon.darkDA * 100 + "%";

            weaponStats.SetActive(true);
        }
        void UpdateArmorStats(ArmorItem armor)
        {
            itemNameText.fontSize = 24;
            armorPhysicalDAText.text = armor.physicalDA * 100 + "%";
            armorFireDAText.text = armor.fireDA * 100 + "%";
            armorMagicDAText.text = armor.magicDA * 100 + "%";
            armorLightningDAText.text = armor.lightningDA * 100 + "%";
            armorDarkDAText.text = armor.darkDA * 100 + "%";

            armorStats.SetActive(true);
        }

        // ¸üÐÂitem×´Ì¬
        public bool UpdateNameAndIcon(Item item)
        {
            if (item == null)
            {
                Debug.Log("item is null.");
                itemNameText.text = string.Empty;
                itemIcon.sprite = null;
                itemIcon.gameObject.SetActive(false);
                return false;
            }
            else
            {
                if (item.itemName == string.Empty)
                    Debug.LogWarning("No Name");
                itemNameText.text = item.itemName;

                if (item.itemIcon == null)
                {
                    Debug.LogWarning("No Icon");
                    itemIcon.gameObject.SetActive(false);
                }
                else
                {
                    itemIcon.gameObject.SetActive(true);
                }
                itemIcon.sprite = item.itemIcon;
                itemIcon.preserveAspect = true;
                return true;
            }
        }
    }
}