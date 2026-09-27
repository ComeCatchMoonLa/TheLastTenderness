using UnityEngine;

namespace CatchMoon
{
    public class FogWall : MonoBehaviour
    {
        private void Start()
        {
            DeactivateFogWall();
        }

        public void ActivateFogWall()
        {
            gameObject.SetActive(true);
        }

        public void DeactivateFogWall()
        {
            gameObject.SetActive(false);
        }
    }
}