using UnityEngine;

namespace CatchMoon
{
    public static class OnlineMarks
    {
        public static bool Shown(bool online)
        {
            return online;
        }

        public static bool TryRead(string assetName, bool online, string body, out string read)
        {
            read = null;
            if (!online) return false;
            if (string.IsNullOrEmpty(body))
            {
                Debug.LogError($"{assetName}: body 未填");
                return false;
            }
            read = body;
            return true;
        }

        public static bool TryRate(bool online, bool alreadyRated)
        {
            return online && !alreadyRated;
        }
    }
}
