using UnityEngine;

namespace CatchMoon
{
    public class EventColliderBeginBossFight : MonoBehaviour
    {
        WorldManager world;

        public EnemyManager enemy;

        private void Awake()
        {
            world = GetComponentInParent<WorldManager>();
        }
        private void Start()
        {
            if (enemy == null || !enemy.aiSettings.isBoss)
            {
                Debug.LogError("请拖入boss对象");
                return;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
                world.wEvent.ActivateBossFight(enemy);
        }
    }
}