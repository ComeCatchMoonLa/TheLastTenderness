using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace CatchMoon
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("攻击动画名称")]
        [Space(5)]// 单手轻击
        public string ohLightAttack1 = "OH_Light_Attack_01";
        public string ohLightAttack2 = "OH_Light_Attack_02";
        [Space(5)]// 单手重击
        public string ohHeavyAttack1 = "OH_Heavy_Attack_01";
        public string ohHeavyAttack2 = "OH_Heavy_Attack_02";
        [Space(5)]// 双手轻击
        public string thLightAttack1 = "TH_Light_Attack_01";
        public string thLightAttack2 = "TH_Light_Attack_02";
        [Space(5)]// 双手重击                         
        public string thHeavyAttack1 = "TH_Heavy_Attack_01";
        public string thHeavyAttack2 = "TH_Heavy_Attack_02";

        [Header("Weapon Art")]
        public string weapon_art = "Parry";

        [Header("最后一击的动画名称")]
        public string lastAttack;      // 最后一次攻击动画的名称

        [Header("IK")]
        protected RigBuilder rigBuilder;
        public TwoBoneIKConstraint leftHandConstraint;
        public TwoBoneIKConstraint rightHandConstraint;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
            rigBuilder = GetComponent<RigBuilder>();

            if (!rigBuilder) Debug.LogWarning("null");
        }
        protected virtual void Update()
        {
            UpdateHandIKWeight();
        }

        /// <summary>
        /// 播放目标动画
        /// </summary>
        /// <param name="targetAnim">目标动画</param>
        /// <param name="isInteracting">是否交互</param>
        public void PlayTargetAnimation(
            string targetAnim,
            bool isInteracting,
            bool canRotate = false,
            bool mirrorAnim = false,
            bool isRotatingWithRootMotion = false)
        {
            character.animator.applyRootMotion = isInteracting;
            character.isInteracting = isInteracting;
            character.canRotate = canRotate;
            character.animator.SetBool("isMirrored", mirrorAnim);
            character.isRotatingWithRootMotion = isRotatingWithRootMotion;
            character.animator.CrossFade(targetAnim, 0.2f);
        }

        /// <summary>
        /// 根据武器设置手的IK
        /// </summary>
        public virtual void SetHandIKForWeapon(LeftHandIKTarget leftHandIKTarget, RightHandIKTarget rightHandIKTarget, bool isTwoHandingWeapon)
        {
            leftHandConstraint.data.target = leftHandIKTarget.transform;
            rightHandConstraint.data.target = rightHandIKTarget.transform;
            leftHandConstraint.weight = rightHandConstraint.weight = 1;
            rigBuilder.Build();
        }

        public void UpdateHandIKWeight()
        {
            if (character.cInventory.rightWeapon.weaponType == WeaponType.melee_TH && character.isTwoHandingWeapon && !character.isInteracting)
                leftHandConstraint.weight = rightHandConstraint.weight = 1;
            else
                leftHandConstraint.weight = rightHandConstraint.weight = 0;
        }

        void DrainStamina()
        {
            if (character.isUsingRightHand)
            {
                WeaponItem rightWeapon = character.cInventory.rightWeapon;
                if (character.cCombat.attackType == AttackType.light_1
                    || character.cCombat.attackType == AttackType.light_2)
                {
                    character.cStats.DeductStamina(rightWeapon.baseStaminaCost * rightWeapon.laStaminaCostM);
                }
                else if (character.cCombat.attackType == AttackType.heavy_1
                    || character.cCombat.attackType == AttackType.heavy_2)
                {
                    character.cStats.DeductStamina(character.cInventory.rightWeapon.baseStaminaCost
                        * character.cInventory.rightWeapon.haStaminaCostM);
                }
            }
            else if (character.isUsingLeftHand)
            {
                WeaponItem leftWeapon = character.cInventory.leftWeapon;
                if (character.cCombat.attackType == AttackType.light_1
                    || character.cCombat.attackType == AttackType.light_2)
                {
                    character.cStats.DeductStamina(leftWeapon.baseStaminaCost
                        * leftWeapon.laStaminaCostM);
                }
                else if (character.cCombat.attackType == AttackType.heavy_1
                    || character.cCombat.attackType == AttackType.heavy_2)
                {
                    character.cStats.DeductStamina(leftWeapon.baseStaminaCost * leftWeapon.haStaminaCostM);
                }
            }
        }

        void GrantWeaponAttackingPoiseBonus()
        {
            WeaponItem currentWeaponBeingUsed = character.cInventory.currentItemBeingUsed as WeaponItem;
            character.cStats.totalPoiseDefence += currentWeaponBeingUsed.offensivePoiseBonus;
        }
        void ResetWeaponAttackingPoiseBonus()
        {
            character.cStats.totalPoiseDefence = character.cStats.armorPoiseBonus;
        }

        void EnableRotate()
        {
            character.canRotate = true;
        }
        void DisableRotate()
        {
            character.canRotate = false;
        }

        void EnableCombo()
        {
            character.canDoCombo = true;
        }
        void DisableCombo()
        {
            character.canDoCombo = false;
        }

        void EnableIsVulnerable()
        {
            character.cStats.isInvulnerable = true;
        }
        void DisableIsVulnerable()
        {
            character.cStats.isInvulnerable = false;
        }

        void EnableIsParrying()
        {
            character.cCombat.isParrying = true;
        }
        void DisableIsParrying()
        {
            character.cCombat.isParrying = false;
        }

        void OpenDamageCollider()
        {
            if (character.isUsingLeftHand)
                character.cWeaponSlot.leftHandDC.EnableDamageCollider();
            if (character.isUsingRightHand)
                character.cWeaponSlot.rightHandDC.EnableDamageCollider();

            character.cSoundFX.PlayRandomWeaponWhoosh();
        }
        void CloseDamageCollider()
        {
            if (character.isUsingLeftHand)
                character.cWeaponSlot.leftHandDC.DisableDamageCollider();
            if (character.isUsingRightHand)
                character.cWeaponSlot.rightHandDC.DisableDamageCollider();
        }

        void SucessfullyGetArrow()
        {
            GameObject loadedArrow = Instantiate(character.cInventory.currentAmmo.loadedItemModel, character.cWeaponSlot.rightHandSlot.overrideParentWhileGetting);
            character.cEffects.Ammo = loadedArrow;
        }
        void EndGetArrow()
        {
            if (character.cEffects.Ammo != null)
            {
                character.cEffects.Ammo.transform.parent = character.cWeaponSlot.rightHandSlot.overrideParentWhileHolding;
                character.cEffects.Ammo.transform.localPosition = Vector3.zero;
                character.cEffects.Ammo.transform.localRotation = Quaternion.identity;
                /// 设置左手的IK
            }
        }

        void ApplyPendingDamage()
        {
            character.cStats.TakeDamage(damageAnimation: null, pd: character.cCombat.pendingCriticalDamage);
        }

        void EnableCanBeParried()
        {
            character.canBeParried = true;
        }
        void DisableCanBeParried()
        {
            character.canBeParried = false;
        }
    }
}