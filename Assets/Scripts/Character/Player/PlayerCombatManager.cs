namespace CatchMoon
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }
        protected virtual void Update()
        {
            if (player.pStats.isDead) return;

            HandleCombatInput();
        }

        public override void AttemptBlock(float guardBreakModifider, string blockAnimation, float pd, float fd, float md, float ld, float dd)
        {
            base.AttemptBlock(guardBreakModifider, blockAnimation, pd, fd, md, ld, dd);
            player.ui.hud.staminaBar.SetCurrentStamina(player.pStats.currentStamina);
        }

        void HandleCombatInput()
        {
            WeaponItem leftWeapon = player.pInventory.leftWeapon;
            WeaponItem rightWeapon = player.pInventory.rightWeapon;

            // 函数顺序决定了能否用一种行为打断另一种行为
            Handle_Hold_E_Input(rightWeapon);
            Handle_Hold_Q_Input(leftWeapon, rightWeapon);
            Handle_Tap_E_Input(leftWeapon, rightWeapon);
            Handle_Tap_R_Input(rightWeapon);
            Handle_Tap_Q_Input(leftWeapon, rightWeapon);
            Handle_Tap_Z_Input(leftWeapon, rightWeapon);
        }
       
        void Handle_Tap_E_Input(WeaponItem leftWeapon, WeaponItem rightWeapon)
        {
            if (player.input.tap_e_Input)
            {
                player.input.tap_e_Input = false;
                if (leftWeapon != null && leftWeapon.weaponType == WeaponType.bow)
                {
                    if (leftWeapon != null && leftWeapon.oh_tap_e_action != null)
                    {
                        player.UpdateWhichHandCharacterIsUsing(usingRightHand: false);
                        player.pInventory.currentItemBeingUsed = leftWeapon;
                        leftWeapon.oh_tap_e_action.PerformAction(player);
                    }
                }
                else
                {
                    if (rightWeapon != null && rightWeapon.oh_tap_e_action != null)
                    {
                        player.UpdateWhichHandCharacterIsUsing(usingRightHand: true);
                        player.pInventory.currentItemBeingUsed = rightWeapon;
                        rightWeapon.oh_tap_e_action.PerformAction(player);
                    }
                }
            }
        }
        void Handle_Hold_E_Input(WeaponItem rightWeapon)
        {
            if (player.isTwoHandingWeapon && player.pInventory.leftWeapon != null && player.pInventory.leftWeapon.weaponType == WeaponType.bow) return;

            if (player.input.hold_e_Input)
            {
                player.input.hold_e_Input = false;
                if (rightWeapon != null && rightWeapon.oh_hold_e_action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(usingRightHand: true);
                    player.pInventory.currentItemBeingUsed = rightWeapon;
                    rightWeapon.oh_hold_e_action.PerformAction(player);
                }
            }
        }
        void Handle_Tap_R_Input(WeaponItem rightWeapon)
        {
            if (player.isTwoHandingWeapon && player.pInventory.leftWeapon != null && player.pInventory.leftWeapon.weaponType == WeaponType.bow) return;

            if (player.input.tap_r_Input)
            {
                player.input.tap_r_Input = false;
                if (rightWeapon != null && rightWeapon.oh_tap_r_action != null)
                {
                    player.UpdateWhichHandCharacterIsUsing(usingRightHand: true);
                    player.pInventory.currentItemBeingUsed = rightWeapon;
                    rightWeapon.oh_tap_r_action.PerformAction(player);
                }
            }
        }
        void Handle_Tap_Q_Input(WeaponItem leftWeapon, WeaponItem rightWeapon)
        {
            if (player.isTwoHandingWeapon && player.pInventory.leftWeapon != null && player.pInventory.leftWeapon.weaponType == WeaponType.bow) return;

            if (player.input.tap_q_Input)
            {
                player.input.tap_q_Input = false;
                if (player.isTwoHandingWeapon)
                {
                    if (rightWeapon != null && rightWeapon.oh_tap_q_action != null)
                    {
                        player.UpdateWhichHandCharacterIsUsing(usingRightHand: true);
                        player.pInventory.currentItemBeingUsed = rightWeapon;
                        rightWeapon.oh_tap_q_action.PerformAction(player);
                    }
                }
                else
                {
                    if (leftWeapon != null && leftWeapon.oh_tap_q_action != null)
                    {
                        player.UpdateWhichHandCharacterIsUsing(usingRightHand: false);
                        player.pInventory.currentItemBeingUsed = leftWeapon;
                        leftWeapon.oh_tap_q_action.PerformAction(player);
                    }
                }
            }
        }
        void Handle_Hold_Q_Input(WeaponItem leftWeapon, WeaponItem rightWeapon)
        {
            if (player.input.hold_q_Input)
            {
                if (player.isTwoHandingWeapon && leftWeapon != null && leftWeapon.weaponType != WeaponType.bow)
                {
                    if (rightWeapon != null && rightWeapon.oh_hold_q_action != null)
                    {
                        player.UpdateWhichHandCharacterIsUsing(usingRightHand: true);
                        player.pInventory.currentItemBeingUsed = rightWeapon;
                        rightWeapon.oh_hold_q_action.PerformAction(player);
                    }
                }
                else
                {
                    if (leftWeapon != null && leftWeapon.oh_hold_q_action != null)
                    {
                        if (leftWeapon.weaponType == WeaponType.melee_OH_Shield)
                            player.UpdateWhichHandCharacterIsUsing(usingRightHand: false);
                        else
                            player.UpdateWhichHandCharacterIsUsing(usingRightHand: false);
                        
                        player.pInventory.currentItemBeingUsed = leftWeapon;
                        leftWeapon.oh_hold_q_action.PerformAction(player);
                    }
                }
            }
            else
            {
                player.pCombat.isBlocking = false;

                player.pCombat.ResetBlockingAbsorption();
                player.aimingMode = false;
            }
        }
        void Handle_Tap_Z_Input(WeaponItem leftWeapon, WeaponItem rightWeapon)
        {
            if (player.isTwoHandingWeapon && player.pInventory.leftWeapon != null && player.pInventory.leftWeapon.weaponType == WeaponType.bow) return;

            if (player.input.tap_z_Input)
            {
                player.input.tap_z_Input = false;
                if (player.isTwoHandingWeapon)
                {
                    if (rightWeapon != null && rightWeapon.oh_tap_z_action != null)
                    {
                        player.UpdateWhichHandCharacterIsUsing(usingRightHand: true);
                        player.pInventory.currentItemBeingUsed = rightWeapon;
                        rightWeapon.oh_tap_z_action.PerformAction(player);
                    }
                }
                else
                {
                    if (leftWeapon != null && leftWeapon.oh_tap_z_action != null)
                    {
                        player.UpdateWhichHandCharacterIsUsing(usingRightHand: false);
                        player.pInventory.currentItemBeingUsed = leftWeapon;
                        leftWeapon.oh_tap_z_action.PerformAction(player);
                    }
                }
            }
        }
    }
}