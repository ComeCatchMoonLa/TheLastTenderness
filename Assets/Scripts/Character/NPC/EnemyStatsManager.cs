using System.Collections;
using UnityEngine;

namespace CatchMoon
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        EnemyManager enemy;

        UIEnemyHealthBar enemyHealthBar;

        WorldManager world;

        PlayerManager player;

        [Header("Souls Awarde Death")]
        public int soulsAwardedDeath = 50; // 死亡后生成的灵魂数量

        public float destoryWaitTime = 3f;
        public bool hiddenForRest;
        public Vector3 restOrigin;

        BossCombatStanceState bossCombatStanceState;

        protected override void Awake()
        {
            base.Awake();
            restOrigin = transform.position;
            enemy = GetComponent<EnemyManager>();

            enemyHealthBar = GetComponentInChildren<UIEnemyHealthBar>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();

            world = FindAnyObjectByType<WorldManager>();
            player = FindAnyObjectByType<PlayerManager>();

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

        public override bool TakeDamage(string damageAnimation, DamageSegments damage, bool playHurtSound = true)
        {
            if (base.TakeDamage(damageAnimation, damage, playHurtSound))
            {
                if (enemy != null && !string.IsNullOrEmpty(damageAnimation))
                    AllyCall.Interrupt(ref enemy.isCalling, ref enemy.callFinished);

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

            if (enemy != null && enemy.aiSettings != null && enemy.aiSettings.isBoss
                && world != null && world.wEvent != null && !world.wEvent.bossHasBeenDefeated)
                world.wEvent.BossHasBeenDefeated();

            if (enemy != null && enemy.aiSettings != null
                && MeleeRest.Keep(enemy.aiSettings.isBoss, enemy.aiSettings.combatStyle))
            {
                hiddenForRest = true;
                gameObject.SetActive(false);
                return;
            }

            if (enemy != null && enemy.aiSettings != null && enemy.aiSettings.isBoss)
            {
                StartCoroutine(HideBossAfterDelay());
                return;
            }

            Destroy(gameObject, destoryWaitTime);
        }

        public void StopBossHide()
        {
            StopAllCoroutines();
        }

        IEnumerator HideBossAfterDelay()
        {
            yield return new WaitForSeconds(destoryWaitTime);
            gameObject.SetActive(false);
        }

        public void RestoreAfterRest()
        {
            if (!hiddenForRest) return;
            if (enemyHealthBar != null)
            {
                enemyHealthBar.SetMaxHealth(maxHP);
                enemyHealthBar.SetCurrentHealth(maxHP);
            }
            MeleeRest.Restore(gameObject, restOrigin, this);
            hiddenForRest = false;
        }
        public void AwardSoulsOnDeath(PlayerManager player)
        {
            player.pStats.AddSouls(soulsAwardedDeath);
        }

        /// <summary>
        /// 打破镇定(进入僵直状态)
        /// </summary>
        public void BreakGuard()
        {
            enemy.eAnimator.PlayTargetAnimation("Guard_Break", true);
        }

        /// <summary>
        /// 更新Boss血条
        /// </summary>
        public void UpdateBossHealthBar(float currentHealth, float maxHealth)
        {
            world.wUI.bossHealthBar.SetBossCurrentHealth(currentHealth);

            if (bossCombatStanceState != null
                && BossPhase.ShouldShift(currentHealth, maxHealth, bossCombatStanceState.phaseThreshold, bossCombatStanceState.hasPhaseShifted))
                ShiftToSecondPhase();
        }

        /// <summary>
        /// 切换至第二阶段
        /// </summary>
        public void ShiftToSecondPhase()
        {
            if (bossCombatStanceState.phaseBlocksHits)
                enemy.eStats.isInvulnerable = true;
            enemy.isPhaseShifting = true;
            enemy.eAnimator.PlayTargetAnimation("Phase Shift", true);
            bossCombatStanceState.hasPhaseShifted = true;
        }
    }
}