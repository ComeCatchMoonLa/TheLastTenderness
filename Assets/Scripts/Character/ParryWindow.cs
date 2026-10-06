using UnityEngine;

namespace CatchMoon
{
    public static class ParryWindow
    {
        public static bool Open(int elapsedFrames, int windowFrames, string assetName)
        {
            if (windowFrames <= 0)
            {
                Debug.LogError($"{assetName}: parryWindowFrames 未填");
                return false;
            }
            return elapsedFrames >= 0 && elapsedFrames < windowFrames;
        }
    }
}
