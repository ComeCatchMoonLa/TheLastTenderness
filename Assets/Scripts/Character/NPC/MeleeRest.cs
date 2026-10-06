using UnityEngine;

namespace CatchMoon
{
    public static class MeleeRest
    {
        public static bool Keep(bool isBoss, NPCCombatStyle style)
        {
            return !isBoss && style == NPCCombatStyle.melee;
        }

        public static void Restore(GameObject body, Vector3 origin, CharacterStatsManager stats)
        {
            body.SetActive(true);
            body.transform.position = origin;
            stats.isDead = false;
            stats.currentHP = stats.maxHP;
        }
    }
}
