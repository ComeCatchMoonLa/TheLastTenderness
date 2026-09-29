using UnityEngine;

namespace CatchMoon
{
    public class DestroyAuto : MonoBehaviour
    {
        public float timeUntilDestroyed = 1f; // 销毁前等待的时间

        private void Start()
        {
            Destroy(gameObject, timeUntilDestroyed);
        }
    }
}