using UnityEngine;

namespace CatchMoon
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        EnemyManager enemy;

        UIEnemyHealthBar enemyHealthBar;

        WorldManager world;

        [Header("Souls Awarde Death")]
        public int soulsAwardedDeath = 50; // 死亡后生成的灵魂数量

        public float destoryWaitTime = 3f;

        BossCombatStanceState bossCombatStanceState;

        protected override void Awake()
        {
            base.Awake();
            enemy = GetComponent<EnemyManager>();

            enemyHealthBar = GetComponentInChildren<UIEnemyHealthBar>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();

            world = FindAnyObjectByType<WorldManager>();

            #region 检错
            if (enemy.aiSettings.isBoss)
            {
                if (bossCombatStanceState == null)
                { Debug.LogError("bossCombatStanceState is null."); }
            }
            else
            {
                if (enemyHealthBar == null)
                { Debug.LogError("enemyHealthBar is null."); }
            }
            #endregion
        }
        protected override void Start()
        {
            base.Start();

            if (!enemy.aiSettings.isBoss)
            {
                enemyHealthBar.SetCurrentHealth(maxHP);
                enemyHealthBar.SetMaxHealth(maxHP);
            }
        }

        public override bool TakeDamage(string damageAnimation, float physicalDamage = 0f, float fireDamage = 0f, float magicDamage = 0f, float lightningDamage = 0f, float darkDamage = 0f)
        {
            if (base.TakeDamage(damageAnimation, physicalDamage, fireDamage, magicDamage, lightningDamage, darkDamage))
            {
                if (!enemy.aiSettings.isBoss)
                {
                    enemyHealthBar.SetCurrentHealth(currentHP);
                    enemyHealthBar.ShowBar();
                }
                else
                {
                    UpdateBossHealthBar(currentHP, maxHP);
                }

                if (currentHP <= 0)
                    Death(enemy);

                return true;
            }
            return false;
        }
        public override void Death(CharacterManager enemy, string deathAnimation = "Death")
        {
            base.Death(enemy, deathAnimation);
            HandleEnemyDeathEvent();
        }

        public void HandleEnemyDeathEvent()
        {
            PlayerManager player = FindAnyObjectByType<PlayerManager>();
            if (player == null)
            {
                Debug.LogError("player is null.");
                return;
            }

            if (player.pCamera.lockOnFlag)
            {
                player.pCamera.Unlock();
                player.pCamera.lockOnMode = true;
                player.pCamera.UpdateLockOnTargets();
            }
            AwardSoulsOnDeath(player);
            
            Destroy(gameObject, destoryWaitTime);
        }
        public void AwardSoulsOnDeath(PlayerManager player)
        {
            player.pStats.AddSouls(soulsAwardedDeath);
            player.ui.hud.soulCountUI.SetSoulCountText(player.pStats.soulCount);
        }

        /// <summary>
        /// 打破镇定(进入僵直状态)
        /// </summary>
        public void BreakGuard()
        {
            enemy.eAnimator.PlayTargetAnimation("Break Guard", true);
        }

        /// <summary>
        /// 更新Boss血条
        /// </summary>
        public void UpdateBossHealthBar(float currentHealth, float maxHealth)
        {
            world.wUI.bossHealthBar.SetBossCurrentHealth(currentHealth);

            if (!bossCombatStanceState.hasPhaseShifted && currentHealth <= maxHealth * 0.5f)
                ShiftToSecondPhase();
        }

        /// <summary>
        /// 切换至第二阶段
        /// </summary>
        public void ShiftToSecondPhase()
        {
            enemy.eStats.isInvulnerable = true;
            enemy.isPhaseShifting = true;
            enemy.eAnimator.PlayTargetAnimation("Phase Shift", true);
            bossCombatStanceState.hasPhaseShifted = true;
        }
    }
}