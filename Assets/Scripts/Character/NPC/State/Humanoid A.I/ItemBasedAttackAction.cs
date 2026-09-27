using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "A.I/Actions/Humanoid A.I Actions/Item Based Attack Action")]
    public class ItemBasedAttackAction : ScriptableObject
    {
        [Header("攻击类型")]
        public bool isLightAttack;

        [Header("攻击行为设置")]
        public bool isRightHandAction = true;

        [Header("攻击行为设置 - Values")]
        public int attackScore = 3;
        public float recoveryTime = 2f;
        public float maxAttackAngle = 35f;
        public float minAttackAngle = -35f;
        public float maxDistNeededToAttack = 2.5f;
        public float minDistNeededToAttack = 0f;

        public void PerformAttackAction(EnemyManager enemy)
        {
            if (isRightHandAction)
            {
                enemy.UpdateWhichHandCharacterIsUsing(usingRightHand: true);
                PerformRightHandItemActionBasedAttackType(enemy);
            }
            else
            {
                enemy.UpdateWhichHandCharacterIsUsing(usingRightHand: false);
                PerformLeftHandItemActionBasedAttackType(enemy);
            }
        }

        void PerformRightHandItemActionBasedAttackType(EnemyManager enemy)
        {
            if (enemy.aiSettings.combatStyle == NPCCombatStyle.melee)
                PerformRightHandMeleeAction(enemy);
        }

        void PerformLeftHandItemActionBasedAttackType(EnemyManager enemy)
        {
            if (enemy.aiSettings.combatStyle == NPCCombatStyle.melee)
                PerformLeftHandMeleeAction(enemy);
            else if (enemy.aiSettings.combatStyle == NPCCombatStyle.archer)
                PerformLeftHandArcherAction(enemy);
        }

        void PerformRightHandMeleeAction(EnemyManager enemy)
        {
            WeaponItem rightWeapon = enemy.eInventory.rightWeapon;
            if (rightWeapon != null)
            {
                if (enemy.isTwoHandingWeapon)
                {
                    if (isLightAttack)
                    {
                        if (rightWeapon.th_tap_e_action != null)
                            rightWeapon.th_tap_e_action.PerformAction(enemy);
                        else
                            Debug.LogWarning("rightWeapon.th_tap_e_action = null");
                    }
                    else
                    {
                        if (rightWeapon.th_tap_r_action != null)
                            rightWeapon.th_tap_r_action.PerformAction(enemy);
                        else
                            Debug.LogWarning("rightWeapon.th_tap_r_action = null");
                    }
                }
                else
                {
                    if (isLightAttack)
                    {
                        if (rightWeapon.oh_tap_e_action != null)
                            rightWeapon.oh_tap_e_action.PerformAction(enemy);
                        else
                            Debug.LogWarning("rightWeapon.oh_tap_e_action = null");
                    }
                    else
                    {
                        if (rightWeapon.oh_tap_r_action != null)
                            rightWeapon.oh_tap_r_action.PerformAction(enemy);
                        else
                            Debug.LogWarning("rightWeapon.oh_tap_r_action = null");
                    }
                }
            }
            else
            {
                Debug.LogWarning("enemy.eInventory.rightWeapon == null");
            }
        }

        void PerformLeftHandMeleeAction(EnemyManager enemy)
        {
            WeaponItem leftWeapon = enemy.eInventory.leftWeapon;
            if (leftWeapon != null)
            {
                if (enemy.isTwoHandingWeapon)
                {
                    if (isLightAttack)
                    {
                        if (leftWeapon.th_tap_q_action != null)
                            leftWeapon.th_tap_q_action.PerformAction(enemy);
                        else
                            Debug.LogWarning("leftWeapon.th_tap_q_action = null");
                    }
                    else
                    {
                        if (leftWeapon.th_tap_z_action != null)
                            leftWeapon.th_tap_z_action.PerformAction(enemy);
                        else
                            Debug.LogWarning("leftWeapon.th_tap_z_action = null");
                    }
                }
                else
                {
                    if (isLightAttack)
                    {
                        if (leftWeapon.oh_tap_q_action != null)
                            leftWeapon.oh_tap_q_action.PerformAction(enemy);
                        else
                            Debug.LogWarning("leftWeapon.oh_tap_q_action = null");
                    }
                    else
                    {
                        if (leftWeapon.oh_tap_z_action != null)
                            leftWeapon.oh_tap_z_action.PerformAction(enemy);
                        else
                            Debug.LogWarning("leftWeapon.oh_tap_z_action = null");
                    }
                }
            }
            else
            {
                Debug.LogWarning("enemy.eInventory.leftWeapon == null");
            }
        }

        void PerformLeftHandArcherAction(EnemyManager enemy)
        {
            WeaponItem leftWeapon = enemy.eInventory.leftWeapon;
            if (leftWeapon != null)
            {
                if (leftWeapon.oh_tap_e_action != null)
                    leftWeapon.oh_tap_e_action.PerformAction(enemy);
                else
                    Debug.LogWarning("leftWeapon.oh_tap_e_action == null");
            }
            else
            {
                Debug.LogWarning("enemy.eInventory.leftWeapon == null");
            }
        }
    }
}