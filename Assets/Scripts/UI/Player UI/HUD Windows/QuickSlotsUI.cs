using UnityEngine;
using UnityEngine.UI;

namespace CatchMoon
{
    public class QuickSlotsUI : MonoBehaviour
    {
        PlayerManager player;

        [SerializeField] Image leftWeaponIcon;
        [SerializeField] Image rightWeaponIcon;
        [SerializeField] Image currentSpellIcon;
        [SerializeField] Image currentConsumableIcon;

        private void Awake()
        {
            player = transform.root.GetComponent<PlayerManager>();
        }
        private void Start()
        {
            #region ºÏ≤‚ø’“˝”√“Ï≥£
            if (leftWeaponIcon == null) 
                Debug.LogError("leftWeaponIcon == null");
            if (rightWeaponIcon == null)
                Debug.LogError("rightWeaponSlotIcon == null");
            if (currentSpellIcon == null)
                Debug.LogError("spellSlotIcon == null");
            if (currentConsumableIcon == null)
                Debug.LogError("consumableSlotIcon == null");
            #endregion

            UpdateCurrentSpellIcon(player.pInventory.currentSpell);
            UpdateCurrentConsumableIcon(player.pInventory.currentConsumable);

            leftWeaponIcon.preserveAspect = true;
            rightWeaponIcon.preserveAspect = true;
            currentSpellIcon.preserveAspect = true;
            currentConsumableIcon.preserveAspect = true;
        }

        public void UpdateCurrentWeaponIcon(bool isLeft, WeaponItem weapon)
        {
            if (weapon.itemIcon == null)
            {
                Debug.LogError("weapon.itemIcon == null");
                return;
            }

            if (isLeft)
            {
                leftWeaponIcon.sprite = weapon.itemIcon;
                leftWeaponIcon.enabled = true;
            }
            else
            {
                rightWeaponIcon.sprite = weapon.itemIcon;
                rightWeaponIcon.enabled = true;      
            }
        }

        public void UpdateCurrentSpellIcon(SpellItem spell)
        {
            if (spell == null) return;
            if (spell.itemIcon == null)
            {
                Debug.LogError("spell.itemIcon == null");
                return;
            }

            currentSpellIcon.sprite = spell.itemIcon;
            currentSpellIcon.enabled = true;
        }

        public void UpdateCurrentConsumableIcon(ConsumableItem consumable)
        {
            if (consumable == null) return;
            if (consumable.itemIcon == null)
            {
                Debug.LogError("consumable.itemIcon == null");
                return;
            }

            currentConsumableIcon.sprite = consumable.itemIcon;
            currentConsumableIcon.enabled = true;
        }
    }
}