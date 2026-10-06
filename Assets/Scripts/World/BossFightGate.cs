using UnityEngine;

namespace CatchMoon
{
    public static class BossFightGate
    {
        public static bool Active()
        {
            WorldEventManager[] events = Object.FindObjectsByType<WorldEventManager>(FindObjectsInactive.Include);
            bool found = false;
            for (int i = 0; i < events.Length; i++)
            {
                if (events[i] == null) continue;
                found = true;
                if (events[i].bossFightIsActive)
                    return true;
            }
            if (!found)
            {
                Debug.LogError("WorldEventManager: bossFightIsActive 未找到");
                return true;
            }
            return false;
        }
    }
}
