using UnityEngine;

namespace CatchMoon
{
    public static class StoryNpcCycle
    {
        public static void Place(GameObject body, Vector3 home, CharacterStatsManager stats)
        {
            if (body == null) return;
            body.SetActive(true);
            body.transform.position = home;
            if (stats == null) return;
            stats.isDead = false;
            stats.currentHP = stats.maxHP;
        }
    }
}
