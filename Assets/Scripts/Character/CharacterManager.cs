using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class CharacterManager : MonoBehaviour
    {
        [HideInInspector] public Animator animator;
        [HideInInspector] public Rigidbody rigidBody;
        [HideInInspector] public CapsuleCollider cCollider; // 悬浮碰撞器

        [HideInInspector] public CharacterAnimatorManager cAnimator;
        [HideInInspector] public CharacterCombatManager cCombat;
        [HideInInspector] public CharacterEffectsManager cEffects;
        [HideInInspector] public CharacterInventoryManager cInventory;
        [HideInInspector] public CharacterStatsManager cStats;
        [HideInInspector] public CharacterWeaponSlotManager cWeaponSlot;
        [HideInInspector] public CharacterSoundFXManager cSoundFX;

        [Header("角色类型")]
        public CharacterType characterType;

        [Header("Transforms")]
        public Transform parentTransform;
        public Transform lockOnTransform;

        [Header("战斗 Flags")]
        public bool canBeRiposted;              // 可以被反击
        public bool canBeParried;               // 可以被弹反
        public bool canDoCombo;                 // 可以连击

        public bool isUsingRightHand;           // 使用右手中
        public bool isUsingLeftHand;            // 使用左手中
        public bool isTwoHandingWeapon;         // 双手持武器中
      
        public bool isBeingBackStabbed;         // 正在被背刺
        public bool isBeingRiposted;            // 正在被反击
        public bool isPerformingBackSttbbed;    // 正在背刺
        public bool isPerformingRiposted;       // 正在反击

        [Header("移动 Flags")]
        public bool canRotate;          // 可以旋转
        public bool isInAir;            // 在空中
        public bool isInteracting;      // 正在交互中
        public bool isJumping;          // 跳跃中
        public bool isRolling;          // 翻滚中
        public bool isRotatingWithRootMotion;   // 用动画根运动实现旋转
        public bool isSprinting;        // 冲刺中

        [Header("对话")]
        public bool talkWithSB;
        [SerializeField] List<string> talkAnimations;
        List<string> potentialTalkAnimations;
        string lastTalkAnimationPlayed;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
            cCollider = GetComponent<CapsuleCollider>();

            cAnimator = GetComponent<CharacterAnimatorManager>();
            cCombat = GetComponent<CharacterCombatManager>();
            cEffects = GetComponent<CharacterEffectsManager>();
            cInventory = GetComponent<CharacterInventoryManager>();
            cStats = GetComponent<CharacterStatsManager>();
            cWeaponSlot = GetComponent <CharacterWeaponSlotManager>();
            cSoundFX = GetComponent<CharacterSoundFXManager>();
        }
        protected virtual void Start()
        {
            #region 判断空引用异常
            if (animator == null)
                Debug.LogError($"{transform.name}: animator == null");
            if (rigidBody == null)
                Debug.LogError($"{transform.name}: rigidBody == null");
            if (cCollider == null)
                Debug.LogError($"{transform.name}: cCollider == null");

            if (cAnimator == null)
                Debug.LogError($"{transform.name}: cAnimator == null");
            if (cCombat == null)
                Debug.LogError($"{transform.name}: cCombat == null");
            if (cEffects == null)
                Debug.LogError($"{transform.name}: cEffects == null");
            if (cInventory == null)
                Debug.LogError($"{transform.name}: cInventory == null");
            if (cStats == null)
                Debug.LogError($"{transform.name}: cStats == null");
            if (cWeaponSlot == null)
                Debug.LogError($"{transform.name}: cWeaponSlot == null");
            if (cSoundFX == null)
                Debug.LogError($"{transform.name}: cSoundFX == null");
            #endregion

            if (talkAnimations.Count < 2)
                Debug.LogWarning("talkAnimations.Count < 2");

            parentTransform = transform.parent;
        }
        protected virtual void Update()
        {
            if (cStats.isDead) return;

            UpdateBoolsValue();

            if (talkWithSB)
                RollForAnimation();
        }

        public virtual void UpdateWhichHandCharacterIsUsing(bool usingRightHand) 
        {
            isUsingLeftHand = !(isUsingRightHand = usingRightHand); 
        }

        protected virtual void UpdateBoolsValue()
        {
            animator.SetBool("isBlocking", cCombat.isBlocking);
            animator.SetBool("isDead", cStats.isDead);
            animator.SetBool("isTwoHandingWeapon", isTwoHandingWeapon);
        }

        void RollForAnimation()
        {
            if (isInteracting) return;

            potentialTalkAnimations = new List<string>();
            foreach (string animation in talkAnimations)
                if (animation != lastTalkAnimationPlayed)
                    potentialTalkAnimations.Add(animation);

            lastTalkAnimationPlayed = potentialTalkAnimations[Random.Range(0, potentialTalkAnimations.Count)];
            cAnimator.PlayTargetAnimation(lastTalkAnimationPlayed, true);
        }
    }
}