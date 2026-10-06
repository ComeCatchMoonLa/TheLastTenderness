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
            TickRegen(Time.deltaTime);
            TickCurse(Time.deltaTime);
            TickPoison(Time.deltaTime);
        }

        public float regenPerSecond;
        public float regenTimeLeft;

        public float curseMeter;
        public float curseCapacity;
        public float curseDecay;
        public bool curseInSource;

        public float poisonMeter;
        public float poisonCapacity;
        public float poisonDuration;
        public float poisonDamagePerSecond;
        public float poisonTimeLeft;

        public bool BeginRegen(float perSecond, float seconds)
        {
            if (perSecond <= 0f || seconds <= 0f) return false;
            regenPerSecond = perSecond;
            regenTimeLeft = seconds;
            return true;
        }

        public void TickCurse(float deltaTime)
        {
            if (isDead || curseInSource) return;
            curseMeter = CurseMeter.Decay(name, curseMeter, curseDecay, deltaTime);
        }

        public bool GainCurse(float gain, float resist)
        {
            if (isDead) return false;
            curseMeter = StatusBuildup.Add(isInvulnerable, name, curseMeter, gain, resist, curseCapacity);
            if (!CurseMeter.TryKill(curseMeter, curseCapacity, out float cleared)) return false;
            curseMeter = cleared;
            currentHP = 0f;
            isDead = true;
            return true;
        }

        public void TickPoison(float deltaTime)
        {
            if (isDead) return;
            currentHP = PoisonDoT.Tick(name, currentHP, poisonTimeLeft, poisonDamagePerSecond, deltaTime, out poisonTimeLeft);
        }

        public bool GainPoison(float gain, float resist)
        {
            if (isDead) return false;
            poisonMeter = StatusBuildup.Add(isInvulnerable, name, poisonMeter, gain, resist, poisonCapacity);
            if (!PoisonDoT.TryStart(name, poisonMeter, poisonCapacity, poisonDuration, out float timeLeft, out float cleared))
                return false;
            poisonMeter = cleared;
            poisonTimeLeft = timeLeft;
            return true;
        }

        public bool DrinkPurpleMoss()
        {
            if (poisonTimeLeft <= 0f) return false;
            poisonTimeLeft = PoisonDoT.Stop();
            return true;
        }

        public bool DrinkBlessing()
        {
            if (isDead) return false;
            if (!CurseMeter.TryBless(curseMeter, curseCapacity, out float cleared)) return false;
            curseMeter = cleared;
            return true;
        }

        public void TickRegen(float deltaTime)
        {
            if (regenTimeLeft <= 0f || regenPerSecond <= 0f || deltaTime <= 0f) return;
            float step = deltaTime < regenTimeLeft ? deltaTime : regenTimeLeft;
            currentHP = RegenHeal.Apply(currentHP, maxHP, regenPerSecond, step);
            regenTimeLeft -= step;
        }

        public static float LevelTimesTen(int level)
        {
            return level * 10;
        }
        float SetMaxHealthFromHealthLevel()
        {
            // 最大血量与血条等级的转换公式
            maxHP = LevelTimesTen(healthLevel);
            return maxHP;
        }
        float SetMaxStaminaFromStaminaLevel()
        {
            // 体力上线与体力条等级的转换公式
            maxStamina = LevelTimesTen(staminaLevel);
            return maxStamina;
        }
        float SetMaxFocusPointsFromFocusLevel()
        {
            // 蓝量上线与蓝条等级的转换公式
            maxMP = LevelTimesTen(focusLevel);
            return maxMP;
        }
        public static int MemorySlotCount(int focusLevel)
        {
            if (focusLevel >= 99) return 10;
            if (focusLevel >= 80) return 9;
            if (focusLevel >= 60) return 8;
            if (focusLevel >= 50) return 7;
            if (focusLevel >= 40) return 6;
            if (focusLevel >= 30) return 5;
            if (focusLevel >= 24) return 4;
            if (focusLevel >= 18) return 3;
            if (focusLevel >= 14) return 2;
            if (focusLevel >= 10) return 1;
            return 0;
        }

        /// <summary>
        /// 受伤
        /// </summary>
        /// <param name="damage">五段伤害</param>
        /// <param name="damageAnimation">受伤动画</param>
        public virtual bool TakeDamage(string damageAnimation, DamageSegments damage, bool playHurtSound = true)
        {
            if (character.cStats.isDead || character.cStats.isInvulnerable) return false;

            float pd = damage.Physical * CombinedArmorRate(headArmorPDA, torsoArmorPDA, hipsArmorPDA);
            float fd = damage.Fire * CombinedArmorRate(headArmorFDA, torsoArmorFDA, hipsArmorFDA);
            float md = damage.Magic * CombinedArmorRate(headArmorMDA, torsoArmorMDA, hipsArmorMDA);
            float ld = damage.Lightning * CombinedArmorRate(headArmorLDA, torsoArmorLDA, hipsArmorLDA);
            float dd = damage.Dark * CombinedArmorRate(headArmorDDA, torsoArmorDDA, hipsArmorDDA);

            float finalDamage = pd + fd + md + ld + dd;
            //Debug.Log($"最终伤害: {finalDamage:N0}.");
            currentHP -= finalDamage;

            if (playHurtSound)
                character.cSoundFX.PlayRandomDamageSoundsFX();

            if (damageAnimation != null)
                character.cAnimator.PlayTargetAnimation(damageAnimation, true);

            return true;
        }

        static float CombinedArmorRate(float head, float torso, float hips)
        {
            return (1f - head) * 0.6f + (1f - torso) * 0.3f + (1f - hips) * 0.1f;
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

        public virtual void DeductAttackStamina(float deductAmount)
        {
            currentStamina = Mathf.Max(currentStamina - deductAmount, -60f);
        }

        public virtual void EmptyStamina()
        {
            currentStamina = 0;
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
                    float amount = character.cCombat.isBlocking
                        ? staminaRegenerationAmountWhilstBlocking
                        : staminaRegenerationAmount;
                    currentStamina = Mathf.Min(currentStamina + amount * Time.deltaTime, maxStamina);
                }
            }
        }
    }
}