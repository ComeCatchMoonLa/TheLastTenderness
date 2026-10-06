using UnityEngine;

namespace CatchMoon
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        PlayerManager player;

        [Header("灵魂数量")]
        public int soulCount = 0;
        public bool hasSoulRemnant;
        public int soulRemnant;
        public bool emberLit;
        public float emberBaseMaxHP;
        public string recordedEnding;

        public bool RecordEnding(string ending)
        {
            return CycleGate.TryRecord(ref recordedEnding, ending, name);
        }

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }
        protected override void Update()
        {
            float before = currentHP;
            base.Update();
            if (currentHP == before) return;
            if (player != null && player.ui != null && player.ui.hud != null && player.ui.hud.healthBar != null)
                player.ui.hud.healthBar.SetCurrentHP(currentHP);
        }

        protected override void Start()
        {
            base.Start();

            player.ui.hud.healthBar.SetMaxHP(maxHP);
            player.ui.hud.healthBar.SetCurrentHP(currentHP);

            player.ui.hud.staminaBar.SetMaxStamina(maxStamina);
            player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);

            player.ui.hud.focusPointsBar.SetMaxMP(maxMP);
            player.ui.hud.focusPointsBar.SetCurrentMP(currentMP);
        }

        public override bool TakeDamage(string damageAnimation, DamageSegments damage, bool playHurtSound = true)
        {
            if (base.TakeDamage(damageAnimation, damage, playHurtSound))
            {
                player.ui.hud.healthBar.SetCurrentHP(currentHP);

                if (currentHP <= 0)
                {
                    base.Death(player);
                    ForfeitSouls();
                    ExtinguishEmber();
                }

                return true;
            }
            else return false;
        }

        public override bool AddHP(float addAmount)
        {
            if (base.AddHP(addAmount))
            {
                player.ui.hud.healthBar.SetCurrentHP(currentHP);
                return true;
            }
            else return false;
        }
        public override bool DeductHP(float deductAmount)
        {
            if (base.DeductHP(deductAmount))
            {
                player.ui.hud.healthBar.SetCurrentHP(currentHP);
                return true;
            }
            else return false;
        }

        public override bool AddStamina(float addAmount)
        {
            if (base.AddStamina(addAmount))
            {
                player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);
                return true;
            }
            else return false;
        }
        public override bool DeductStamina(float deductAmount)
        {
            if (base.DeductStamina(deductAmount))
            {
                player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);
                return true;
            }
            else return false;
        }

        public override void DeductAttackStamina(float deductAmount)
        {
            base.DeductAttackStamina(deductAmount);
            player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);
        }

        public override void EmptyStamina()
        {
            base.EmptyStamina();
            player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);
        }

        public override bool AddMP(float addAmount)
        {
            if (base.AddMP(addAmount))
            {
                player.ui.hud.focusPointsBar.SetCurrentMP(currentMP);
                return true;
            }
            else return false;
        }
        public override bool DeductMP(float deductAmount)
        {
            if (base.DeductMP(deductAmount))
            {
                player.ui.hud.focusPointsBar.SetCurrentMP(currentMP);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 获取灵魂
        /// </summary>
        public void AddSouls(int souls)
        {
            SetSouls(soulCount + souls);
        }

        public void SetSouls(int souls)
        {
            soulCount = souls;
            if (player != null && player.ui != null && player.ui.hud != null && player.ui.hud.soulCountUI != null)
                player.ui.hud.soulCountUI.SetSoulCountText(soulCount);
        }

        public void ForfeitSouls()
        {
            hasSoulRemnant = SoulDrop.TryLeave(soulCount, out soulRemnant);
            SetSouls(0);
        }

        public bool RecoverRemnant()
        {
            if (!hasSoulRemnant) return false;
            int amount = soulRemnant;
            hasSoulRemnant = false;
            soulRemnant = 0;
            AddSouls(amount);
            return true;
        }

        public void ReturnFromDeath(Transform fire, Transform body)
        {
            isDead = false;
            if (fire != null && body != null)
                body.position = fire.position;
        }

        public bool ApplyEmber(float ratio, string assetName)
        {
            if (!EmberHealth.TryRaise(maxHP, ratio, out float newMax))
            {
                Debug.LogError($"{assetName}: healthRatio 未填");
                return false;
            }
            if (!emberLit)
                emberBaseMaxHP = maxHP;
            emberLit = true;
            maxHP = newMax;
            currentHP = newMax;
            if (player != null && player.ui != null && player.ui.hud != null && player.ui.hud.healthBar != null)
            {
                player.ui.hud.healthBar.SetMaxHP(maxHP);
                player.ui.hud.healthBar.SetCurrentHP(currentHP);
            }
            return true;
        }

        public void ExtinguishEmber()
        {
            if (!emberLit) return;
            maxHP = emberBaseMaxHP;
            currentHP = maxHP;
            emberLit = false;
            if (player != null && player.ui != null && player.ui.hud != null && player.ui.hud.healthBar != null)
            {
                player.ui.hud.healthBar.SetMaxHP(maxHP);
                player.ui.hud.healthBar.SetCurrentHP(currentHP);
            }
        }

        public void RestoreVitalsToMax()
        {
            currentHP = maxHP;
            currentStamina = maxStamina;
            currentMP = maxMP;
            if (player != null && player.pInventory != null)
                player.pInventory.RefillConsumablesToMax();
            player.ui.hud.healthBar.SetCurrentHP(currentHP);
            player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);
            player.ui.hud.focusPointsBar.SetCurrentMP(currentMP);
        }

        protected override void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer -= Time.deltaTime;
            }
            else
            {
                totalPoiseDefence = armorPoiseBonus;
            }
        }

        public override void RegenerateStamina()
        {
            if (player.isInteracting || player.isSprinting)
            {
                staminaRegenTimer = 0;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;
                if (currentStamina < maxStamina && staminaRegenTimer > 1f)
                {
                    float amount = player.pCombat.isBlocking
                        ? staminaRegenerationAmountWhilstBlocking
                        : staminaRegenerationAmount;
                    currentStamina = Mathf.Min(currentStamina + amount * Time.deltaTime, maxStamina);
                    player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);
                }
            }
        }
    }
}