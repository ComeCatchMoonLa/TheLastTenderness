using UnityEngine;

namespace CatchMoon
{
    public class WorldUIManager : MonoBehaviour
    {
        public UIBossHealthBar bossHealthBar;

        private void Awake()
        {
            bossHealthBar = FindAnyObjectByType<UIBossHealthBar>();

            if (bossHealthBar == null)
                Debug.LogError("bossHealthBar == null");
        }

        public void ActivateBossBar(EnemyStatsManager eStats)
        {
            if (bossHealthBar == null)
            {
                Debug.LogError("bossHealthBar == null");
                return;
            }
            if (!BossBar.TryShow(eStats.cName, eStats.currentHP, eStats.maxHP, eStats.name, out string shown, out float length))
                return;
            bossHealthBar.SetBossName(shown);
            bossHealthBar.SetBossMaxHealth(eStats.maxHP);
            bossHealthBar.SetBossCurrentHealth(eStats.maxHP * length);
            bossHealthBar.gameObject.SetActive(true);
        }

        public void DeactivateBossBar()
        {
            bossHealthBar.gameObject.SetActive(false);
        }
    }
}
