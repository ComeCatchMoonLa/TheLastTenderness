using UnityEngine;

namespace CatchMoon
{
    /// <summary>
    /// 伤害玩家
    /// </summary>
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 25;

        /// <summary>
        /// 进入触发器
        /// </summary>
        /// <param name="other">触发器捕获的对象</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                PlayerStatsManager playerStatsManager = other.GetComponent<PlayerStatsManager>();

                if (playerStatsManager != null)
                {
                    playerStatsManager.TakeDamage("Damage_ForwardRight_01", new DamageSegments { Fire = damage });
                }
            }
        }
    }
}