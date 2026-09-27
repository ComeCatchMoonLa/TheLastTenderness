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
        /// 在武器槽中加载武器
        /// </summary>
        public override bool LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (!base.LoadWeaponOnSlot(weaponItem, isLeft))
                return false;
            player.ui.hud.quickSlotsUI.UpdateCurrentWeaponIcon(isLeft, weaponItem);
            return true;
        }
    }
}