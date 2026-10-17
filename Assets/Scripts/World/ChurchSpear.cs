using UnityEngine;

namespace CatchMoon
{
    public static class ChurchSpear
    {
        public static EnemyManager Opponent(bool offline, EnemyManager local, string assetName)
        {
            if (!offline) return null;
            if (local == null)
            {
                Debug.LogError($"{assetName}: local 未填");
                return null;
            }
            return local;
        }
    }
}
