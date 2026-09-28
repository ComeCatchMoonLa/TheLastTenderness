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
            #region 检测空引用异常
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
            if (weapon == null) return;
            ShowIcon(isLeft ? leftWeaponIcon : rightWeaponIcon, weapon.itemIcon);
        }

        public void UpdateCurrentSpellIcon(SpellItem spell)
        {
            if (spell == null) return;
            ShowIcon(currentSpellIcon, spell.itemIcon);
        }

        public void UpdateCurrentConsumableIcon(ConsumableItem consumable)
        {
            if (consumable == null) return;
            ShowIcon(currentConsumableIcon, consumable.itemIcon);
        }

        static void ShowIcon(Image icon, Sprite sprite)
        {
            if (sprite == null)
            {
                icon.sprite = null;
                icon.enabled = false;
                return;
            }

            icon.sprite = sprite;
            icon.enabled = true;
        }
    }
}