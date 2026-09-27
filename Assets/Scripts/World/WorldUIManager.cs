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
            bossHealthBar.SetBossName(eStats.cName);
            bossHealthBar.SetBossCurrentHealth(eStats.maxHP);
            bossHealthBar.SetBossMaxHealth(eStats.maxHP);

            bossHealthBar.gameObject.SetActive(true);
        }

        public void DeactivateBossBar()
        {
            bossHealthBar.gameObject.SetActive(false);
        }
    }
}
