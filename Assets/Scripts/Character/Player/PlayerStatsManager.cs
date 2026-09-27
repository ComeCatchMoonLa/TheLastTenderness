using UnityEngine;

namespace CatchMoon
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        PlayerManager player;

        [Header("灵魂数量")]
        public int soulCount = 0;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }
        protected override void Start()
        {
            base.Start();

            player.ui.hud.healthBar.SetMaxHP(maxHP);
            player.ui.hud.healthBar.SetCurrentHP(currentHP);

            player.ui.hud.staminaBar.SetMaxStamina(maxStamina);
            player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);

            player.ui.hud.manaBar.SetMaxMP(maxMP);
            player.ui.hud.manaBar.SetCurrentMP(currentMP);
        }

        public override bool TakeDamage(string damageAnimation, float pd = 0f, float fd = 0f, float md = 0f, float ld = 0f, float dd = 0f, bool playHurtSound = true)
        {
            if (base.TakeDamage(damageAnimation, pd, fd, md, ld, dd, playHurtSound))
            {
                player.ui.hud.healthBar.SetCurrentHP(currentHP);

                if (currentHP <= 0)
                    base.Death(player);

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

        public override void EmptyStamina()
        {
            base.EmptyStamina();
            player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);
        }

        public override bool AddMP(float addAmount)
        {
            if (base.AddMP(addAmount))
            {
                player.ui.hud.manaBar.SetCurrentMP(currentMP);
                return true;
            }
            else return false;
        }
        public override bool DeductMP(float deductAmount)
        {
            if (base.DeductMP(deductAmount))
            {
                player.ui.hud.manaBar.SetCurrentMP(currentMP);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 获取灵魂
        /// </summary>
        public void AddSouls(int souls)
        {
            soulCount += souls;
        }

        protected override void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer -= Time.deltaTime;
            }
            else
            {
                if (!player.isInteracting)
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
                    if (player.pCombat.isBlocking)
                    {
                        currentStamina += staminaRegenerationAmountWhilstBlocking * Time.deltaTime;
                        player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);
                    }
                    else
                    {
                        currentStamina += staminaRegenerationAmount * Time.deltaTime;
                        player.ui.hud.staminaBar.SetCurrentStamina(currentStamina);
                    }
                }
            }
        }
    }
}