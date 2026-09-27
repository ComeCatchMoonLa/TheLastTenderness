using System;
using UnityEngine;

namespace CatchMoon
{
    public class CharacterStatsManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("名称")]
        public string cName;

        [Header("Flags")]
        public bool isDead;         // 是否死亡
        public bool isInvulnerable; // 是否无敌

        [Header("队伍ID")]
        public int teamID = 2;

        [Header("HP SP MP")]
        public float maxHP;     // 最大生命
        public float currentHP; // 当前生命
        [Space(5)]
        public float maxStamina;          // 最大体力
        public float currentStamina;      // 当前体力
        [Space(5)]
        public float maxMP;     // 最大蓝量
        public float currentMP; // 当前蓝量

        [Header("等级")]
        public int healthLevel = 10;        // 血条等级
        public int staminaLevel = 10;       // 体力条等级
        public int focusLevel = 10;         // 蓝条等级
        public int poiseLevel = 10;
        public int strengthLevel = 10;
        public int dexterityLevel = 10;
        public int intelligenceLevel = 10;
        public int faithLevel = 10;

        [Header("Poise")]
        public float totalPoiseDefence;             // 伤害计算时的总镇定(火力差很大的话可以形成压制)
        public float offensivePoiseBonus;           // 你用武器攻击时的镇定
        public float armorPoiseBonus;               // 通过装备获得的镇定
        public float totalPoiseResetTime = 15f;
        public float poiseResetTimer = 0f;

        [Header("盔甲的伤害吸收率")]    
        [Range(0, 1)] public float headArmorPDA;
        [Range(0, 1)] public float torsoArmorPDA;
        [Range(0, 1)] public float hipsArmorPDA;
        [Space(5)]
        [Range(0, 1)] public float headArmorFDA;
        [Range(0, 1)] public float torsoArmorFDA;
        [Range(0, 1)] public float hipsArmorFDA; 
        [Space(5)]
        [Range(0, 1)] public float headArmorMDA;
        [Range(0, 1)] public float torsoArmorMDA;
        [Range(0, 1)] public float hipsArmorMDA;
        [Space(5)]
        [Range(0, 1)] public float headArmorLDA;
        [Range(0, 1)] public float torsoArmorLDA;
        [Range(0, 1)] public float hipsArmorLDA;
        [Space(5)]
        [Range(0, 1)] public float headArmorDDA;
        [Range(0, 1)] public float torsoArmorDDA;
        [Range(0, 1)] public float hipsArmorDDA;

        [Header("防御时的伤害吸收率")]
        [Range(0, 1)] public float blockingPDA;
        [Range(0, 1)] public float blockingFDA;
        [Range(0, 1)] public float blockingMDA;
        [Range(0, 1)] public float blockingLDA;
        [Range(0, 1)] public float blockingDDA;

        [Header("防御时的体力兑换率")]
        [Range(0, 1)] public float blockingStabilityRating;

        [Header("体力恢复计时器")]
        public int staminaRegenerationAmount = 300;
        public int staminaRegenerationAmountWhilstBlocking = 20;
        public float staminaRegenTimer;

        protected virtual void Awake()
        {
            currentHP = maxHP = SetMaxHealthFromHealthLevel();
            currentStamina = maxStamina = SetMaxStaminaFromStaminaLevel();
            currentMP = maxMP = SetMaxFocusPointsFromFocusLevel();

            character = GetComponent<CharacterManager>();
        }
        protected virtual void Start()
        {
            totalPoiseDefence = armorPoiseBonus;
        }
        protected virtual void Update()
        {
            RegenerateStamina();
            HandlePoiseResetTimer();
        }

        float SetMaxHealthFromHealthLevel()
        {
            // 最大血量与血条等级的转换公式
            maxHP = healthLevel * 10;
            return maxHP;
        }
        float SetMaxStaminaFromStaminaLevel()
        {
            // 体力上线与体力条等级的转换公式
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }
        float SetMaxFocusPointsFromFocusLevel()
        {
            // 蓝量上线与蓝条等级的转换公式
            maxMP = focusLevel * 10;
            return maxMP;
        }

        /// <summary>
        /// 受伤
        /// </summary>
        /// <param name="pd">物理伤害</param>
        /// <param name="damageAnimation">受伤动画</param>
        public virtual bool TakeDamage(string damageAnimation, float pd = 0f, float fd = 0f, float md = 0f, float ld = 0f, float dd = 0f)
        {
            if (character.cStats.isDead || character.cStats.isInvulnerable) return false;

            /// 计算最终伤害占原始伤害的比率(head承受60%伤害, torso承受30%伤害, hips承受10%伤害)
            Func<float, float, float, float> TotalDamageRate = (headDA, torsoDA, hipsDA) =>  (1 - headDA) * 0.6f + (1 - torsoDA) * 0.3f + (1 - hipsDA) * 0.1f;

            pd *= TotalDamageRate(headArmorPDA, torsoArmorPDA, hipsArmorPDA);
            fd *= TotalDamageRate(headArmorFDA, torsoArmorFDA, hipsArmorFDA);
            md *= TotalDamageRate(headArmorMDA, torsoArmorMDA, hipsArmorMDA);
            ld *= TotalDamageRate(headArmorLDA, torsoArmorLDA, hipsArmorLDA);
            dd *= TotalDamageRate(headArmorDDA, torsoArmorDDA, hipsArmorDDA);

            float finalDamage = pd + fd + md + ld + dd;
            //Debug.Log($"最终伤害: {finalDamage:N0}.");
            currentHP -= finalDamage;

            character.cSoundFX.PlayRandomDamageSoundsFX();

            if (damageAnimation != null)
                character.cAnimator.PlayTargetAnimation(damageAnimation, true);

            return true;
        }

        /// <summary>
        /// 死亡
        /// </summary>
        /// <param name="deathAnimation">死亡动画</param>
        public virtual void Death(CharacterManager character, string deathAnimation = "Death")
        {
            currentHP = 0;
            character.cAnimator.PlayTargetAnimation(deathAnimation, true);
            character.cStats.isDead = true;
        }

        /// <summary>
        /// [处理]镇定重置[计时器]
        /// </summary>
        protected virtual void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
                poiseResetTimer -= Time.deltaTime;
            else
                totalPoiseDefence = armorPoiseBonus;
        }

        public virtual bool AddHP(float addAmount)
        {
            if (currentHP < maxHP)
            {
                currentHP = Mathf.Min(currentHP + addAmount, maxHP);
                return true;
            }
            else return false;
        }
        public virtual bool DeductHP(float deductAmount)
        {
            if (currentHP >= deductAmount)
            {
                currentHP -= deductAmount;
                return true;
            }
            else return false;
        }

        public virtual bool AddStamina(float addAmount)
        {
            if (currentStamina < maxStamina)
            {
                currentStamina = Mathf.Min(currentStamina + addAmount, maxStamina);
                return true;
            }
            else return false;
        }
        public virtual bool DeductStamina(float deductAmount)
        {
            if (currentStamina >= deductAmount)
            {
                currentStamina -= deductAmount;
                return true;
            }
            else return false;
        }

        public virtual bool AddMP(float addAmount)
        {
            if (currentMP < maxMP)
            {
                currentMP = Mathf.Min(currentMP + addAmount, maxMP);
                return true;
            }
            else return false;
        }
        public virtual bool DeductMP(float deductAmount)
        {
            if (currentMP >= deductAmount)
            {
                currentMP -= deductAmount;
                return true;
            }
            else return false;
        }

        public virtual void RegenerateStamina()
        {
            if (character.isInteracting || character.isSprinting)
            {
                staminaRegenTimer = 0;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;
                if (currentStamina < maxStamina && staminaRegenTimer > 1f)
                {
                    if (character.cCombat.isBlocking)
                        currentStamina += staminaRegenerationAmountWhilstBlocking * Time.deltaTime;
                    else
                        currentStamina += staminaRegenerationAmount * Time.deltaTime;
                }
            }
        }
    }
}