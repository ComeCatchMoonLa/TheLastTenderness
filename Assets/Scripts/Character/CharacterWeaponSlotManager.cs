using UnityEngine;

namespace CatchMoon
{
    public class CharacterWeaponSlotManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("武器槽")]
        public WeaponHolderSlot leftHandSlot;  // 左手武器槽
        public WeaponHolderSlot rightHandSlot; // 右手武器槽
        public WeaponHolderSlot backSlot;      // 背部武器槽

        [Header("Hand IK Targets")]
        public LeftHandIKTarget leftHandIKTarget;
        public RightHandIKTarget rightHandIKTarget;

        public DamageCollider leftHandDC;  // 左手武器伤害碰撞器
        public DamageCollider rightHandDC; // 右手武器伤害碰撞器

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
            LoadWeaponHolderSlots();
        }

        /// <summary>
        /// 加载武器槽
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
        /// 加载左手右手的武器
        /// </summary>
        /// <param name="twoHandFlag">双手持武器模式</param>
        public virtual void LoadWeaponsOnBothHands()
        {
            LoadWeaponOnSlot(character.cInventory.leftWeapon, true);
            LoadWeaponOnSlot(character.cInventory.rightWeapon, false);
        }

        /// <summary>
        /// 在槽中加载武器
        /// </summary>
        /// <param name="weaponItem">武器项</param>
        /// <param name="isLeft">是否为左手武器</param>
        public virtual bool LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (weaponItem == null || weaponItem.modelPrefab == null)
            {
                Debug.LogError("model is null.");
                return false;
            }

            if (isLeft)
                return LoadLeftHandWeapon(weaponItem);
            if (leftHandSlot.currentWeapon != null && leftHandSlot.currentWeapon.weaponType == WeaponType.bow)
                return StowRightWeaponOnBack(weaponItem);
            return LoadRightHandWeapon(weaponItem);
        }

        bool LoadLeftHandWeapon(WeaponItem weaponItem)
        {
            if (!PlaceModel(leftHandSlot, weaponItem))
                return false;

            LoadWeaponDamageCollider(weaponItem, true);
            if (weaponItem.weaponType == WeaponType.unarmed)
            {
                character.animator.Play("Left Arm Empty");
            }
            else if (weaponItem.weaponType == WeaponType.bow)
            {
                character.cAnimator.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, new AnimationOptions { CanRotate = true });
                character.animator.Play("Left Arm Empty");
                character.animator.runtimeAnimatorController = weaponItem.weaponController;
            }
            else
            {
                character.cAnimator.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, new AnimationOptions { CanRotate = true });
            }
            return true;
        }

        bool StowRightWeaponOnBack(WeaponItem weaponItem)
        {
            if (!backSlot.LoadWeaponModel(weaponItem))
                return false;

            backSlot.currentWeapon = weaponItem;
            rightHandSlot.currentWeapon = weaponItem;
            rightHandSlot.UnloadWeaponAndDestroy();
            character.animator.Play("Right Arm Empty");
            return true;
        }

        bool LoadRightHandWeapon(WeaponItem weaponItem)
        {
            if (!PlaceModel(rightHandSlot, weaponItem))
                return false;

            LoadWeaponDamageCollider(weaponItem, false);
            LoadTwoHandIKTarget();
            if (character.isTwoHandingWeapon)
                MoveLeftWeaponToBack();
            else
                backSlot.UnloadWeaponAndDestroy();
            character.animator.runtimeAnimatorController = weaponItem.weaponController;
            return true;
        }

        bool PlaceModel(WeaponHolderSlot slot, WeaponItem weaponItem)
        {
            if (!slot.LoadWeaponModel(weaponItem))
                return false;
            slot.currentWeapon = weaponItem;
            return true;
        }

        void MoveLeftWeaponToBack()
        {
            WeaponItem moving = leftHandSlot.currentWeapon;
            if (moving == null || !backSlot.LoadWeaponModel(moving))
                return;

            backSlot.currentWeapon = moving;
            leftHandSlot.currentWeapon = character.cInventory.unarmed;
            leftHandSlot.UnloadWeaponAndDestroy();
            character.animator.Play("Left Arm Empty");
        }

        /// <summary>
        /// 加载武器伤害触发器
        /// </summary>
        /// <param name="weaponItem">武器项</param>
        /// <param name="isLeft">是否为左手武器</param>
        protected virtual void LoadWeaponDamageCollider(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                if (leftHandSlot.currentWeapon.weaponType == WeaponType.unarmed
                    || leftHandSlot.currentWeapon.weaponType == WeaponType.faithCaster
                    || leftHandSlot.currentWeapon.weaponType == WeaponType.pyromancyCaster
                    || leftHandSlot.currentWeapon.weaponType == WeaponType.spellCaster
                    || leftHandSlot.currentWeapon.weaponType == WeaponType.bow
                    || leftHandSlot.currentWeapon.weaponType == WeaponType.melee_OH_Shield)
                {
                    character.cEffects.leftWeaponFX = null;
                    return;
                }

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
                NoteBuffFire(weaponItem, leftHandSlot.currentWeaponModel.transform);
            }
            else
            {
                if (rightHandSlot.currentWeapon.weaponType == WeaponType.unarmed
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
                NoteBuffFire(weaponItem, rightHandSlot.currentWeaponModel.transform);
            }
        }

        /// <summary>
        /// 加载两手IK的目标
        /// </summary>
        void NoteBuffFire(WeaponItem weaponItem, Transform model)
        {
            GameObject fire = WeaponBuff.FindFire(model);
            if (fire == null) return;
            character.cCombat.buffFire = fire;
            bool show = character.cCombat.weaponBuff.active && character.cCombat.weaponBuff.weapon == weaponItem;
            WeaponBuff.ShowFire(fire, show);
        }

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