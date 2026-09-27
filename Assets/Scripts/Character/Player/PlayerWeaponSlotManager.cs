namespace CatchMoon
{
    public class PlayerWeaponSlotManager : CharacterWeaponSlotManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        /// <summary>
        /// ‘⁄Œ‰∆˜≤€÷–º”‘ÿŒ‰∆˜
        /// </summary>
        public override void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            base.LoadWeaponOnSlot(weaponItem, isLeft);
            player.ui.hud.quickSlotsUI.UpdateCurrentWeaponIcon(isLeft, weaponItem);
        }
    }
}