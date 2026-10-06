using UnityEngine;

namespace CatchMoon
{
    public static class HingedDoor
    {
        public static bool FromFront(Vector3 doorForward, Vector3 fromDoorToActor)
        {
            doorForward.y = 0f;
            fromDoorToActor.y = 0f;
            if (doorForward.sqrMagnitude <= 0.0001f || fromDoorToActor.sqrMagnitude <= 0.0001f)
                return false;
            return Vector3.Dot(doorForward.normalized, fromDoorToActor.normalized) > 0f;
        }

        public static bool TryOpen(bool alreadyOpen, bool fromFront)
        {
            if (alreadyOpen) return false;
            return fromFront;
        }

        public static bool AfterRest(bool opened)
        {
            return opened;
        }

        public static Quaternion Swing(Quaternion closed, bool opened)
        {
            if (!opened) return closed;
            return closed * Quaternion.Euler(0f, 90f, 0f);
        }
    }
}
