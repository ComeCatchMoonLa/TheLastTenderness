using TMPro;
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
        [SerializeField] TextMeshProUGUI consumableCountText;
        [SerializeField] TextMeshProUGUI flaskCountText;

        private void Awake()
        {
            player = PlayerUIManager.FindPlayer(this);
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
            if (consumableCountText == null)
                Debug.LogError("consumableCountText == null");
            if (flaskCountText == null)
                Debug.LogError("flaskCountText == null");
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
            ShowIcon(currentSpellIcon, spell == null ? null : spell.itemIcon);
        }

        public void UpdateCurrentConsumableIcon(ConsumableItem consumable)
        {
            if (consumable == null) return;
            ShowIcon(currentConsumableIcon, consumable.itemIcon);
            if (player != null && player.pInventory != null)
            {
                SetConsumableCount(player.pInventory.ConsumableRemaining(consumable));
                SetFlaskCounts(player.pInventory.estusLeft, player.pInventory.ashLeft);
            }
        }

        public void SetConsumableCount(int remaining)
        {
            if (consumableCountText == null)
            {
                Debug.LogError("consumableCountText == null");
                return;
            }

            consumableCountText.text = remaining.ToString();
        }

        public void SetFlaskCounts(int estusLeft, int ashLeft)
        {
            if (flaskCountText == null) return;
            flaskCountText.text = FlaskHudCounts.Line(estusLeft, ashLeft);
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