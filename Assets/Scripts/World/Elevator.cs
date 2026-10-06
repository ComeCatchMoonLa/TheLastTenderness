using UnityEngine;

namespace CatchMoon
{
    public static class Elevator
    {
        public static bool Next(string assetName, Vector3 home, Vector3 away, bool goingAway, out Vector3 position, out bool nextGoingAway)
        {
            position = goingAway ? away : home;
            nextGoingAway = !goingAway;
            if (home == away)
            {
                Debug.LogError($"{assetName}: away 未填");
                position = home;
                nextGoingAway = goingAway;
                return false;
            }
            return true;
        }
    }
}
