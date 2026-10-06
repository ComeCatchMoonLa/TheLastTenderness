using UnityEngine;

namespace CatchMoon
{
    public class EmberSummonSign : MonoBehaviour
    {
        public GameObject helper;

        public bool TrySummon(bool emberLit)
        {
            if (helper == null)
            {
                Debug.LogError($"{name}: helper 未填");
                return false;
            }
            bool appears = EmberSummon.Appears(emberLit, true);
            helper.SetActive(appears);
            return appears;
        }
    }
}
