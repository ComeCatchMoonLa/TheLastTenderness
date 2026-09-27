using UnityEngine;

namespace CatchMoon
{
    public class DestoryAuto : MonoBehaviour
    {
        public float timeUntilDestoryed = 1f; // 销毁前等待的时间

        private void Start()
        {
            Destroy(gameObject, timeUntilDestoryed);
        }
    }
}