using UnityEngine;

namespace CatchMoon
{
    public class CharacterWeaponSlotManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Œ‰∆˜≤€")]
        public WeaponHolderSlot leftHandSlot;  // ◊Û ÷Œ‰∆˜≤€
        public WeaponHolderSlot rightHandSlot; // ”“ ÷Œ‰∆˜≤€
        public WeaponHolderSlot backSlot;      // ±≥≤øŒ‰∆˜≤€

        [Header("Hand IK Targets")]
        public LeftHandIKTarget leftHandIKTarget;
        public RightHandIKTarget rightHandIKTarget;

        public DamageCollider leftHandDC;  // ◊Û ÷Œ‰∆˜…À∫¶≈ˆ◊≤∆˜
        public DamageCollider rightHandDC; // ”“ ÷Œ‰∆˜…À∫¶≈ˆ◊≤∆˜

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
            LoadWeaponHolderSlots();
        }

        /// <summary>
        /// º”‘ÿŒ‰∆˜≤€
        /// </summary>
        protected virtual void LoadWeaponHolderSlots()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.slotType == ItemSlotType.leftHandSlot)
                    leftHandSlot = weaponSlot;
                else if (weaponSlot.slotType == ItemSlotType.rightHandSlot)
                    rightHandSlot = weaponSlot;
                else if (weaponSlot.slotType == ItemSlotType.backSlot)
                    backSlot = weaponSlot;
            }

            if (leftHandSlot == null) Debug.LogError($"{character.transform.name}: leftHandSlot is null.");
            if (rightHandSlot == null) Debug.LogError($"{character.transform.name}: rightHandSlot is null.");
            if (backSlot == null) Debug.LogError($"{character.transform.name}: backSlot is null.");
        }

        /// <summary>
        /// º”‘ÿ◊Û ÷”“ ÷µƒŒ‰∆˜
        /// </summary>
        /// <param name="twoHandFlag">À´ ÷≥÷Œ‰∆˜ƒ£ Ω</param>
        public virtual void LoadWeaponsOnBothHands()
        {
            LoadWeaponOnSlot(character.cInventory.leftWeapon, true);
            LoadWeaponOnSlot(character.cInventory.rightWeapon, false);
        }

        /// <summary>
        /// ‘⁄≤€÷–º”‘ÿŒ‰∆˜
        /// </summary>
        /// <param name="weaponItem">Œ‰∆˜œÓ</param>
        /// <param name="isLeft"> «∑ÒŒ™◊Û ÷Œ‰∆˜</param>
        public virtual void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadWeaponDamageCollider(weaponItem, true);

                if (weaponItem.weaponType == WeaponType.unaremd)
                    character.animator.Play("Left Arm Empty");
                else
                    character.cAnimator.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, true);

                if (weaponItem.weaponType == WeaponType.bow)
                {
                    character.animator.Play("Left Arm Empty");
                    character.animator.runtimeAnimatorController = weaponItem.weaponController;
                }
            }
            else
            {
                if (leftHandSlot.currentWeapon != null && leftHandSlot.currentWeapon.weaponType == WeaponType.bow)
                {
                    backSlot.currentWeapon = rightHandSlot.currentWeapon = weaponItem;
                    backSlot.LoadWeaponModel(weaponItem);
                    rightHandSlot.UnloadWeaponAndDestroy();
                    character.animator.Play("Right Arm Empty");
                }
                else
                {
                    rightHandSlot.currentWeapon = weaponItem;
                    rightHandSlot.LoadWeaponModel(weaponItem);
                    LoadWeaponDamageCollider(weaponItem, false);
                    LoadTwoHandIKTarget();

                    if (character.isTwoHandingWeapon)
                    {
                        backSlot.currentWeapon = leftHandSlot.currentWeapon;
                        leftHandSlot.currentWeapon = character.cInventory.unaremd;
                        backSlot.LoadWeaponModel(backSlot.currentWeapon);
                        leftHandSlot.UnloadWeaponAndDestroy();
                        character.animator.Play("Left Arm Empty");
                    }
                    else
                    {
                        backSlot.UnloadWeaponAndDestroy();
                    }
                    character.animator.runtimeAnimatorController = weaponItem.weaponController;
                }
            }
        }

        /// <summary>
        /// º”‘ÿŒ‰∆˜…À∫¶¥•∑¢∆˜
        /// </summary>
        /// <param name="weaponItem">Œ‰∆˜œÓ</param>
        /// <param name="isLeft"> «∑ÒŒ™◊Û ÷Œ‰∆˜</param>
        protected virtual void LoadWeaponDamageCollider(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                if (leftHandSlot.currentWeapon.weaponType == WeaponType.unaremd
                    || leftHandSlot.currentWeapon.weaponType == WeaponType.faithCaster
                    || leftHandSlot.currentWeapon.weaponType == WeaponType.pyromancyCaster
                    || leftHandSlot.currentWeapon.weaponType == WeaponType.spellCaster
                    || leftHandSlot.currentWeapon.weaponType == WeaponType.bow
                    || leftHandSlot.currentWeapon.weaponType == WeaponType.melee_OH_Shield)
                    return;

                leftHandDC = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                if (leftHandDC == null)
                {
                    Debug.LogError("leftHandDamageCollider is null.");
                    return;
                }
                leftHandDC.character = GetComponentInParent<CharacterManager>(); 
                if (leftHandDC.character == null)
                {
                    Debug.LogError("leftHandDamageCollider.characterManager is null.");
                    return;
                }

                leftHandDC.teamID = character.cStats.teamID;
                leftHandDC.poiseBreak = weaponItem.poiseBreak;
                leftHandDC.pd = weaponItem.pd;
                leftHandDC.fd = weaponItem.fd;
                leftHandDC.md = weaponItem.md;
                leftHandDC.ld = weaponItem.ld;
                leftHandDC.dd = weaponItem.dd;

                character.cEffects.leftWeaponFX = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            }
            else
            {
                if (rightHandSlot.currentWeapon.weaponType == WeaponType.unaremd
                    || rightHandSlot.currentWeapon.weaponType == WeaponType.faithCaster
                    || rightHandSlot.currentWeapon.weaponType == WeaponType.pyromancyCaster
                    || rightHandSlot.currentWeapon.weaponType == WeaponType.spellCaster)
                    return;

                rightHandDC = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                if (rightHandDC == null)
                {
                    Debug.LogError("rightHandDamageCollider is null.");
                    return;
                }
                rightHandDC.character = GetComponentInParent<CharacterManager>();
                if (rightHandDC.character == null)
                {
                    Debug.LogError("rightHandDamageCollider.characterManager is null.");
                    return;
                }

                rightHandDC.teamID = character.cStats.teamID;
                rightHandDC.poiseBreak = weaponItem.poiseBreak;
                rightHandDC.pd = weaponItem.pd;
                rightHandDC.fd = weaponItem.fd;
                rightHandDC.md = weaponItem.md;
                rightHandDC.ld = weaponItem.ld;
                rightHandDC.dd = weaponItem.dd;

                character.cEffects.rightWeaponFX = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            }
        }

        /// <summary>
        /// º”‘ÿ¡Ω ÷IKµƒƒø±Í
        /// </summary>
        public virtual void LoadTwoHandIKTarget()
        {
            if (rightHandSlot.currentWeapon.weaponType == WeaponType.melee_TH)
            {
                leftHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
                rightHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();

                character.cAnimator.SetHandIKForWeapon(leftHandIKTarget, rightHandIKTarget, character.isTwoHandingWeapon);
            }
        }
    }
}